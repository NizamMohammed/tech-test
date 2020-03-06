using System.Collections.Generic;

// Models
namespace CDRApi.Models
{

	// Partial class
	public partial class Aurora {

		// Configuration
		public static partial class Config {

			//
 			 //user level settings
			//
			// User level info
			public static List<UserLevel> UserLevels = new List<UserLevel> {
			};

			// User level priv info
			public static List<UserLevelPermission> UserLevelPermissions = new List<UserLevelPermission> {
			};

			// User level table info // DN
			public static List<UserLevelTablePermission> UserLevelTablePermissions = new List<UserLevelTablePermission> {
			};
		}
	} // End Partial class
} // End namespace