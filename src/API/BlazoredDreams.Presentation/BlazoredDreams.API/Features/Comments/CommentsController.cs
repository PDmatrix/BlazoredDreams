using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using BlazoredDreams.Application.Comments.Commands;
using BlazoredDreams.Application.Comments.Models;
using BlazoredDreams.Application.Comments.Queries;
using BlazoredDreams.Application.Dreams.Commands;
using BlazoredDreams.Application.Dreams.Models;
using BlazoredDreams.Application.Dreams.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlazoredDreams.API.Features.Comments
{
	[Route("api/posts/{postId:int}/comment")]
	public class CommentsController : BaseController
	{
		[HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesDefaultResponseType]
        public async Task<ActionResult<IEnumerable<CommentDto>>> GetAll(int postId)
		{
			var res = await Mediator.Send(new GetAllCommentsQuery {PostId = postId});
			return res.ToList();
		}
      
        [Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesDefaultResponseType]
        [Consumes("application/json")]
        public async Task<ActionResult> Create(int postId, CommentRequest commentRequest)
        {
	        var addCommentCommand = new AddCommentCommand
	        {
		        Content = commentRequest.Content,
		        PostId = postId,
		        UserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value
	        };
	        var createdCommentId = await Mediator.Send(addCommentCommand);
	        return StatusCode(201, createdCommentId);
        }
        
        [Authorize]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Delete(int id)
        {
	        await Mediator.Send(new DeleteCommentCommand {Id = id});
	        return NoContent();
        }
        
        [Authorize]
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesDefaultResponseType]
        [Consumes("application/json")]
        public async Task<ActionResult> Update(int id, CommentRequest commentRequest)
        {
	        var updateCommentCommand = new UpdateCommentCommand
	        {
		        Id = id,
		        Content = commentRequest.Content
	        };
	        await Mediator.Send(updateCommentCommand);
	        return NoContent();
        }
	}
}