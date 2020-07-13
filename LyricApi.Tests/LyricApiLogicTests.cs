using Lyric.API.Logic;
using Lyric.API.Logic.Interfaces;
using NUnit.Framework;

namespace LyricApi.Tests
{
	[TestFixture]
	public class LyricApiLogicTests
	{
		private ILyricApiLogic _lyricApiLogic;

		[SetUp]
		public void Setup()
		{
			_lyricApiLogic = new LyricApiLogic();
		}
	}
}
