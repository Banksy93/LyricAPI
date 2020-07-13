using System.Threading.Tasks;
using Lyric.API.Logic.Interfaces;
using Lyric.API.Models;
using Lyrics.Service.Interfaces;
using MusicBrainz.Service.Interfaces;

namespace Lyric.API.Logic
{
	public class LyricApiLogic : ILyricApiLogic
	{
		private readonly IMusicBrainzService _musicBrainzService;
		private readonly ILyricService _lyricService;
		private readonly ILyricCalculator _lyricCalculator;

		public LyricApiLogic(IMusicBrainzService musicBrainzService, ILyricService lyricService, ILyricCalculator lyricCalculator)
		{
			_musicBrainzService = musicBrainzService;
			_lyricService = lyricService;
			_lyricCalculator = lyricCalculator;
		}

		public Task<ArtistAverage> GetAverageLyricCount(string artist)
		{
			throw new System.NotImplementedException();
		}
	}
}
