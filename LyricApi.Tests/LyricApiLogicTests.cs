using System.Collections.Generic;
using System.Threading.Tasks;
using Lyric.API.Logic;
using Lyric.API.Logic.Interfaces;
using Lyric.API.Models;
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

		[Test]
		public async Task GetAverageLyricCount_ReturnsNull_IfNoArtistPassedIn()
		{
			// Act
			var result = await _lyricApiLogic.GetAverageLyricCount(null);

			// Assert
			Assert.IsNull(result);
		}

		[Test]
		public async Task GetAverageLyricCount_DoesNotCall_GetSongs_WithNoReleases()
		{
			// Arrange
			var artist = new ArtistData
			{
				Name = "Hadouken",
				Id = "8c9200b8-8e05-41d5-836e-44a37905560e"
			};

			_musicBrainzService.Setup(mb => mb.GetArtistData(It.IsAny<string>()))
				.Returns(artist);
			_musicBrainzService.Setup(mb => mb.GetArtistReleases(It.IsAny<string>(), It.IsAny<string>()))
				.Returns(new List<string>());

			// Act
			var result = await _lyricApiLogic.GetAverageLyricCount("Hadouken");

			// Assert
			Assert.AreEqual(artist.Name, result.ArtistName);
			_musicBrainzService.Verify(mb => mb.GetSongsByReleases(It.IsAny<string>(), It.IsAny<List<string>>()),
				Times.Never);
		}

		[Test]
		public async Task GetAverageLyricCount_DoesNotCall_LyricApiService_NoSongs()
		{
			// Arrange
			var artist = new ArtistData
			{
				Name = "Hadouken",
				Id = "8c9200b8-8e05-41d5-836e-44a37905560e"
			};

			_musicBrainzService.Setup(mb => mb.GetArtistData(It.IsAny<string>()))
				.Returns(artist);
			_musicBrainzService.Setup(mb => mb.GetArtistReleases(It.IsAny<string>(), It.IsAny<string>()))
				.Returns(new List<string>
				{
					"Every Weekend",
					"For The Masses"
				});
			_musicBrainzService.Setup(mb => mb.GetSongsByReleases(It.IsAny<string>(), It.IsAny<List<string>>()))
				.Returns(new List<string>());

			// Act
			var result = await _lyricApiLogic.GetAverageLyricCount("Hadouken");

			// Assert
			Assert.AreEqual(artist.Name, result.ArtistName);
			_lyricService.Verify(ls => ls.GetSongLyrics(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
		}

		[Test]
		public async Task GetAverageLyricCount_ReturnsAverageDetails()
		{
			// Arrange
			var artist = new ArtistData
			{
				Name = "Hadouken",
				Id = "8c9200b8-8e05-41d5-836e-44a37905560e"
			};

			var averageDetails = new AverageDetails
			{
				Average = 101.75,
				MaxCount = 132,
				MinCount = 75
			};

			_musicBrainzService.Setup(mb => mb.GetArtistData(It.IsAny<string>()))
				.Returns(artist);
			_musicBrainzService.Setup(mb => mb.GetArtistReleases(It.IsAny<string>(), It.IsAny<string>()))
				.Returns(new List<string>
				{
					"Every Weekend",
					"For The Masses"
				});
			_musicBrainzService.Setup(mb => mb.GetSongsByReleases(It.IsAny<string>(), It.IsAny<List<string>>()))
				.Returns(new List<string>
				{
					"The Vortex", "Oxygen", "Turn The Lights Out"
				});
			_lyricService.Setup(ls => ls.GetSongLyrics(It.IsAny<string>(), It.IsAny<string>()))
				.ReturnsAsync(new SongLyrics {Lyrics = "Some lyrics to a song."});
			_lyricCalculator.Setup(lc => lc.GetLyricCount(It.IsAny<string>()))
				.Returns(57);

			// Act
			var result = await _lyricApiLogic.GetAverageLyricCount("Hadouken");

			// Assert
			Assert.AreEqual(artist.Name, result.ArtistName);
			Assert.AreEqual(averageDetails.Average, result.AverageDetails.Average);
			Assert.AreEqual(averageDetails.MaxCount, result.AverageDetails.MaxCount);
			Assert.AreEqual(averageDetails.MinCount, result.AverageDetails.MinCount);
		}
	}
}
