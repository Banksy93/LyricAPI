using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lyric.API.Logic.Interfaces;
using Lyric.API.Models;
using Lyric.Data.Interfaces;
using Lyrics.Service.Interfaces;
using MusicBrainz.Service.Interfaces;

namespace Lyric.API.Logic
{
	public class LyricApiLogic : ILyricApiLogic
	{
		private readonly IMusicBrainzService _musicBrainzService;
		private readonly ILyricService _lyricService;
		private readonly ILyricCalculator _lyricCalculator;
		private readonly ILyricDataReader _lyricDataReader;
		private readonly ILyricDataManager _lyricDataManager;

		public LyricApiLogic(IMusicBrainzService musicBrainzService, ILyricService lyricService, ILyricCalculator lyricCalculator,
			ILyricDataReader lyricDataReader, ILyricDataManager lyricDataManager)
		{
			_musicBrainzService = musicBrainzService;
			_lyricService = lyricService;
			_lyricCalculator = lyricCalculator;
			_lyricDataReader = lyricDataReader;
			_lyricDataManager = lyricDataManager;
		}

		public async Task<ArtistAverage> GetAverageLyricCount(string artist)
		{
			if (string.IsNullOrEmpty(artist))
				return null;

			var existingData = await _lyricDataReader.GetArtistAverage(artist);
			if (!string.IsNullOrEmpty(existingData.ArtistId))
				return existingData;

			var model = new ArtistAverage{ ArtistName = artist };

			var artistData = _musicBrainzService.GetArtistData(artist);
			if (artistData == null)
				return model;

			model.ArtistId = artistData.Id;

			var releases = _musicBrainzService.GetArtistReleases(artistData.Id, "album").ToList();
			if (!releases.Any())
				return model;

			var songs = _musicBrainzService.GetSongsByReleases(artistData.Id, releases).ToList();
			if (!songs.Any())
				return model;

			// Build a list of lyric counts for each song
			var lyricCountList = await BuildLyricCountList(artist, songs);

			model.AverageDetails = _lyricCalculator.GetLyricCountAverageDetails(lyricCountList);

			return model;
		}

		private async Task<IList<int>> BuildLyricCountList(string artist, IEnumerable<string> songs)
		{
			var list = new List<int>();

			foreach (var song in songs)
			{
				var songInfo = await _lyricService.GetSongLyrics(artist, song);

				if (string.IsNullOrEmpty(songInfo.Lyrics))
					continue;

				list.Add(_lyricCalculator.GetLyricCount(songInfo.Lyrics));
			}

			return list;
		}
	}
}
