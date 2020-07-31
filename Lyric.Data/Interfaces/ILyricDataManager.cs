using System.Threading.Tasks;
using Lyric.API.Models;

namespace Lyric.Data.Interfaces
{
	public interface ILyricDataManager
	{
		/// <summary>
		/// Add artist data to the json file
		/// </summary>
		/// <param name="data"></param>
		/// <returns></returns>
		Task<bool> AddArtistData(ArtistAverage data);
	}
}
