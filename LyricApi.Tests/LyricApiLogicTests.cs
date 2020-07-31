using System.Collections.Generic;
using System.Threading.Tasks;
using Lyric.API.Logic;
using Lyric.API.Logic.Interfaces;
using Lyric.API.Models;
using Lyric.Data.Interfaces;
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
		private Mock<ILyricDataReader> _lyricDataReader;

		[SetUp]
		public void Setup()
		{
			_lyricService = new Mock<ILyricService>();
			_musicBrainzService = new Mock<IMusicBrainzService>();
			_lyricCalculator = new Mock<ILyricCalculator>();
			_lyricDataReader = new Mock<ILyricDataReader>();

			_lyricApiLogic = new LyricApiLogic(_musicBrainzService.Object, _lyricService.Object, _lyricCalculator.Object,
				_lyricDataReader.Object);
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

			_lyricDataReader.Setup(ldr => ldr.GetArtistAverage(It.IsAny<string>()))
				.ReturnsAsync(new ArtistAverage());
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

			_lyricDataReader.Setup(ldr => ldr.GetArtistAverage(It.IsAny<string>()))
				.ReturnsAsync(new ArtistAverage());
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
		public async Task GetAverageLyricCount_ReturnsAverageDetails_AndNotReturnFromJsonFile()
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

			_lyricDataReader.Setup(ldr => ldr.GetArtistAverage(It.IsAny<string>()))
				.ReturnsAsync(new ArtistAverage());
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
			_lyricCalculator.Setup(lc => lc.GetLyricCountAverageDetails(It.IsAny<List<int>>()))
				.Returns(averageDetails);

			// Act
			var result = await _lyricApiLogic.GetAverageLyricCount("Hadouken");

			// Assert
			Assert.AreEqual(artist.Name, result.ArtistName);
			Assert.AreEqual(averageDetails.Average, result.AverageDetails.Average);
			Assert.AreEqual(averageDetails.MaxCount, result.AverageDetails.MaxCount);
			Assert.AreEqual(averageDetails.MinCount, result.AverageDetails.MinCount);
		}

		[Test]
		public async Task GetAverageLyricCount_ReturnsLyricCount_FromJsonFile()
		{
			// Arrange
			var artist = new ArtistAverage
				{
					ArtistName = "Enter Shikari",
					ArtistId = "adef43243425-fsad-er32-342f-42132sasd",
					AverageDetails = new AverageDetails
					{
						Average = 121,
						MaxCount = 154,
						MinCount = 88
					}
				};

			_lyricDataReader.Setup(ldr => ldr.GetArtistAverage(It.IsAny<string>()))
				.ReturnsAsync(artist);

			// Act
			var result = await _lyricApiLogic.GetAverageLyricCount("Enter Shikari");

			//Assert
			_musicBrainzService.Verify(mb => mb.GetArtistData(It.IsAny<string>()), Times.Never);
			_musicBrainzService.Verify(mb => mb.GetArtistReleases(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
			_musicBrainzService.Verify(mb => mb.GetSongsByReleases(It.IsAny<string>(), It.IsAny<List<string>>()),
				Times.Never);
			_lyricService.Verify(ls => ls.GetSongLyrics(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
			_lyricCalculator.Verify(lc => lc.GetLyricCount(It.IsAny<string>()), Times.Never);
			_lyricCalculator.Verify(lc => lc.GetLyricCountAverageDetails(It.IsAny<List<int>>()), Times.Never);

			Assert.AreEqual(artist.ArtistName, result.ArtistName);
			Assert.AreEqual(artist.AverageDetails.Average, result.AverageDetails.Average);
			Assert.AreEqual(artist.AverageDetails.MaxCount, result.AverageDetails.MaxCount);
			Assert.AreEqual(artist.AverageDetails.MinCount, result.AverageDetails.MinCount);
		}
	}
}
