using System.Threading.Tasks;
using Lyrics.Service;
using Lyrics.Service.Interfaces;
using NUnit.Framework;

namespace LyricApi.Tests
{
	[TestFixture]
	public class LyricServiceTests
	{
		private ILyricService _lyricService;

		[SetUp]
		public void Setup()
		{
			_lyricService = new LyricService();
		}

		[TestCase(null, "Song2")]
		[TestCase("Queen", null)]
		[TestCase(null, null)]
		public async Task GetSongLyrics_ReturnsEmptyModel_IfAnyParamterIsNull(string artist, string song)
		{
			// Act
			var result = await _lyricService.GetSongLyrics(artist, song);

			// Assert
			Assert.IsNull(result.Lyrics);
		}

		[Test]
		public async Task GetSongLyrics_ReturnsEmptyModel_IfSongCannotBeFound()
		{
			// Act
			var result = await _lyricService.GetSongLyrics("Queen", "This is not a Queen song");

			// Assert
			Assert.IsNull(result.Lyrics);
		}

		[Test]
		public async Task GetSongLyrics_ReturnsLyrics()
		{
			// Act
			var result = await _lyricService.GetSongLyrics("Queen", "Don't stop me now");

			// Assert
			Assert.NotNull(result.Lyrics);
		}
	}
}
