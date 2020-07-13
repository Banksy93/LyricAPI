using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Lyric.API.Logic.Interfaces;
using Lyric.API.Models;

namespace Lyric.API.Logic
{
	public class LyricCalculator : ILyricCalculator
	{
		public int GetLyricCount(string songLyrics)
		{
			if (string.IsNullOrEmpty(songLyrics))
				return 0;

			var escapedLyrics = Regex.Replace(songLyrics, @"\t|\n|\r", " ");

			var lyricArray = escapedLyrics.Split(' ').Where(s => s.Length > 0);

			return lyricArray.Count();
		}

		public AverageDetails GetLyricCountAverageDetails(IList<int> lyricCounts)
		{
			if (!lyricCounts.Any())
				return new AverageDetails();

			return new AverageDetails
			{
				Average = lyricCounts.Average(),
				MaxCount = lyricCounts.Max(),
				MinCount = lyricCounts.Min()
			};
		}
	}
}
