using API.Dtos;
using API.Entities;
using API.Extensions;
using API.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    /// <summary>
    /// Manages operations related to posts, including searching, getting, creation, editing, and deletion.
    /// </summary>
    public class PostsController : BaseApiController
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
        /// Initializes a new instance of the <see cref="PostsController"/> class.
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="userManager"></param>
        public PostsController(IUnitOfWork unitOfWork, UserManager<User> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;

        }
        /// <summary>
        /// Searches posts based on the provided search string.
        /// </summary>
        /// <param name="searchstring"></param>
        /// <returns>Status code of operation with list of posts that contain provided search string</returns>
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<PostDto>>> SearchPosts([FromQuery] string searchstring)
        {
            return Ok(await _unitOfWork.PostRepository.SearchPosts(searchstring));
        }
        /// <summary>
        /// Retrieves all posts except those belonging to the current user.
        /// </summary>
        /// <returns>Status code of operation with list of posts except those belonging to the current user</returns>
        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<PostDto>>> GetAllPostsExceptUser()
        {
            var username = User.GetUsernameFromToken();
            var user = await _userManager.Users.Include(p => p.Posts).FirstOrDefaultAsync(u => u.UserName == username);
            if (user == null) return NotFound("User not found");
            var posts = _unitOfWork.PostRepository.GetAllPosts();
            return Ok(posts);
        }
        /// <summary>
        /// Retrieves posts authored by the current user.
        /// </summary>
        /// <returns>Status code of operation with list of posts that are authored by the current user</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PostDto>>> GetPostsForUser()
        {
            var username = User.GetUsernameFromToken();
            if (username == null) return NotFound("user not found");
            var user = await _userManager.FindByNameAsync(username);
            if (user == null) return NotFound("user not found");
            var posts = _unitOfWork.PostRepository.GetPostsForUser(username);
            return Ok(posts);
        }
        /// <summary>
        /// Retrieves posts authored by the user with the specified ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Status code of operation with list of posts authored by the user with the specified ID.</returns>
        [HttpGet("user/{id}")]
        public async Task<ActionResult<IEnumerable<PostDto>>> GetPostsForUserId(string id)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (user == null) return NotFound("user not found");
            var posts = _unitOfWork.PostRepository.GetPostsForUser(user.UserName);
            return Ok(posts);
        }
        /// <summary>
        /// Retrieves a post by its ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Status code of operation with post object retrieved by its ID</returns>
        [HttpGet("{id}")]
         public async Task<ActionResult<PostDto>> GetPostsForById(int id)
         {
           var post = await _unitOfWork.PostRepository.GetPostById(id);
           if(post == null) return BadRequest("Post doesn't exists");
           return Ok(_unitOfWork.PostRepository.ConvertToDto(post));
         }
        /// <summary>
        /// Adds a new post.
        /// </summary>
        /// <param name="post"></param>
        /// <returns>Status code of operation with created post object</returns>
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

            await _unitOfWork.PostRepository.AddPost(post);
            var newPost = new PostDto
            {
                Id = post.Id,
                Created = post.Created,
                Author = userDto,
                TextContent = post.TextContent,
                ImgUrl = post.ImgUrl
            };
            if (await _unitOfWork.SaveChangesAsync())
            {
                return Ok(newPost);
            }
            else
            {
                return BadRequest("Failed to add post");
            }
        }
        /// <summary>
        /// Deletes a post by its ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Status code of operation</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePost(int id)
        {
            var username = User.GetUsernameFromToken();
            var user = await _userManager.Users.Include(p => p.Posts).FirstOrDefaultAsync(u => u.UserName == username);
            var userpost = user.Posts.FirstOrDefault(p => p.Id == id);
            if (userpost == null) return BadRequest("Unable to delete");
            if (userpost.RepostedFromId != 0) return BadRequest("You can't delete post this way, you have to unrepost");

            await _unitOfWork.PostRepository.DeletePost(id);
            if (await _unitOfWork.SaveChangesAsync())
            {
                return Ok("Succesfully deleted post");
            }
            else
            {
                return BadRequest("Failed to delete post");
            }
        }
        /// <summary>
        /// Edits a post with the specified ID.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="post"></param>
        /// <returns>Status code of operation</returns>
        [HttpPut("{id}")]
        public async Task<ActionResult> EditPost(int id, [FromBody] Post post)
        {
            var username = User.GetUsernameFromToken();
            var user = await _userManager.Users.Include(p => p.Posts).FirstOrDefaultAsync(u => u.UserName == username);
            var userpost = user.Posts.FirstOrDefault(p => p.Id == id);
            if (userpost == null) return BadRequest("Unable to edit");
            await _unitOfWork.PostRepository.EditPost(id, post);
            if (await _unitOfWork.SaveChangesAsync())
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