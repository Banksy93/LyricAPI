using System.Threading.Tasks;
using Lyric.API.Models;

namespace Lyrics.Service.Interfaces
{
	public interface ILyricService
	{
		/// <summary>
		/// Get the lyrics of a song by a certain artist
		/// </summary>
		/// <param name="artist">Artist's name</param>
		/// <param name="song">Name of the song</param>
		/// <returns></returns>
		Task<SongLyrics> GetSongLyrics(string artist, string song);
	}
}
