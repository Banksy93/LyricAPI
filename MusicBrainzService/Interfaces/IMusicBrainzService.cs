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
	}
}
