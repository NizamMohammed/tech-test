using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System.Threading.Tasks;
using static CDRApi.Models.Aurora;

// Controllers
namespace CDRApi.Controllers
{

	// Partial class
	public partial class HomeController : Controller
	{

		// list
		[Route("cdrlist/{id?}")]
		[Route("Home/cdrlist/{id?}")]

		public async Task<IActionResult> cdrlist()
		{

			// Create page object
			CDR_List = new _CDR_List(this);
			CDR_List.Cache = _cache;

			// Run the page
			return await CDR_List.Run();
		}
	}
}