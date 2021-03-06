﻿using System.Collections.Generic;
using System.Linq;
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

		[TestCase("32483sdasdkkj2", null)]
		[TestCase(null, "Albums")]
		[TestCase(null, null)]
		public void GetArtistReleases_ReturnsEmptyList_IfAnyParameterIsNull(string artistId, string type)
		{
			// Act
			var data = _musicBrainzService.GetArtistReleases(artistId, type);

			// Assert
			Assert.IsFalse(data.Any());
		}

		[Test]
		public void GetArtistReleases_ReturnsEmptyList_WhenNoReleasesFound()
		{
			// Act
			var data = _musicBrainzService.GetArtistReleases("", "album");

			// Assert
			Assert.IsFalse(data.Any());
		}

		[Test]
		public void GetArtistReleases_ReturnsReleases()
		{
			// Act
			var data = _musicBrainzService.GetArtistReleases("0383dadf-2a4e-4d10-a46a-e9e041da8eb3", "album");

			// Assert
			Assert.IsTrue(data.Any());
		}

		[Test]
		public void GetSongsByReleases_ReturnsEmptyList_IfArtistIdIsNull()
		{
			// Act
			var data = _musicBrainzService.GetSongsByReleases(null, new List<string> {"Every Weekend", "For The Masses"});

			// Assert
			Assert.IsEmpty(data);
		}

		[Test]
		public void GetSongsByReleases_ReturnsEmptyList_IfReleasesIsEmpty()
		{
			// Act
			var data = _musicBrainzService.GetSongsByReleases("8c9200b8-8e05-41d5-836e-44a37905560e", new List<string>());

			// Assert
			Assert.IsEmpty(data);
		}

		[Test]
		public void GetSongsByReleases_ReturnsListOfSongs()
		{
			// Act
			var data = _musicBrainzService.GetSongsByReleases("8c9200b8-8e05-41d5-836e-44a37905560e", new List<string> { "Every Weekend", "For The Masses" });

			// Assert
			Assert.IsTrue(data.Any());
		}
	}
}
