using MusicBrainz.Service;
using MusicBrainz.Service.Interfaces;
using NUnit.Framework;

namespace LyricApi.Tests
{
	[TestFixture]
	public class MusicBrainzTests
	{
		private IMusicBrainzService _musicBrainzService;

		[SetUp]
		public void Setup()
		{
			_musicBrainzService = new MusicBrainzService();
		}

		[Test]
		public void GetArtistData_ReturnsNull_NoArtistPassedIn()
		{
			// Act
			var data = _musicBrainzService.GetArtistData(null);

			// Assert
			Assert.IsNull(data);
		}

		[Test]
		public void GetArtistData_ReturnsNull_ArtistNotFound()
		{
			// Act
			var data = _musicBrainzService.GetArtistData("lskjfskldjfoisejfid");

			// Assert
			Assert.IsNull(data);
		}

		[Test]
		public void GetArtistData_Returns_ArtistData()
		{
			// Act
			var data = _musicBrainzService.GetArtistData("Queen");

			// Assert
			Assert.AreEqual("Queen", data.Name);
		}
	}
}
