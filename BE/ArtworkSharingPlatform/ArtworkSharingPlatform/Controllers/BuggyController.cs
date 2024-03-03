using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ArtworkSharingHost.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class BuggyController : ControllerBase
	{
		[Authorize]
		[HttpGet("auth")]
		public ActionResult<string> GetSecret()
		{
			return "secret text";
		}

		[HttpGet("not-found")]
		public ActionResult<string> GetNotFound()
		{
			string thing = null;
			if (thing == null)
			{
				return NotFound();
			}
			return thing;
		}

		[HttpGet("server-error")]
		public ActionResult<string> GetServerError()
		{
			string thing = null;
			var thingToReturn = thing.ToString();
			return thingToReturn;
		}

		[HttpGet("bad-request")]
		public ActionResult<string> GetBadRequest()
		{
			return BadRequest("This is not a good request");
		}
	}
}
