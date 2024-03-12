using ArtworkSharingPlatform.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ArtworkSharingHost.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class GenreController : ControllerBase
	{
		private readonly IGenreService _genreService;

		public GenreController(IGenreService genreService)
        {
			_genreService = genreService;
		}
		
		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			return Ok(await _genreService.GetAll());
		}
    }
}
