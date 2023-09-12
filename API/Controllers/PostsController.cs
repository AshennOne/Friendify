using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class PostsController:BaseApiController
    {
        private readonly IPostRepository _postRepository;
        private readonly UserManager<User> _userManager;
        public PostsController(IPostRepository postRepository, UserManager<User> userManager)
        {
            _userManager = userManager;
            _postRepository = postRepository;

        }
        [HttpGet("{username}")]
        public async Task<ActionResult<IEnumerable<Post>>> GetPostsForUser(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            if(user == null) return NotFound("user not found");
            var posts = await _postRepository.GetPostsForUser(username);
            return Ok(posts);
        }
        [HttpPost]
        public async Task<ActionResult> AddPost(Post post){
           await _postRepository.AddPost(post);
           if(await _postRepository.SaveChangesAsync()){
            return Ok("Succesfully added new post");
           }else{
            return BadRequest("Failed to add post");
           }
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePost(int id)
        {
           await _postRepository.DeletePost(id);
           if(await _postRepository.SaveChangesAsync()){
            return Ok("Succesfully deleted post");
           }else{
            return BadRequest("Failed to delete post");
           }
        }
    }
}