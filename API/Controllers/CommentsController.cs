using API.Dtos;
using API.Entities;
using API.Extensions;
using API.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class CommentsController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly UserManager<User> _userManager;
        public CommentsController(IUnitOfWork unitOfWork, UserManager<User> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;


        }
        [HttpGet("{postId}")]
        public ActionResult<IEnumerable<CommentResponseDto>> GetCommentsForPost(int postId)
        {
            var comments = _unitOfWork.CommentRepository.GetCommentsForPost(postId);
            return Ok(comments);

        }
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
        private async Task<User> GetUser()
        {
            var username = User.GetUsernameFromToken();
            return await _userManager.FindByNameAsync(username);

        }

    }
}