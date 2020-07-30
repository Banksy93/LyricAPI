using System.Threading.Tasks;
using Lyric.API.Models;

namespace Lyric.Data.Interfaces
{
	public interface ILyricDataReader
	{
		/// <summary>
		/// Try and get existing data for an artist using their name
		/// </summary>
		/// <param name="artist"></param>
		/// <returns></returns>
		Task<ArtistAverage> GetArtistAverage(string artist);

		/// <summary>
		/// Get the file path for the json file storing artist data
		/// </summary>
		/// <returns></returns>
		string GetDataPath();
	}
}
