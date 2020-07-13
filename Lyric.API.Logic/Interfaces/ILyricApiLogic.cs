using System.Threading.Tasks;
using Lyric.API.Models;

namespace Lyric.API.Logic.Interfaces
{
	public interface ILyricApiLogic
	{
		/// <summary>
		/// Get average lyric count for an artist across all of their releases
		/// </summary>
		/// <param name="artist">The artist's name</param>
		/// <returns></returns>
		Task<ArtistAverage> GetAverageLyricCount(string artist);
	}
}
