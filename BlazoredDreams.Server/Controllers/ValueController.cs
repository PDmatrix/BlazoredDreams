using BlazoredDreams.Service;
using Microsoft.AspNetCore.Mvc;

namespace BlazoredDreams.Server.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class ValueController : ControllerBase
	{
		private readonly ITagService _tagService;

		public ValueController(ITagService tagService)
		{
			_tagService = tagService;
		}

		// GET
		[HttpGet("{id}")]
		public ActionResult Index(long id)
		{
			var tag = _tagService.Method(id);
			return Ok(tag);
		}
	}
}