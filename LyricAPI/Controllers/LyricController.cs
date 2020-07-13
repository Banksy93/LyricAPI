using System.Threading.Tasks;
using Lyric.API.Logic.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LyricAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class LyricController : ControllerBase
	{
		private ILyricApiLogic _lyricApiLogic;

		public LyricController(ILyricApiLogic lyricApiLogic)
		{
			_lyricApiLogic = lyricApiLogic;
		}

		[HttpGet]
		[Route("{artist}")]
		public async Task<IActionResult> ArtistLyricAverage([FromRoute] string artist)
		{
			if (string.IsNullOrEmpty(artist))
				return BadRequest("Please enter an artist name.");

			var artistLyricData = await _lyricApiLogic.GetAverageLyricCount(artist);

			return Ok(artistLyricData);
		}
	}
}
