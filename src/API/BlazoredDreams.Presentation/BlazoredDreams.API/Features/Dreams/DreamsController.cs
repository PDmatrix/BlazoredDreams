using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BlazoredDreams.Application.Dreams.Commands;
using BlazoredDreams.Application.Dreams.Models;
using BlazoredDreams.Application.Dreams.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlazoredDreams.API.Features.Dreams
{
	public class DreamsController : BaseController
	{
		[HttpGet]
		[ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<DreamDto>>> GetAll(int page = 1)
		{
			if (page < 1)
				page = 1;
			
			var res = await Mediator.Send(new GetAllDreamsQuery {Page = page});
			return res.ToList();
		}
        

        [HttpGet("{id}")]
        public async Task<ActionResult<DreamDto>> GetById(int id)
        {
	        var res = await Mediator.Send(new GetDreamQuery {Id = id});
	        if (res == null)
		        return NotFound();
	        
	        return res;
        }
        
        [Authorize]
        [HttpPost]
        [Consumes("application/json")]
        public async Task<ActionResult> Create(DreamRequest dreamRequest)
        {
	        var addDreamCommand = new AddDreamCommand
	        {
		        Content = dreamRequest.Content,
		        UserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value
	        };
	        var createdDreamId = await Mediator.Send(addDreamCommand);
	        return CreatedAtAction(nameof(GetById), new {id = createdDreamId}, null);
        }
        
        [Authorize]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> Delete(int id)
        {
	        await Mediator.Send(new DeleteDreamCommand {Id = id});
	        return NoContent();
        }
        
        [Authorize]
        [HttpPut("{id}")]
        [Consumes("application/json")]
        public async Task<ActionResult> Update(int id, DreamRequest dreamRequest)
        {
	        var updateDreamCommand = new UpdateDreamCommand
	        {
		        Id = id,
		        Content = dreamRequest.Content
	        };
	        await Mediator.Send(updateDreamCommand);
	        return NoContent();
        }
	}
}