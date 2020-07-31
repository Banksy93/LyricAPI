using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Lyric.API.Models;
using Lyric.Data;
using Lyric.Data.Interfaces;
using Moq;
using NUnit.Framework;

namespace LyricApi.Tests
{
	[TestFixture]
	public class LyricDataManagerTests
	{
		private Mock<ILyricDataReader> _lyricDataReader;

		private ILyricDataManager _lyricDataManager;

		[SetUp]
		public void Setup()
		{
			_lyricDataReader = new Mock<ILyricDataReader>();

			_lyricDataManager = new LyricDataManager(_lyricDataReader.Object);
		}

		[Test]
		public async Task AddArtistData_ReturnsFalse_NullObjectPassedIn()
		{
			// Act
			var result = await _lyricDataManager.AddArtistData(null);

			// Assert
			Assert.IsFalse(result);

			_lyricDataReader.Verify(ldr => ldr.GetAllArtistsData(), Times.Never);
			_lyricDataReader.Verify(ldr => ldr.GetDataPath(), Times.Never);
		}

		[Test]
		public async Task AddArtistData_ReturnsTrue()
		{
			// Arrange
			var artistList = new List<ArtistAverage>
			{
				new ArtistAverage
				{
					ArtistId = "23478932749da-d3q43eq2-ddsj34832",
					ArtistName = "Armin Van Buuren",
					AverageDetails = new AverageDetails
					{
						MaxCount = 232,
						MinCount = 0,
						Average = 116
					}
				}
			};

			_lyricDataReader.Setup(ld => ld.GetAllArtistsData())
				.ReturnsAsync(artistList);
			_lyricDataReader.Setup(ldr => ldr.GetDataPath())
				.Returns(Path.Combine(Environment.CurrentDirectory, "data.json"));

			// Act
			var result = await _lyricDataManager.AddArtistData(artistList.First());

			// Assert
			Assert.IsTrue(result);
		}
	}
}
