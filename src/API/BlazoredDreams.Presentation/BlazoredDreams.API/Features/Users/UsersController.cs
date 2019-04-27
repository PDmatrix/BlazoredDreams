using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BlazoredDreams.Application.Comments.Commands;
using BlazoredDreams.Application.Users.Commands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlazoredDreams.API.Features.Users
{
	public class UsersController : BaseController
	{
        [Authorize]
        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesDefaultResponseType]
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
	}
}