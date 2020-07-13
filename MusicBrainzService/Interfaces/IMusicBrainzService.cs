using System.Collections.Generic;
using MusicBrainz.Data;

namespace MusicBrainz.Service.Interfaces
{
	public interface IMusicBrainzService
	{
		/// <summary>
		/// Get artist information
		/// </summary>
		/// <param name="artist">Artist's name</param>
		/// <returns></returns>
		ArtistData GetArtistData(string artist);

		/// <summary>
		/// Get a list of artist releases by type
		/// </summary>
		/// <param name="artistId"></param>
		/// <param name="primaryType">Type of release, e.g. album</param>
		/// <returns></returns>
		IEnumerable<string> GetArtistReleases(string artistId, string primaryType);
	}
}
