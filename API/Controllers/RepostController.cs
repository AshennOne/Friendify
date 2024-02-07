using API.Dtos;
using API.Entities;
using API.Extensions;
using API.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    /// <summary>
    /// Manages operations related to reposting and unreposting posts.
    /// </summary>
    public class RepostController : BaseApiController
    {
        /// <summary>
        /// Provides access to the unit of work for interacting with the database.
        /// </summary>
        private readonly IUnitOfWork _unitOfWork;
        /// <summary>
        /// Manages user-related operations such as finding correct user.
        /// </summary>
        private readonly UserManager<User> _userManager;
        /// <summary>
        /// Initializes a new instance of the <see cref="RepostController"/> class.
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="userManager"></param>
        public RepostController(UserManager<User> userManager, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;

        }
        /// <summary>
        /// Reposts a post with the specified ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Status code of operation with reposted post (our, non-original)</returns>
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
                await _unitOfWork.NotificationRepository.AddNotification(user, post.Author, NotiType.Repost, newPost.RepostedFromId);
                return Ok(newPost);
            }
            else
            {
                return BadRequest("Failed to repost");
            }
        }
        /// <summary>
        /// Unreposts a previously reposted post with the specified ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Status code of operation</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult> Unrepost(int id)
        {
            var username = User.GetUsernameFromToken();
            var user = await _userManager.FindByNameAsync(username);
            var post = await _unitOfWork.PostRepository.GetPostById(id);
            if (post == null) return BadRequest("post doesn't exists");
            if (user.Id == post.AuthorId) return BadRequest("Invalid request");
            if (post.RepostedFromId != 0) return BadRequest("This post is reposted");
           var repost = await _unitOfWork.PostRepository.UnRepost(post, user);
            if (await _unitOfWork.SaveChangesAsync())
            {
                await _unitOfWork.NotificationRepository.RemoveNotification(NotiType.Repost, repost.RepostedFromId);
                return Ok("Success");
            }
            else
            {
                return BadRequest("Failed to delete");
            }
        }
        /// <summary>
        /// Retrieves all posts reposted by the current user.
        /// </summary>
        /// <returns>Status code of operation with list of posts</returns>
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