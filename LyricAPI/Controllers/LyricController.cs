using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace LyricAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class LyricController : ControllerBase
	{
		[HttpGet]
		[Route("{artist}")]
		public async Task<IActionResult> ArtistLyricAverage([FromRoute] string artist)
		{
			return Ok();
		}
	}
}
