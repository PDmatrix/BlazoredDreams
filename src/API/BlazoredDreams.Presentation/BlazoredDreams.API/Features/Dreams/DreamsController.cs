using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BlazoredDreams.Application.Dreams.Commands;
using BlazoredDreams.Application.Dreams.Models;
using BlazoredDreams.Application.Dreams.Queries;
using BlazoredDreams.Application.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlazoredDreams.API.Features.Dreams
{
    public class DreamsController : BaseController
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<Page<DreamDto>>> GetAll(int page = 1)
        {
	        if (page < 1)
		        page = 1;

	        var getAllDreamsQuery = new GetAllDreamsQuery
	        {
		        Page = page,
		        UserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value
	        };
	        
	        return await Mediator.Send(getAllDreamsQuery);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<DreamDto>> GetById(int id)
        {
	        var res = await Mediator.Send(new GetDreamQuery {Id = id});
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
        public async Task<ActionResult> Create(DreamRequest dreamRequest)
        {
	        var addDreamCommand = new AddDreamCommand
	        {
		        Content = dreamRequest.Content,
		        UserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value,
		        Date = dreamRequest.Date
	        };
	        var createdDreamId = await Mediator.Send(addDreamCommand);
	        return CreatedAtAction(nameof(GetById), new {id = createdDreamId}, null);
        }

        [Authorize]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Delete(int id)
        {
	        await Mediator.Send(new DeleteDreamCommand {Id = id});
	        return NoContent();
        }

        [Authorize]
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesDefaultResponseType]
        [Consumes("application/json")]
        public async Task<ActionResult> Update(int id, DreamRequest dreamRequest)
        {
	        var updateDreamCommand = new UpdateDreamCommand
	        {
		        Id = id,
		        Content = dreamRequest.Content,
	        };
	        await Mediator.Send(updateDreamCommand);
	        return NoContent();
        }
    }
}