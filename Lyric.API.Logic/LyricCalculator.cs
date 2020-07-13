using System.Collections.Generic;
using Lyric.API.Logic.Interfaces;
using Lyric.API.Models;

namespace Lyric.API.Logic
{
	public class LyricCalculator : ILyricCalculator
	{
		public int GetLyricCount(string songLyrics)
		{
			throw new System.NotImplementedException();
		}

		public AverageDetails GetLyricCountAverageDetails(IList<int> lyricCounts)
		{
			throw new System.NotImplementedException();
		}
	}
}
