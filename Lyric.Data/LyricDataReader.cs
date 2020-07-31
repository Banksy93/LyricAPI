using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Lyric.API.Models;
using Lyric.Data.Interfaces;
using Newtonsoft.Json;

namespace Lyric.Data
{
	public class LyricDataReader : ILyricDataReader
	{
		public async Task<ArtistAverage> GetArtistAverage(string artist)
		{
			var data = await GetArtistDataFromFile();

			if (data == null)
				return new ArtistAverage();

			return data.First(d => d.ArtistName == artist) ?? new ArtistAverage();
		}

		public string GetDataPath()
		{
			return Path.Combine(Environment.CurrentDirectory, "data.json");
		}

		public async Task<IEnumerable<ArtistAverage>> GetAllArtistsData()
		{
			var data = await GetArtistDataFromFile();

			return data ?? new List<ArtistAverage>();
		}

		private async Task<IEnumerable<ArtistAverage>> GetArtistDataFromFile()
		{
			using (var sr = new StreamReader(GetDataPath()))
			{
				var data = await sr.ReadToEndAsync();

				return JsonConvert.DeserializeObject<IEnumerable<ArtistAverage>>(data);
			}
		}
	}
}
