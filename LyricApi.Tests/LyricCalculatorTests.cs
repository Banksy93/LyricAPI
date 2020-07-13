using System.Collections.Generic;
using Lyric.API.Logic;
using Lyric.API.Logic.Interfaces;
using NUnit.Framework;

namespace LyricApi.Tests
{
	[TestFixture]
	public class LyricCalculatorTests
	{
		private ILyricCalculator _lyricCalculator;

		[SetUp]
		public void Setup()
		{
			_lyricCalculator = new LyricCalculator();
		}

		[Test]
		public void GetLyricCount_ReturnsZero_IfNoLyricsPassedIn()
		{
			// Act
			var count = _lyricCalculator.GetLyricCount(null);

			// Assert
			Assert.AreEqual(0, count);
		}

		[Test]
		public void GetLyricCount_ReturnsLyricCount()
		{
			// Arrange
			const string lyrics = "These are some test lyrics for the lyric calculator logic";

			// Act
			var count = _lyricCalculator.GetLyricCount(lyrics);

			// Assert
			Assert.AreEqual(10, count);
		}

		[Test]
		public void GetLyricCountAverageDetails_ReturnsEmptyModel_IfEmptyListPassedIn()
		{
			// Act
			var details = _lyricCalculator.GetLyricCountAverageDetails(new List<int>());

			// Assert
			Assert.AreEqual(0.0d, details.Average);
			Assert.AreEqual(0.0d, details.MinCount);
			Assert.AreEqual(0.0d, details.MaxCount);
		}

		[Test]
		public void GetLyricCountAverageDetails_Returns_AverageDetails()
		{
			// Arrange
			var lyricCounts = new List<int>
			{
				132, 75, 101, 99
			};

			// Act
			var details = _lyricCalculator.GetLyricCountAverageDetails(lyricCounts);

			// Assert
			Assert.AreEqual(101.75, details.Average);
			Assert.AreEqual(75, details.MinCount);
			Assert.AreEqual(132, details.MaxCount);
		}
	}
}
