using System.Collections.Generic;
using System.Threading.Tasks;
using Lyric.API.Logic.Interfaces;
using Lyric.API.Models;
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

		[HttpGet]
		[Route("{artistOne}/{artistTwo}")]
		public async Task<IActionResult> CompareArtists([FromRoute] string artistOne, [FromRoute]string artistTwo)
		{
			if (artistOne.Equals(artistTwo))
				return BadRequest("Please specify two different artists.");

			var model = new List<ArtistAverage>
			{
				await _lyricApiLogic.GetAverageLyricCount(artistOne),
				await _lyricApiLogic.GetAverageLyricCount(artistTwo)
			};

			return Ok(model);
		}
	}
}
