// Models
namespace CDRApi.Models
{

	// Partial class
	public partial class Aurora {

		// Menu language
		public static Lang MenuLanguage;

		// Set up menus
		public static void SetupMenus() {

			// Menu Language
			if (Language != null && Language.LanguageFolder == Config.LanguageFolder)
				MenuLanguage = Language;
			else
				MenuLanguage = new Lang();

			// Navbar menu
			var topMenu = new Menu("navbar", true, true);
			TopMenu = topMenu.ToScript();

			// Sidebar menu
			var sideMenu = new Menu("menu", true, false);
			sideMenu.AddMenuItem(1, "mi_CDR", Language.MenuPhrase("1", "MenuText"), "cdrlist", -1, "", true, false, false, "", "", false);
			sideMenu.AddMenuItem(3, "mci_File_Upload", Language.MenuPhrase("3", "MenuText"), "uploadfilesadd", -1, "", true, false, true, "", "", false);
			SideMenu = sideMenu.ToScript();
		}
	} // End Partial class
} // End namespace