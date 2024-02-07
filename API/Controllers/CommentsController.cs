using API.Dtos;
using API.Entities;
using API.Extensions;
using API.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    /// <summary>
    /// Manages operations related to comments on posts, such as creating, getting, updating and deleting comments.
    /// </summary>
    public class CommentsController : BaseApiController
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
        /// Initializes a new instance of the <see cref="CommentsController"/> class.
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="userManager"></param>
        public CommentsController(IUnitOfWork unitOfWork, UserManager<User> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;


        }
        /// <summary>
        /// Retrieves comments associated with a specific post.
        /// </summary>
        /// <param name="postId"></param>
        /// <returns>Status code of operation</returns>
        [HttpGet("{postId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<CommentResponseDto>))]
        public IActionResult GetCommentsForPost(int postId)
        {
            var comments = _unitOfWork.CommentRepository.GetCommentsForPost(postId);
            return Ok(comments);

        }
        /// <summary>
        /// Adds a new comment to a post.
        /// </summary>
        /// <param name="commentDto"></param>
        /// <returns>Status code of operation</returns>
        [HttpPost]
        public async Task<ActionResult> AddComment([FromBody] CommentDto commentDto)
        {
            var user = await GetUser();
            if (user == null) return NotFound("User not found");
            var comment = new Comment
            {
                Content = commentDto.Content,
                CommentedById = user.Id,
                PostId = commentDto.PostId,
                Post = await _unitOfWork.PostRepository.GetPostById(commentDto.PostId)
            };
            await _unitOfWork.CommentRepository.AddComment(comment);
            if (await _unitOfWork.SaveChangesAsync())
            {
                if (comment.CommentedById != comment.Post.Author.Id)
                {
                    await _unitOfWork.NotificationRepository.AddNotification(user, comment.Post.Author, NotiType.Comment, comment.PostId);

                }
                return Ok("Success");
            }

            return BadRequest("Adding comment failed");
        }
        /// <summary>
        /// Edits an existing comment.
        /// </summary>
        /// <param name="commentDto"></param>
        /// <param name="id"></param>
        /// <returns>Status code of operation</returns>
        [HttpPut("{id}")]
        public async Task<ActionResult> EditComment([FromBody] CommentDto commentDto, [FromRoute] int id)
        {
            var user = await GetUser();
            if (user == null) return NotFound("User not found");
            if (!await _unitOfWork.CommentRepository.BelongsToUser(user.Id, id)) return BadRequest("You can edit only your own comments");
            await _unitOfWork.CommentRepository.EditComment(id, user.Id, commentDto.Content);
            if (await _unitOfWork.SaveChangesAsync())
                return Ok("Success");
            return BadRequest("Editing comment failed");
        }
        /// <summary>
        /// Deletes a comment.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Status code of operation</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteComment(int id)
        {
            var user = await GetUser();
            if (user == null) return NotFound("User not found");
            if (!await _unitOfWork.CommentRepository.BelongsToUser(user.Id, id)) return BadRequest("You can delete only your own comments");
            var comment = await _unitOfWork.CommentRepository.GetCommentById(id);
            await _unitOfWork.CommentRepository.DeleteComment(id, user.Id);
            if (await _unitOfWork.SaveChangesAsync())
            {
                await _unitOfWork.NotificationRepository.RemoveNotification(NotiType.Comment,comment.PostId);
                return Ok("Success");
            }

            return BadRequest("Deleting comment failed");
        }
        /// <summary>
        /// Retrieves the current user.
        /// </summary>
        /// <returns>Current user data</returns>
        private async Task<User> GetUser()
        {
            var username = User.GetUsernameFromToken();
            return await _userManager.FindByNameAsync(username);

        }

    }
}