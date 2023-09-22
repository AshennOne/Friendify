using API.Dtos;
using API.Entities;
using API.Extensions;
using API.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class PostsController : BaseApiController
    {
        private readonly IPostRepository _postRepository;
        private readonly UserManager<User> _userManager;
        public PostsController(IPostRepository postRepository, UserManager<User> userManager)
        {
            _userManager = userManager;
            _postRepository = postRepository;

        }
        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<PostDto>>> GetAllPostsExceptUser()
        {
            var username = User.GetUsernameFromToken();
            var user = await _userManager.Users.Include(p => p.Posts).FirstOrDefaultAsync(u => u.UserName == username);
            if (user == null) return NotFound("User not found");
            var posts = _postRepository.GetAllPosts();
            return Ok(posts);
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PostDto>>> GetPostsForUser()
        {
            var username = User.GetUsernameFromToken();
            if (username == null) return NotFound("user not found");
            var user = await _userManager.FindByNameAsync(username);
            if (user == null) return NotFound("user not found");
            var posts = _postRepository.GetPostsForUser(username);
            return Ok(posts);
        }
        [HttpPost]
        public async Task<ActionResult<Post>> AddPost(Post post)
        {
            var username = User.GetUsernameFromToken();
            var user = await _userManager.FindByNameAsync(username);
            post.AuthorId = user.Id;
            
            var userDto = new UserClientDto
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName,
                ImgUrl = user.ImgUrl,
                Id = user.Id
            };
            if (user == null) return NotFound("User not found");

            await _postRepository.AddPost(post);
            var newPost = new PostDto
            {
                Id = post.Id,
                Created = post.Created,
                Author = userDto,
                TextContent = post.TextContent,
                ImgUrl = post.ImgUrl
            };
            if (await _postRepository.SaveChangesAsync())
            {
                return Ok(newPost);
            }
            else
            {
                return BadRequest("Failed to add post");
            }
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePost(int id)
        {
            var username = User.GetUsernameFromToken();
            var user = await _userManager.Users.Include(p => p.Posts).FirstOrDefaultAsync(u => u.UserName == username);
            var userpost = user.Posts.FirstOrDefault(p => p.Id == id);
            if (userpost == null) return BadRequest("Unable to delete");
            if (userpost.RepostedFromId != 0) return BadRequest("You can't delete post this way, you have to unrepost");

            await _postRepository.DeletePost(id);
            if (await _postRepository.SaveChangesAsync())
            {
                return Ok("Succesfully deleted post");
            }
            else
            {
                return BadRequest("Failed to delete post");
            }
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> EditPost(int id, [FromBody] Post post)
        {
            var username = User.GetUsernameFromToken();
            var user = await _userManager.Users.Include(p => p.Posts).FirstOrDefaultAsync(u => u.UserName == username);
            var userpost = user.Posts.FirstOrDefault(p => p.Id == id);
            if (userpost == null) return BadRequest("Unable to edit");
            await _postRepository.EditPost(id, post);
            if (await _postRepository.SaveChangesAsync())
            {
                return Ok("Succesfully edited post");
            }
            else
            {
                return BadRequest("Failed to edit post");
            }
        }
    }
}