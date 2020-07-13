using System.Threading.Tasks;
using Lyric.API.Models;
using Lyrics.Service.Interfaces;
using Newtonsoft.Json;
using RestSharp;

namespace Lyrics.Service
{
	public class LyricService : ILyricService
	{
		private const string _baseUrl = "https://api.lyrics.ovh/v1/";

		public async Task<SongLyrics> GetSongLyrics(string artist, string song)
		{
			if (string.IsNullOrEmpty(artist) || string.IsNullOrEmpty(song))
				return new SongLyrics();

			var request = new RestRequest(_baseUrl + artist + $"/{song}", Method.GET);
			var client = new RestClient(_baseUrl);

			var response = await client.ExecuteAsync(request);

			if (!response.IsSuccessful)
				return new SongLyrics();

			var songInfo = JsonConvert.DeserializeObject<SongLyrics>(response.Content);

			return string.IsNullOrEmpty(songInfo.Lyrics) ? new SongLyrics() : songInfo;
		}
	}
}
