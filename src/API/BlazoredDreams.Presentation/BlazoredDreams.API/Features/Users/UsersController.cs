using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BlazoredDreams.Application.Comments.Commands;
using BlazoredDreams.Application.Users.Commands;
using BlazoredDreams.Application.Users.Models;
using BlazoredDreams.Application.Users.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlazoredDreams.API.Features.Users
{
	public class UsersController : BaseController
	{
        [Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesDefaultResponseType]
        [Consumes("application/json")]
        public async Task<ActionResult> SignIn()
        {
	        var signInCommand = new SignInCommand
	        {
		        UserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value,
		        AccessToken = Request.Headers["Authorization"],
		        UserInfoEndpoint = User.FindAll("aud").Last()?.Value
	        };
	        await Mediator.Send(signInCommand);
	        return NoContent();
        }

        [Authorize]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<UserDto>> GetById()
        {
	        var getUserQuery = new GetUserQuery
	        {
		        UserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value
	        };
	        return await Mediator.Send(getUserQuery);
        }
        
        [Authorize]
        [HttpPost("image")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesDefaultResponseType]
        [Consumes("multipart/form-data")]
        public async Task<ActionResult> ChangeImage([FromForm] IFormFile file)
        {
	        await Mediator.Send(new ImageUploadCommand
	        {
		        FileStream = file.OpenReadStream(),
		        FileName = file.FileName,
		        UserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value
	        });
	        return NoContent();
        }
	}
}