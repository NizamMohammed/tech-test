using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

// Models
namespace CDRApi.Models
{

	// Partial class
	public partial class Aurora {

		/// <summary>
		/// File Viewer class
		/// </summary>
		public class FileViewer
		{

			// Constructor
			public FileViewer(Controller controller = null) { // DN
				if (controller != null)
					Controller = controller;
			}

			/// <summary>
			/// Output file by file name
			/// </summary>
			/// <returns>Action result</returns>
			public async Task<IActionResult> GetFile(string fn)
			{

				// Get parameters
				string sessionId = Get("session");
				sessionId = Decrypt(sessionId);
				bool resize = Get<bool>("resize");
				int width = Get<int>("width");
				int height = Get<int>("height");
				bool download = Get("download", out StringValues d) ? ConvertToBool(d) : true; // Download by default
				if (width == 0 && height == 0 && resize) {
					width = Config.ThumbnailDefaultWidth;
					height = Config.ThumbnailDefaultHeight;
				}

				// If using session (internal request), file path is always encrypted.
				// If not (external request), DO NOT support external request for file path.

				string key = Config.RandomKey + sessionId;
				fn = (UseSession) ? Decrypt(fn, key) : "";
				if (FileExists(fn)) {
					Response.Clear();
					string ext = Path.GetExtension(fn).Replace(".", "").ToLower();
					string ct = ContentType(fn);
					if (Config.ImageAllowedFileExtensions.Contains(ext, StringComparer.OrdinalIgnoreCase)) {
						if (width > 0 || height > 0)
							return Controller.File(ResizeFileToBinary(fn, ref width, ref height), ct, Path.GetFileName(fn));
						else
							return Controller.PhysicalFile(fn, ct, Path.GetFileName(fn));
					} else if (Config.DownloadAllowedFileExtensions.Contains(ext, StringComparer.OrdinalIgnoreCase)) {
						if (ext == "pdf" && Config.EmbedPdf && FileExists(fn)) // Embed Pdf // DN
							return Controller.File(await FileReadAllBytes(fn), ct); // Return File Content
						else
							return Controller.PhysicalFile(fn, ct, Path.GetFileName(fn));
					}
				}
				return JsonBoolResult.FalseResult;
			}

			/// <summary>
			/// Output file by table name, field name and primary key
			/// </summary>
			/// <returns>Action result</returns>
			public async Task<IActionResult> GetFile(string table, string field, string recordkey)
			{

				// Get parameters
				//string sessionId = Get("session");

				bool resize = Get<bool>("resize");
				int width = Get<int>("width");
				int height = Get<int>("height");
				bool download = Get("download", out StringValues d) ? ConvertToBool(d) : true; // Download by default
				if (width == 0 && height == 0 && resize) {
					width = Config.ThumbnailDefaultWidth;
					height = Config.ThumbnailDefaultHeight;
				}

				// Get table object
				string tableName = "";
				dynamic tbl = null;
				if (!Empty(table)) {
					tbl = CreateTable(table);
					tableName = tbl.Name;
				}
				if (Empty(tableName) || Empty(field) || Empty(recordkey))
					return JsonBoolResult.FalseResult;
				bool validRequest = true;

				// Reject invalid request
				if (!validRequest)
					return JsonBoolResult.FalseResult;
				return await tbl.GetFileData(field, recordkey, resize, width, height);
			}
		}
	} // End Partial class
} // End namespace