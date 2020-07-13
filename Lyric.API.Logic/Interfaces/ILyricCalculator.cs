using System.Collections.Generic;
using Lyric.API.Models;

namespace Lyric.API.Logic.Interfaces
{
	public interface ILyricCalculator
	{
		/// <summary>
		/// Get a count of lyrics from a song
		/// </summary>
		/// <param name="songLyrics"></param>
		/// <returns></returns>
		int GetLyricCount(string songLyrics);

		/// <summary>
		/// Calculate the average, min and max values from a list of lyric counts
		/// </summary>
		/// <param name="lyricCounts"></param>
		/// <returns></returns>
		AverageDetails GetLyricCountAverageDetails(IList<int> lyricCounts);
	}
}
