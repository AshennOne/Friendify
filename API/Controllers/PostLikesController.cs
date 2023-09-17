using API.Entities;
using API.Extensions;
using API.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class PostLikesController : BaseApiController
{
  private readonly IPostLikeRepository _postLikeRepository;
  private readonly UserManager<User> _userManager;
  public PostLikesController(IPostLikeRepository postLikeRepository, UserManager<User> userManager)
  {
    _userManager = userManager;
    _postLikeRepository = postLikeRepository;
  }
  [HttpGet]
  public async Task<ActionResult<IEnumerable<Post>>> GetLikedPostsForUser()
  {
    var user = await GetUser();
    if (user == null) return NotFound("User not found");
    var likes = await _postLikeRepository.GetLikedPosts(user.Id);
    return Ok(likes);
  }
  [HttpPost("{id}")]
  public async Task<ActionResult> AddLike(int id)
  {
    var user = await GetUser();
    if (user == null) return NotFound("User not found");
   var isAdded = await _postLikeRepository.AddLike(user.Id, id);
   if(!isAdded) return BadRequest("Invalid operation, you cannot like your own post and like post that is already liked");
    if (await _postLikeRepository.SaveChangesAsync())
      return Ok("Success");
    else return BadRequest("Adding like failed");
  }
  [HttpDelete("{id}")]
  public async Task<ActionResult> RemoveLike(int id)
  {
    var user = await GetUser();
    if (user == null) return NotFound("User not found");
   var isRemoved = await _postLikeRepository.RemoveLike(user.Id,id);
   if(!isRemoved) return BadRequest("you can delete only your own like");
    if (await _postLikeRepository.SaveChangesAsync())
      return Ok("Success");
    else return BadRequest("Deleting like failed");
  }
  private async Task<User> GetUser()
  {
    var username = User.GetUsernameFromToken();
    return await _userManager.FindByNameAsync(username);
  }
}