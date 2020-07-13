using System.Diagnostics;
using System.Threading.Tasks;
using Lyric.API.Logic.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Lyric.Web.Models;

namespace Lyric.Web.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly ILyricApiLogic _lyricApiLogic;

		public HomeController(ILogger<HomeController> logger)
		{
			_logger = logger;
		}

		public IActionResult Index()
		{
			return View();
		}

		public async Task<IActionResult> AverageLyricCount(string artist)
		{
			return string.IsNullOrEmpty(artist)
				? Error()
				: Json(await _lyricApiLogic.GetAverageLyricCount(artist));
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
