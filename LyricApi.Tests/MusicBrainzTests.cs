using MusicBrainz.Service;
using MusicBrainz.Service.Interfaces;
using NUnit.Framework;

namespace LyricApi.Tests
{
	[TestFixture]
	public class MusicBrainzTests
	{
		private IMusicBrainzService _musicBrainzService;

		[SetUp]
		public void Setup()
		{
			_musicBrainzService = new MusicBrainzService();
		}
	}
}
