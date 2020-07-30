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
			using (var sr = new StreamReader(GetDataPath()))
			{
				var data = await sr.ReadToEndAsync();

				var deserializedData = JsonConvert.DeserializeObject<IEnumerable<ArtistAverage>>(data);

				if (deserializedData == null)
					return new ArtistAverage();

				return deserializedData.First(d => d.ArtistName == artist) ?? new ArtistAverage();
			}
		}

		public string GetDataPath()
		{
			return Path.Combine(Environment.CurrentDirectory, "data.json");
		}
	}
}
