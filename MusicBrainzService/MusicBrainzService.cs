using System.Linq;
using MusicBrainz.Data;
using MusicBrainz.Service.Interfaces;

namespace MusicBrainz.Service
{
	public class MusicBrainzService : IMusicBrainzService
	{
		public ArtistData GetArtistData(string artist)
		{
			if (string.IsNullOrEmpty(artist))
				return null;

			var artistData = Search.Artist(artist: artist);

			if (artistData.Data == null || !artistData.Data.Any())
				return null;

			// Highest scored artist is the first one
			return artistData.Data.First();
		}
	}
}
