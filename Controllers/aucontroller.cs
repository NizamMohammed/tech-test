using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using static CDRApi.Models.Aurora;

// Controllers
namespace CDRApi.Controllers
{

	// Partial class
	[AutoValidateAntiforgeryToken]

	public partial class HomeController : Controller
	{

		private IMemoryCache _cache;

		// Constructor
		public HomeController(ILogger<HomeController> logger, IMemoryCache memoryCache)
		{
			_cache = memoryCache;
			Logger = logger;
			UseSession = true;
		}

		// menu
		[Route("ewmenu")]
		[Route("Home/ewmenu")]

		public IActionResult ewmenu() => View();

		// ewemail
		[Route("ewemail")]
		[Route("Home/ewemail")]

		public IActionResult ewemail() => View();

		// index
		[Route("")]
		[Route("index")]
		[Route("Home/index")]

		public async Task<IActionResult> index()
		{

			// Create page object
			_index = new __index(this);

			// Run the page
			return await _index.Run();
		}

		// error
		[Route("error")]
		[Route("Home/error")]

		public async Task<IActionResult> error()
		{

			// Create page object
			_error = new __error(this);

			// Run the page
			return await _error.Run();
		}

		// Dispose
		protected override void Dispose(bool disposing) {
			try {
				base.Dispose(disposing);
			} finally {
				CurrentPage?.Terminate();
			}
		}
	}
}