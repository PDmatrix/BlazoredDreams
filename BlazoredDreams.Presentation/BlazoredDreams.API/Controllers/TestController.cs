using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlazoredDreams.API.Controllers
{
	[ApiController]
	[Produces("application/json")]
	[Route("api/[controller]")]
	public class TestController : ControllerBase
	{
		/// <summary>
		/// Creates a TodoItem.
		/// </summary>
		/// <remarks>
		/// Sample request:
		///
		///     POST /Todo
		///     {
		///        "id": 1,
		///        "name": "Item1",
		///        "isComplete": true
		///     }
		///
		/// </remarks>
		/// <returns>A newly created TodoItem</returns>
		/// <response code="200">If the item is null</response>
		[HttpGet]
		[ProducesDefaultResponseType]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public ActionResult<string> Test(int id)
		{
			return "Hello" + id;
		}
	}
}