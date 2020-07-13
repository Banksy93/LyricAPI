﻿using System.Collections.Generic;
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

		public IEnumerable<string> GetArtistReleases(string artistId, string primaryType)
		{
			if (string.IsNullOrEmpty(artistId) || string.IsNullOrEmpty(primaryType))
				return new List<string>();

			var releases = Search.Release(arid: artistId, primarytype: primaryType);

			if (releases.Data == null || !releases.Data.Any())
				return new List<string>();

			return releases.Data.Select(r => r.Title).Distinct();
		}

		public IEnumerable<string> GetSongsByReleases(string artistId, IList<string> releases)
		{
			throw new System.NotImplementedException();
		}
	}
}
