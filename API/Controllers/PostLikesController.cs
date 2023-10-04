using API.Data.Repositories;
using API.Entities;
using API.Extensions;
using API.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class PostLikesController : BaseApiController
{
  private readonly UserManager<User> _userManager;
  private readonly IUnitOfWork _unitOfWork;
  public PostLikesController(IUnitOfWork unitOfWork, UserManager<User> userManager)
  {
    _unitOfWork = unitOfWork;
    _userManager = userManager;
  }
  [HttpGet]
  public async Task<ActionResult<IEnumerable<Post>>> GetLikedPostsForUser()
  {
    var user = await GetUser();
    if (user == null) return NotFound("User not found");
    var likes = await _unitOfWork.PostLikeRepository.GetLikedPosts(user.Id);
    return Ok(likes);
  }
  [HttpPost("{postId}")]
  public async Task<ActionResult> AddLike(int postId)
  {
    var user = await GetUser();
    if (user == null) return NotFound("User not found");
    var belongsToUser = await _unitOfWork.PostLikeRepository.PostBelongsToUser(user, postId);
    if (belongsToUser) return BadRequest("You can't like and dislike your own post");
    var like = await _unitOfWork.PostLikeRepository.AddLike(user.Id, postId);

    var post = await _unitOfWork.PostRepository.GetPostById(postId);
    var Author = await _userManager.FindByIdAsync(post.AuthorId);
    if (await _unitOfWork.SaveChangesAsync())
    {
      await _unitOfWork.NotificationRepository.AddNotification(user, Author, NotiType.PostLike, like.PostId);
      return Ok("Success");
    }

    else return BadRequest("Adding like failed");
  }
  [HttpDelete("{postId}")]
  public async Task<ActionResult> RemoveLike(int postId)
  {
    var user = await GetUser();
    if (user == null) return NotFound("User not found");
    if (await _unitOfWork.PostLikeRepository.PostBelongsToUser(user, postId))
    {
      return BadRequest("You can't like and dislike your own post");
    }

   var like = await _unitOfWork.PostLikeRepository.RemoveLike(user.Id, postId);
    if (await _unitOfWork.SaveChangesAsync())
    {
      await _unitOfWork.NotificationRepository.RemoveNotification(NotiType.PostLike, like.PostId);
      return Ok("Success");
    }

    else return BadRequest("Deleting like failed");
  }
  private async Task<User> GetUser()
  {
    var username = User.GetUsernameFromToken();
    return await _userManager.FindByNameAsync(username);
  }
}