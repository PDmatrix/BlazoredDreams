using System.Security.Claims;
using System.Threading.Tasks;
using BlazoredDreams.Application.Posts.Commands;
using BlazoredDreams.Application.Posts.Models;
using BlazoredDreams.Application.Posts.Queries;
using BlazoredDreams.Application.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlazoredDreams.API.Features.Posts
{
    public class PostsController : BaseController
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<Page<PostPreviewDto>>> GetAll(int page = 1)
        {
	        if (page < 1)
		        page = 1;
	        
	        return await Mediator.Send(new GetAllPostsQuery {Page = page});
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<PostDto>> GetById(int id)
        {
	        var res = await Mediator.Send(new GetPostQuery {Id = id});
	        if (res == null)
		        return NotFound();
	        
	        return res;
        }

        [Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesDefaultResponseType]
        [Consumes("application/json")]
        public async Task<ActionResult> Create(PostRequest postRequest)
        {
	        var addPostCommand = new AddPostCommand
	        {
		        Title = postRequest.Title,
		        Excerpt = postRequest.Excerpt,
		        DreamId = postRequest.DreamId,
		        Tags = postRequest.Tags.Split(','),
		        UserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value
	        };
	        var createdPostId = await Mediator.Send(addPostCommand);
	        return CreatedAtAction(nameof(GetById), new {id = createdPostId}, null);
        }

        [Authorize]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Delete(int id)
        {
	        await Mediator.Send(new DeletePostCommand {Id = id});
	        return NoContent();
        }

        [Authorize]
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesDefaultResponseType]
        [Consumes("application/json")]
        public async Task<ActionResult> Update(int id, PostRequest postRequest)
        {
	        var updatePostCommand = new UpdatePostCommand
	        {
		        Id = id,
		        Title = postRequest.Title,
		        Excerpt = postRequest.Excerpt
	        };
	        await Mediator.Send(updatePostCommand);
	        return NoContent();
        }

        [Authorize]
        [HttpPut("{id}/image")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesDefaultResponseType]
        [Consumes("multipart/form-data")]
        public async Task<ActionResult> AddImageToPost(int id, [FromForm] IFormFile file)
        {
	        var addImageToPostCommand = new AddImageToPostCommand
	        {
		        Id = id,
		        FileName = file.FileName,
		        FileStream = file.OpenReadStream()
	        };
	        await Mediator.Send(addImageToPostCommand);
	        return NoContent();
        }
    }
}