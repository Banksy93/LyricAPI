using System.Threading.Tasks;
using Lyric.API.Models;
using Lyrics.Service.Interfaces;

namespace Lyrics.Service
{
	public class LyricService : ILyricService
	{
		public async Task<SongLyrics> GetSongLyrics(string artist, string song)
		{
			throw new System.NotImplementedException();
		}
	}
}
