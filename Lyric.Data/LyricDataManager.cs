using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Lyric.API.Models;
using Lyric.Data.Interfaces;
using Newtonsoft.Json;

namespace Lyric.Data
{
	public class LyricDataManager : ILyricDataManager
	{
		private readonly ILyricDataReader _lyricDataReader;

		public LyricDataManager(ILyricDataReader lyricDataReader)
		{
			_lyricDataReader = lyricDataReader;
		}

		public async Task<bool> AddArtistData(ArtistAverage data)
		{
			if (data == null)
				return false;

			var existingData = (await _lyricDataReader.GetAllArtistsData()).ToList();

			existingData.Add(data);

			var filePath = _lyricDataReader.GetDataPath();

			var jsonData = JsonConvert.SerializeObject(existingData, Formatting.Indented);

			try
			{
				await File.WriteAllTextAsync(filePath, jsonData);

				return true;
			}
			catch (Exception ex)
			{
				//TODO: Add logging
				return false;
			}
		}
	}
}
