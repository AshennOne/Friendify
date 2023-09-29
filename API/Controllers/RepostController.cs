using API.Dtos;
using API.Entities;
using API.Extensions;
using API.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class RepostController : BaseApiController
    {
        private readonly UserManager<User> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        public RepostController(UserManager<User> userManager, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;

        }
        [HttpPost("{id}")]
        public async Task<ActionResult<Post>> Repost(int id)
        {
            var username = User.GetUsernameFromToken();
            var user = await _userManager.FindByNameAsync(username);
            var post = await _unitOfWork.PostRepository.GetPostById(id);
            if (post == null) return BadRequest("post doesn't exists");
            if (post.AuthorId == user.Id) return BadRequest("You cannot repost your own post");
            if (_unitOfWork.PostRepository.CheckIsReposted(post, user.Id))
            {
                return BadRequest("You already reposted this post!");
            }
            post.RepostCount += 1;
            var newPost = new Post
            {
                Author = user,
                AuthorId = user.Id,
                TextContent = post.TextContent,
                ImgUrl = post.ImgUrl,
                RepostedFromId = post.Id,
                OriginalAuthorId = post.Author.Id,
                OriginalAuthor = post.Author
            };
            await _unitOfWork.PostRepository.AddPost(newPost);
            if (await _unitOfWork.SaveChangesAsync())
            {
                return Ok(newPost);
            }
            else
            {
                return BadRequest("Failed to repost");
            }
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> Unrepost(int id)
        {
            var username = User.GetUsernameFromToken();
            var user = await _userManager.FindByNameAsync(username);
            var post = await _unitOfWork.PostRepository.GetPostById(id);
            if (post == null) return BadRequest("post doesn't exists");
            if (user.Id == post.AuthorId) return BadRequest("Invalid request");
            if (post.RepostedFromId != 0) return BadRequest("This post is reposted");
            await _unitOfWork.PostRepository.UnRepost(post, user);
            if (await _unitOfWork.SaveChangesAsync())
            {

                return Ok("Success");
            }
            else
            {
                return BadRequest("Failed to delete");
            }
        }
        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<PostDto>>> GetAllRepostedPostsByUser()
        {
            var username = User.GetUsernameFromToken();
            var user = await _userManager.FindByNameAsync(username);
            if (user == null) return Unauthorized();
            var posts = await _unitOfWork.PostRepository.GetRepostedPosts(user);
            return Ok(posts);
        }
    }
}