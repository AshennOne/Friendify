using API.Data.Repositories;
using API.Entities;
using API.Extensions;
using API.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
/// <summary>
/// Manages operations related to liking and unliking posts, including getting liked posts, adding likes, and removing likes.
/// </summary>
public class PostLikesController : BaseApiController
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
    /// Initializes a new instance of the <see cref="PostLikesController"/> class.
    /// </summary>
    /// <param name="unitOfWork"></param>
    /// <param name="userManager"></param>
    public PostLikesController(IUnitOfWork unitOfWork, UserManager<User> userManager)
    {
        _unitOfWork = unitOfWork;
        _userManager = userManager;
    }
    /// <summary>
    /// Retrieves the posts liked by the current user.
    /// </summary>
    /// <returns>Status code of operation with list of liked posts</returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Post>>> GetLikedPostsForUser()
    {
        var user = await GetUser();
        if (user == null) return NotFound("User not found");
        var likes = await _unitOfWork.PostLikeRepository.GetLikedPosts(user.Id);
        return Ok(likes);
    }
    /// <summary>
    /// Adds a like to the specified post.
    /// </summary>
    /// <param name="postId"></param>
    /// <returns>Status code of operation</returns>
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
    /// <summary>
    /// Removes a like from the specified post.
    /// </summary>
    /// <param name="postId"></param>
    /// <returns>Status code of operation</returns>
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
    /// <summary>
    /// Retrieves the current user asynchronously.
    /// </summary>
    /// <returns>Current user data</returns>
    private async Task<User> GetUser()
    {
        var username = User.GetUsernameFromToken();
        return await _userManager.FindByNameAsync(username);
    }
}