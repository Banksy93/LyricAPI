using System.Threading.Tasks;
using Lyric.API.Logic;
using Lyric.API.Logic.Interfaces;
using Lyrics.Service.Interfaces;
using Moq;
using MusicBrainz.Data;
using MusicBrainz.Service.Interfaces;
using NUnit.Framework;

namespace LyricApi.Tests
{
	[TestFixture]
	public class LyricApiLogicTests
	{
		private ILyricApiLogic _lyricApiLogic;

		private Mock<ILyricService> _lyricService;
		private Mock<IMusicBrainzService> _musicBrainzService;
		private Mock<ILyricCalculator> _lyricCalculator;

		[SetUp]
		public void Setup()
		{
			_lyricService = new Mock<ILyricService>();
			_musicBrainzService = new Mock<IMusicBrainzService>();
			_lyricCalculator = new Mock<ILyricCalculator>();

			_lyricApiLogic = new LyricApiLogic(_musicBrainzService.Object, _lyricService.Object, _lyricCalculator.Object);
		}
	}
}
