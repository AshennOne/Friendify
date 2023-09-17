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
        private readonly ICommentRepository _commentRepository;
        private readonly UserManager<User> _userManager;
        public CommentsController(ICommentRepository commentRepository, UserManager<User> userManager)
        {
            _userManager = userManager;
            _commentRepository = commentRepository;

        }
        [HttpGet("{postId}")]
        public ActionResult<IEnumerable<CommentResponseDto>> GetCommentsForPost(int postId)
        {
            var comments =  _commentRepository.GetCommentsForPost(postId);
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
                PostId = commentDto.PostId
            };
            await _commentRepository.AddComment(comment);
            if (await _commentRepository.SaveChangesAsync())
                return Ok("Success");
            return BadRequest("Adding comment failed");
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> EditComment([FromBody] CommentDto commentDto, [FromRoute] int id)
        {
            var user = await GetUser();
            if (user == null) return NotFound("User not found");
            if (!await _commentRepository.BelongsToUser(user.Id, id)) return BadRequest("You can edit only your own comments");
            await _commentRepository.EditComment(id, user.Id, commentDto.Content);
            if (await _commentRepository.SaveChangesAsync())
                return Ok("Success");
            return BadRequest("Editing comment failed");
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteComment(int id)
        {
            var user = await GetUser();
            if (user == null) return NotFound("User not found");
            if (!await _commentRepository.BelongsToUser(user.Id, id)) return BadRequest("You can delete only your own comments");
            await _commentRepository.DeleteComment(id, user.Id);
            if (await _commentRepository.SaveChangesAsync())
                return Ok("Success");
            return BadRequest("Deleting comment failed");
        }
        private async Task<User> GetUser()
        {
            var username = User.GetUsernameFromToken();
            return await _userManager.FindByNameAsync(username);

        }

    }
}