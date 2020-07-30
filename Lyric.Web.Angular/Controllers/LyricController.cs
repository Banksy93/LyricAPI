using System.Threading.Tasks;
using Lyric.API.Logic.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Lyric.Web.Angular.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class LyricController : ControllerBase
	{
		private readonly ILyricApiLogic _lyricApiLogic;

		public LyricController(ILyricApiLogic lyricApiLogic)
		{
			_lyricApiLogic = lyricApiLogic;
		}

		[HttpGet]
		[Route("{artist}")]
		public async Task<IActionResult> ArtistData([FromRoute] string artist)
		{
			return Ok(await _lyricApiLogic.GetAverageLyricCount(artist));
		}
	}
}
