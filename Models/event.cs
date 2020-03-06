using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.StaticFiles;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;

// Models
namespace CDRApi.Models
{

	// Partial class
	public partial class Aurora {

		/// <summary>
		/// Global user code
		/// </summary>
		/// <summary>
		/// Static constructor
		/// </summary>

		static Aurora()
		{
			var provider = new FileExtensionContentTypeProvider();
			FileOptions = new StaticFileOptions()
			{
				ContentTypeProvider = provider
			};

			// ContentType Mapping event
			ContentType_Mapping(provider.Mappings);

			// Class Init event
			Class_Init();
		}

		/// <summary>
		/// Global events
		/// </summary>
		// ContentType Mapping event
		public static void ContentType_Mapping(IDictionary<string, string> mappings) {

			// Example:
			//mappings[".image"] = "image/png"; // Add new mappings
			//mappings[".rtf"] = "application/x-msdownload"; // Replace an existing mapping
			//mappings.Remove(".mp4"); // Remove MP4 videos

		}

		// Class Init event
		public static void Class_Init() {

			// Enter your code here
		}

		// Page Loading event
		public static void Page_Loading() {

			// Enter your code here
		}

		// Page Rendering event
		public static void Page_Rendering() {

			//Log("Page Rendering");
		}

		// Page Unloaded event
		public static void Page_Unloaded() {

			// Enter your code here
		}

		// Personal Data Downloading event
		public static void PersonalData_Downloading(Dictionary<string, object> row) {

			//Log("PersonalData Downloading");
		}

		// Personal Data Deleted event
		public static void PersonalData_Deleted(Dictionary<string, object> row) {

			//Log("PersonalData Deleted");
		}

		// AuditTrail Inserting event
		public static bool AuditTrail_Inserting(Dictionary<string, object> rsnew) {
			return true;
		}

		// Chart Rendered event
		//public static void Chart_Rendered(ChartJsRenderer renderer) {

			// Example:
			//var data = renderer.Data;
			//var options = renderer.Options;
			//DbChart chart = renderer.Chart;
			//if (chart.ID == "<Report>_<Chart>") { // Check chart ID
			//}

		//}

		/// <summary>
		/// DatabaseConnection class 
		/// </summary>
		public class DatabaseConnection<N, M, R, T> : DatabaseConnectionBase<N, M, R, T>
			where N : DbConnection
			where M : DbCommand
			where R : DbDataReader
		{

			// Constructor
			public DatabaseConnection(string dbid) : base(dbid)
			{
			}

			// Constructor
			public DatabaseConnection() : base()
			{
			}
		}

		/// <summary>
		/// Advanced Security class
		/// </summary>
		public class AdvancedSecurity : AdvancedSecurityBase {

			// Constructor
			public AdvancedSecurity() : base() {
			}
		}

		/// <summary>
		/// Menu class
		/// </summary>
		public class Menu : MenuBase {

			// Constructor
			public Menu(object menuId, bool isRoot = false, bool isNavbar = false, string languageFolder = null) : base(menuId, isRoot, isNavbar, languageFolder) {
			}

			// Render
			public override async Task<string> ToJson() {
				Menu_Rendering();
				return await base.ToJson();
			}
		}
	} // End Partial class
} // End namespace