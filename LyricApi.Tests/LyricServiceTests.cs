using Lyrics.Service;
using Lyrics.Service.Interfaces;
using NUnit.Framework;

namespace LyricApi.Tests
{
	[TestFixture]
	public class LyricServiceTests
	{
		private ILyricService _lyricService;

		[SetUp]
		public void Setup()
		{
			_lyricService = new LyricService();
		}
	}
}
