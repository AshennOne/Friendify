using API.Data.Repositories;
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
  [HttpPost("{postId}")]
  public async Task<ActionResult> AddLike(int postId)
  {
    var user = await GetUser();
    if (user == null) return NotFound("User not found");
   var belongsToUser = await _postLikeRepository.PostBelongsToUser(user, postId);
   if(belongsToUser) return BadRequest("You can't like and dislike your own post");
   await _postLikeRepository.AddLike(user.Id,postId);
    if (await _postLikeRepository.SaveChangesAsync())
      return Ok("Success");
    else return BadRequest("Adding like failed");
  }
  [HttpDelete("{postId}")]
  public async Task<ActionResult> RemoveLike(int postId)
  {
    var user = await GetUser();
    if (user == null) return NotFound("User not found");
    if(await _postLikeRepository.PostBelongsToUser(user,postId)){
      return BadRequest("You can't like and dislike your own post");
    }
  await _postLikeRepository.RemoveLike(user.Id,postId);
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