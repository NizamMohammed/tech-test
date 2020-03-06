using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

// Models
namespace CDRApi.Models
{

	// Partial class
	public partial class Aurora {

		/// <summary>
		/// UploadFiles
		/// </summary>
		public static _UploadFiles UploadFiles {
			get => HttpData.GetOrCreate<_UploadFiles>("UploadFiles");
			set => HttpData["UploadFiles"] = value;
		}

		/// <summary>
		/// Table class for UploadFiles
		/// </summary>
		public class _UploadFiles: DbTable {

			public int RowCount = 0; // DN

			public bool UseSessionForListSql = true;

			// Column CSS classes
			public string LeftColumnClass = "col-sm-2 col-form-label ew-label";

			public string RightColumnClass = "col-sm-10";

			public string OffsetColumnClass = "col-sm-10 offset-sm-2";

			public string TableLeftColumnClass = "w-col-2";

			public readonly DbField<SqlDbType> FileName;

			public readonly DbField<SqlDbType> FileSize;

			public readonly DbField<SqlDbType> FileType;

			public readonly DbField<SqlDbType> DateUploaded;

			public readonly DbField<SqlDbType> ID;

			// Constructor
			public _UploadFiles() {

				// Language object // DN
				Language ??= new Lang();
				TableVar = "UploadFiles";
				Name = "UploadFiles";
				Type = "TABLE";

				// Update Table
				UpdateTable = "[dbo].[UploadFiles]";
				DbId = "DB"; // DN
				ExportAll = true;
				ExportPageBreakCount = 0; // Page break per every n record (PDF only)
				ExportPageOrientation = "portrait"; // Page orientation (PDF only)
				ExportPageSize = "a4"; // Page size (PDF only)
				ExportExcelPageOrientation = ""; // Page orientation (EPPlus only)
				ExportExcelPageSize = ""; // Page size (EPPlus only)
				ExportColumnWidths = new float[] {  }; // Column widths (PDF only) // DN
				DetailAdd = false; // Allow detail add
				DetailEdit = false; // Allow detail edit
				DetailView = false; // Allow detail view
				ShowMultipleDetails = false; // Show multiple details
				GridAddRowCount = 5;
				AllowAddDeleteRow = true; // Allow add/delete row
				UserIdAllowSecurity = 0; // User ID Allow
				BasicSearch = new BasicSearch(TableVar);

				// FileName
				FileName = new DbField<SqlDbType> {
					TableVar = "UploadFiles",
					TableName = "UploadFiles",
					FieldVar = "x_FileName",
					Name = "FileName",
					Expression = "[FileName]",
					BasicSearchExpression = "[FileName]",
					Type = 202,
					DbType = SqlDbType.NVarChar,
					DateTimeFormat = -1,
					VirtualExpression = "[FileName]",
					IsVirtual = false,
					ForceSelection = false,
					SelectMultiple = false,
					VirtualSearch = false,
					ViewTag = "FORMATTED TEXT",
					HtmlTag = "FILE",
					Sortable = true, // Allow sort
					UploadMaxFileSize = 2147483647,
					UploadMultiple = true,
					UploadMaxFileCount = 0,
					IsUpload = true
				};
				FileName.Init(this); // DN
				Fields.Add("FileName", FileName);

				// FileSize
				FileSize = new DbField<SqlDbType> {
					TableVar = "UploadFiles",
					TableName = "UploadFiles",
					FieldVar = "x_FileSize",
					Name = "FileSize",
					Expression = "[FileSize]",
					BasicSearchExpression = "CAST([FileSize] AS NVARCHAR)",
					Type = 20,
					DbType = SqlDbType.BigInt,
					DateTimeFormat = -1,
					VirtualExpression = "[FileSize]",
					IsVirtual = false,
					ForceSelection = false,
					SelectMultiple = false,
					VirtualSearch = false,
					ViewTag = "FORMATTED TEXT",
					HtmlTag = "TEXT",
					Sortable = true, // Allow sort
					DefaultErrorMessage = Language.Phrase("IncorrectInteger"),
					IsUpload = false
				};
				FileSize.Init(this); // DN
				Fields.Add("FileSize", FileSize);

				// FileType
				FileType = new DbField<SqlDbType> {
					TableVar = "UploadFiles",
					TableName = "UploadFiles",
					FieldVar = "x_FileType",
					Name = "FileType",
					Expression = "[FileType]",
					BasicSearchExpression = "[FileType]",
					Type = 202,
					DbType = SqlDbType.NVarChar,
					DateTimeFormat = -1,
					VirtualExpression = "[FileType]",
					IsVirtual = false,
					ForceSelection = false,
					SelectMultiple = false,
					VirtualSearch = false,
					ViewTag = "FORMATTED TEXT",
					HtmlTag = "TEXT",
					Sortable = true, // Allow sort
					IsUpload = false
				};
				FileType.Init(this); // DN
				Fields.Add("FileType", FileType);

				// DateUploaded
				DateUploaded = new DbField<SqlDbType> {
					TableVar = "UploadFiles",
					TableName = "UploadFiles",
					FieldVar = "x_DateUploaded",
					Name = "DateUploaded",
					Expression = "[DateUploaded]",
					BasicSearchExpression = CastDateFieldForLike("[DateUploaded]", 0, "DB"),
					Type = 135,
					DbType = SqlDbType.DateTime,
					DateTimeFormat = 0,
					VirtualExpression = "[DateUploaded]",
					IsVirtual = false,
					ForceSelection = false,
					SelectMultiple = false,
					VirtualSearch = false,
					ViewTag = "FORMATTED TEXT",
					HtmlTag = "TEXT",
					Sortable = true, // Allow sort
					DefaultErrorMessage = Convert.ToString(Language.Phrase("IncorrectDate")).Replace("%s", DateFormat),
					IsUpload = false
				};
				DateUploaded.Init(this); // DN
				Fields.Add("DateUploaded", DateUploaded);

				// ID
				ID = new DbField<SqlDbType> {
					TableVar = "UploadFiles",
					TableName = "UploadFiles",
					FieldVar = "x_ID",
					Name = "ID",
					Expression = "[ID]",
					BasicSearchExpression = "CAST([ID] AS NVARCHAR)",
					Type = 3,
					DbType = SqlDbType.Int,
					DateTimeFormat = -1,
					VirtualExpression = "[ID]",
					IsVirtual = false,
					ForceSelection = false,
					SelectMultiple = false,
					VirtualSearch = false,
					ViewTag = "FORMATTED TEXT",
					HtmlTag = "NO",
					IsAutoIncrement = true, // Autoincrement field
					IsPrimaryKey = true, // Primary key field
					Nullable = false, // NOT NULL field
					Sortable = false, // Allow sort
					DefaultErrorMessage = Language.Phrase("IncorrectInteger"),
					IsUpload = false
				};
				ID.Init(this); // DN
				Fields.Add("ID", ID);
			}

			// Set Field Visibility
			public override bool GetFieldVisibility(string fldname) {
				var fld = FieldByName(fldname);
				return fld.Visible; // Returns original value
			}

			// Invoke method // DN
			public object Invoke(string name, object[] parameters = null) {
				MethodInfo mi = this.GetType().GetMethod(name);
				if (mi != null) {
					if (IsAsyncMethod(mi)) {
						return InvokeAsync(mi, parameters).GetAwaiter().GetResult();
					} else {
						return mi.Invoke(this, parameters);
					}
				}
				return null;
			}

			// Invoke async method // DN
			public async Task<object> InvokeAsync(MethodInfo mi, object[] parameters = null) {
				if (mi != null) {
					dynamic awaitable = mi.Invoke(this, parameters);
					await awaitable;
					return awaitable.GetAwaiter().GetResult();
				}
				return null;
			}
			#pragma warning disable 1998

			// Invoke async method // DN
			public async Task<object> InvokeAsync(string name, object[] parameters = null) => InvokeAsync(this.GetType().GetMethod(name), parameters);
			#pragma warning restore 1998

			// Check if Invoke async method // DN
			public bool IsAsyncMethod(MethodInfo mi) {
				if (mi != null) {
					Type attType = typeof(AsyncStateMachineAttribute);
					var attrib = (AsyncStateMachineAttribute)mi.GetCustomAttribute(attType);
					return (attrib != null);
				}
				return false;
			}

			// Check if Invoke async method // DN
			public bool IsAsyncMethod(string name) => IsAsyncMethod(this.GetType().GetMethod(name));
			#pragma warning disable 618

			// Connection
			public virtual DatabaseConnectionBase<SqlConnection, SqlCommand, SqlDataReader, SqlDbType> Connection => GetConnection(DbId);
			#pragma warning restore 618

			// Set left column class (must be predefined col-*-* classes of Bootstrap grid system)
			public void SetLeftColumnClass(string columnClass) {
				Match m = Regex.Match(columnClass, @"^col\-(\w+)\-(\d+)$");
				if (m.Success) {
					LeftColumnClass = columnClass + " col-form-label ew-label";
					RightColumnClass = "col-" + m.Groups[1].Value + "-" + Convert.ToString(12 - ConvertToInt(m.Groups[2].Value));
					OffsetColumnClass = RightColumnClass + " " + columnClass.Replace("col-", "offset-");
					TableLeftColumnClass = Regex.Replace(columnClass, @"/^col-\w+-(\d+)$/", "w-col-$1"); // Change to w-col-*
				}
			}

			// Single column sort
			public void UpdateSort(DbField fld) {
				string lastSort, sortField, thisSort;
				if (CurrentOrder == fld.Name) {
					sortField = fld.Expression;
					lastSort = fld.Sort;
					if (CurrentOrderType == "ASC" || CurrentOrderType == "DESC") {
						thisSort = CurrentOrderType;
					} else {
						thisSort = (lastSort == "ASC") ? "DESC" : "ASC";
					}
					fld.Sort = thisSort;
					SessionOrderBy = sortField + " " + thisSort; // Save to Session
				} else {
					fld.Sort = "";
				}
			}

			// Table level SQL
			// FROM
			private string _sqlFrom = null;

			public string SqlFrom {
				get => _sqlFrom ?? "[dbo].[UploadFiles]";
				set => _sqlFrom = value;
			}

			// SELECT
			private string _sqlSelect = null;

			public string SqlSelect { // Select
				get => _sqlSelect ?? "SELECT * FROM " + SqlFrom;
				set => _sqlSelect = value;
			}

			// WHERE // DN
			private string _sqlWhere = null;

			public string SqlWhere {
				get {
					string where = "";
					return _sqlWhere ?? where;
				}
				set {
					_sqlWhere = value;
				}
			}

			// Group By
			private string _sqlGroupBy = null;

			public string SqlGroupBy {
				get => _sqlGroupBy ?? "";
				set => _sqlGroupBy = value;
			}

			// Having
			private string _sqlHaving = null;

			public string SqlHaving {
				get => _sqlHaving ?? "";
				set => _sqlHaving = value;
			}

			// Order By
			private string _sqlOrderBy = null;

			public string SqlOrderBy {
				get => _sqlOrderBy ?? "";
				set => _sqlOrderBy = value;
			}

			// Apply User ID filters
			public string ApplyUserIDFilters(string filter) {
				return filter;
			}

			// Check if User ID security allows view all
			public bool UserIDAllow(string id = "") {
				int allow = Config.UserIdAllow;
				return id switch {
					"add" => ((allow & 1) == 1),
					"copy" => ((allow & 1) == 1),
					"gridadd" => ((allow & 1) == 1),
					"register" => ((allow & 1) == 1),
					"addopt" => ((allow & 1) == 1),
					"edit" => ((allow & 4) == 4),
					"gridedit" => ((allow & 4) == 4),
					"update" => ((allow & 4) == 4),
					"changepwd" => ((allow & 4) == 4),
					"forgotpwd" => ((allow & 4) == 4),
					"delete" => ((allow & 2) == 2),
					"view" => ((allow & 32) == 32),
					"search" => ((allow & 64) == 64),
					_ => ((allow & 8) == 8)
				};
			}

			// Get record count by reading data reader
			public async Task<int> GetRecordCount(string sql, dynamic c = null) { // use by Lookup // DN
				try {
					var cnt = 0;
					var conn = c ?? Connection;
					using var dr = await conn.OpenDataReaderAsync(sql);
					while (await dr.ReadAsync())
						cnt++;
					return cnt;
				} catch {
					if (Config.Debug)
						throw;
					return -1;
				}
			}

			// Try to get record count by SELECT COUNT(*)
			public async Task<int> TryGetRecordCount(string sql, dynamic c = null) {
				string orderBy = OrderBy;
				var conn = c ?? Connection;
				sql = Regex.Replace(sql, @"/\*BeginOrderBy\*/[\s\S]+/\*EndOrderBy\*/", "", RegexOptions.IgnoreCase).Trim(); // Remove ORDER BY clause (MSSQL)
				if (!string.IsNullOrEmpty(orderBy) && sql.EndsWith(orderBy))
					sql = sql.Substring(0, sql.Length - orderBy.Length); // Remove ORDER BY clause
				try {
					string sqlcnt;
					if ((new List<string> { "TABLE", "VIEW", "LINKTABLE" }).Contains(Type) && sql.StartsWith(SqlSelect)) { // Handle Custom Field
						sqlcnt = "SELECT COUNT(*) FROM " + SqlFrom + sql.Substring(SqlSelect.Length);
					} else {
						sqlcnt = "SELECT COUNT(*) FROM (" + sql + ") EW_COUNT_TABLE";
					}
					return Convert.ToInt32(await conn.ExecuteScalarAsync(sqlcnt));
				} catch {
					return await GetRecordCount(sql, c);
				}
			}

			// Get SQL
			public string GetSql(string where, string orderBy = "") => BuildSelectSql(SqlSelect, SqlWhere, SqlGroupBy, SqlHaving, SqlOrderBy, where, orderBy);

			// Table SQL
			public string CurrentSql {
				get {
					string filter = CurrentFilter;
					filter = ApplyUserIDFilters(filter); // Add User ID filter
					string sort = SessionOrderBy;
					return GetSql(filter, sort);
				}
			}

			// Table SQL with List page filter
			public string ListSql {
				get {
					string sort = "";
					string select = "";
					string filter = UseSessionForListSql ? SessionWhere : "";
					AddFilter(ref filter, CurrentFilter);
					Recordset_Selecting(ref filter);
					filter = ApplyUserIDFilters(filter); // Add User ID filter
					select = SqlSelect;
					sort = UseSessionForListSql ? SessionOrderBy : "";
					return BuildSelectSql(select, SqlWhere, SqlGroupBy, SqlHaving, SqlOrderBy, filter, sort);
				}
			}

			// Get ORDER BY clause
			public string OrderBy {
				get {
					string sort = SessionOrderBy;
					return BuildSelectSql("", "", "", "", SqlOrderBy, "", sort);
				}
			}

			// Get record count based on filter (for detail record count in master table pages)
			public async Task<int> LoadRecordCount(string filter) => await TryGetRecordCount(GetSql(filter));

			// Get record count (for current List page)
			public async Task<int> ListRecordCount() => await TryGetRecordCount(ListSql);

			// Insert
			public async Task<int> InsertAsync(Dictionary<string, object> row) {
				int result;
				var r = row.Where(kvp => {
					var fld = FieldByName(kvp.Key);
					return (fld != null && !fld.IsCustom);
				}).ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
				var fields = r.Select(kvp => Fields[kvp.Key]);
				var names = String.Join(",", fields.Select(fld => fld.Expression));
				var values = String.Join(",", fields.Select(fld => SqlParameter(fld)));
				if (Empty(names))
					return -1;
				string sql = "INSERT INTO " + UpdateTable + " (" + names + ") VALUES (" + values + ")";
				using var command = Connection.GetCommand(sql);
				foreach (var (key, value) in r) {
					var fld = (DbField<SqlDbType>)Fields[key]; // DN
					try {
						command.Parameters.Add(fld.FieldVar, fld.DbType).Value = ParameterValue(fld, value);
					} catch {
						if (Config.Debug)
							throw;
					}
				}
				result = await command.ExecuteNonQueryAsync();
				if (result > 0) {

					// Get insert ID
					ID.SetDbValue(await Connection.GetLastInsertIdAsync());
					row["ID"] = ID.DbValue;
				}
				return result;
			}

			// Insert
			public int Insert(Dictionary<string, object> row) => InsertAsync(row).GetAwaiter().GetResult();

			// Update
			#pragma warning disable 168, 219

			public async Task<int> UpdateAsync(Dictionary<string, object> row, object where = null, Dictionary<string, object> rsold = null, bool curfilter = true) {
				int result;
				var rscascade = new Dictionary<string, object>();
				string whereClause = "";
				row = row.Where(kvp => {
					var fld = FieldByName(kvp.Key);
					return fld != null && !fld.IsCustom;
				}).ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
				var fields = row.Select(kvp => Fields[kvp.Key]);
				var values = String.Join(",", fields.Select(fld => fld.Expression + "=" + SqlParameter(fld)));
				if (Empty(values))
					return -1;
				string sql = "UPDATE " + UpdateTable + " SET " + values;
				string filter = curfilter ? CurrentFilter : "";
				if (IsDictionary(where))
					whereClause = ArrayToFilter((IDictionary<string, object>)where);
				else
					whereClause = (string)where;
				AddFilter(ref filter, whereClause);
				if (!Empty(filter))
					sql += " WHERE " + filter;
				using var command = Connection.GetCommand(sql);
				foreach (var (key, value) in row) {
					var fld = (DbField<SqlDbType>)Fields[key]; // DN
					try {
						command.Parameters.Add(fld.FieldVar, fld.DbType).Value = ParameterValue(fld, value);
					} catch {
						if (Config.Debug)
							throw;
					}
				}
				result = await command.ExecuteNonQueryAsync();
				return result;
			}
			#pragma warning restore 168, 219

			// Update
			public int Update(Dictionary<string, object> row, object where = null, Dictionary<string, object> rsold = null, bool curfilter = true)
				=> UpdateAsync(row, where, rsold, curfilter).GetAwaiter().GetResult();

			// Convert to parameter name for use in SQL
			public string SqlParameter(DbField fld) {
				string symbol = GetSqlParamSymbol(DbId);
				string value = symbol;
				if (symbol != "?")
					value += fld.FieldVar;
				return value;
			}

			// Convert value to object for parameter
			public object ParameterValue(DbField fld, object value) {
				if (((DbField<SqlDbType>)fld).DbType == SqlDbType.Bit) {
					return ConvertToBool(value);
				}
				return value;
			}
			#pragma warning disable 168, 1998

			// Delete
			public async Task<int> DeleteAsync(Dictionary<string, object> row, object where = null, bool curfilter = true) {
				bool delete = true;
				string whereClause = "";
				string sql = "DELETE FROM " + UpdateTable + " WHERE ";
				string filter = curfilter ? CurrentFilter : "";
				if (IsDictionary(where))
					whereClause = ArrayToFilter((IDictionary<string, object>)where);
				else
					whereClause = (string)where;
				AddFilter(ref filter, whereClause);
				if (row != null) {
					DbField fld;
					fld = FieldByName("ID");
					AddFilter(ref filter, fld.Expression + "=" + QuotedValue(row["ID"], FieldByName("ID").DataType, DbId));
				}
				if (!Empty(filter))
					sql += filter;
				else
					sql += "0=1"; // Avoid delete
				int result = -1;
				if (delete)
					result = await Connection.ExecuteAsync(sql);
				return result;
			}
			#pragma warning restore 168, 1998

			// Delete
			public int Delete(Dictionary<string, object> row, object where = null, bool curfilter = true) =>
				DeleteAsync(row, where, curfilter).GetAwaiter().GetResult();

			// Load DbValue from recordset
			public void LoadDbValues(Dictionary<string, object> row) {
				if (row == null)
					return;
				FileName.Upload.DbValue = row["FileName"];
				FileSize.SetDbValue(row["FileSize"], false);
				FileType.SetDbValue(row["FileType"], false);
				DateUploaded.SetDbValue(row["DateUploaded"], false);
				ID.SetDbValue(row["ID"], false);
			}

			public void DeleteUploadedFiles(Dictionary<string, object> row) {
				LoadDbValues(row);
				if (!Empty(row["FileName"])) {
					var oldFiles = Convert.ToString(row["FileName"]).Split(Config.MultipleUploadSeparator);
					oldFiles.ToList().ForEach(oldFile => DeleteFile(FileName.OldPhysicalUploadPath + oldFile));
				}
			}

			// Record filter WHERE clause
			private string _sqlKeyFilter => "[ID] = @ID@";
			#pragma warning disable 168

			// Get record filter
			public string GetRecordFilter(Dictionary<string, object> row = null)
			{
				string keyFilter = _sqlKeyFilter;
				object val, result;
				val = !Empty(row) && row.TryGetValue("ID", out result) ? result : null;
				val ??= !Empty(ID.OldValue) ? ID.OldValue : ID.CurrentValue; // DN
				if (!IsNumeric(val))
					return "0=1"; // Invalid key
				if (val == null)
					return "0=1"; // Invalid key
				else
					keyFilter = keyFilter.Replace("@ID@", AdjustSql(val, DbId)); // Replace key value
				return keyFilter;
			}
			#pragma warning restore 168

			// Return URL
			public string ReturnUrl {
				get {
					string name = Config.ProjectName + "_" + TableVar + "_" + Config.TableReturnUrl;

					// Get referer URL automatically
					if (!Empty(ReferUrl()) && ReferPage() != CurrentPageName() &&
						ReferPage() != "login") {// Referer not same page or login page
							Session[name] = ReferUrl(); // Save to Session
					}
					if (!Empty(Session[name])) {
						return Session.GetString(name);
					} else {
						return "uploadfileslist";
					}
				}
				set {
					Session[Config.ProjectName + "_" + TableVar + "_" + Config.TableReturnUrl] = value;
				}
			}

			// Get modal caption
			public string GetModalCaption(string pageName) {
				if (SameString(pageName, "uploadfilesview"))
					return Language.Phrase("View");
				else if (SameString(pageName, "uploadfilesedit"))
					return Language.Phrase("Edit");
				else if (SameString(pageName, "uploadfilesadd"))
					return Language.Phrase("Add");
				else
					return "";
			}

			// List URL
			public string ListUrl => "uploadfileslist";

			// View URL
			public string ViewUrl => GetViewUrl();

			// View URL
			public string GetViewUrl(string parm = "") {
				string url = "";
				if (!Empty(parm))
					url = KeyUrl("uploadfilesview", UrlParm(parm));
				else
					url = KeyUrl("uploadfilesview", UrlParm(Config.TableShowDetail + "="));
				return AddMasterUrl(url);
			}

			// Add URL
			public string AddUrl { get; set; } = "uploadfilesadd";

			// Add URL
			public string GetAddUrl(string parm = "") {
				string url = "";
				if (!Empty(parm))
					url = "uploadfilesadd?" + UrlParm(parm);
				else
					url = "uploadfilesadd";
				return AppPath(AddMasterUrl(url));
			}

			// Edit URL
			public string EditUrl => GetEditUrl();

			// Edit URL (with parameter)
			public string GetEditUrl(string parm = "") {
				string url = "";
				url = KeyUrl("uploadfilesedit", UrlParm(parm));
				return AppPath(AddMasterUrl(url)); // DN
			}

			// Inline edit URL
			public string InlineEditUrl =>
				AppPath(AddMasterUrl(KeyUrl(CurrentPageName(), UrlParm("action=edit")))); // DN

			// Copy URL
			public string CopyUrl => GetCopyUrl();

			// Copy URL
			public string GetCopyUrl(string parm = "") {
				string url = "";
				url = KeyUrl("uploadfilesadd", UrlParm(parm));
				return AppPath(AddMasterUrl(url)); // DN
			}

			// Inline copy URL
			public string InlineCopyUrl =>
				AppPath(AddMasterUrl(KeyUrl(CurrentPageName(), UrlParm("action=copy")))); // DN

			// Delete URL
			public string DeleteUrl =>
				AppPath(KeyUrl("uploadfilesdelete", UrlParm())); // DN

			// Add master URL
			public string AddMasterUrl(string url) {
				return url;
			}

			// Get primary key as JSON
			public string KeyToJson() {
				string json = "";
				json += "ID:" + ConvertToJson(ID.CurrentValue, "number", true);
				return "{" + json + "}";
			}

			// Add key value to URL
			public string KeyUrl(string url, string parm = "") { // DN
				if (!IsDBNull(ID.CurrentValue)) {
					url += "/" + ID.CurrentValue;
				} else {
					return "javascript:ew.alert(ew.language.phrase('InvalidRecord'));";
				}
				if (Empty(parm))
					return url;
				else
					return url + "?" + parm;
			}

			// Sort URL (already URL-encoded)
			public string SortUrl(DbField fld) {
				if (!Empty(CurrentAction) || !Empty(Export) ||
					(new List<int> {141, 201, 203, 128, 204, 205}).Contains(fld.Type)) { // Unsortable data type
					return "";
				} else if (fld.Sortable) {
					string urlParm = UrlParm("order=" + UrlEncode(fld.Name) + "&amp;ordertype=" + fld.ReverseSort());
					return AddMasterUrl(CurrentPageName() + "?" + urlParm);
				}
				return "";
			}
			#pragma warning disable 168

			// Get record keys
			public List<string> GetRecordKeys() {
				var result = new List<string>();
				StringValues sv;
				var keysList = new List<string>();
				if (Post("key_m[]", out sv) || Get("key_m[]", out sv)) { // DN
					keysList = sv.ToList();
				} else if (RouteValues.Count > 0 || Query.Count > 0 || Form.Count > 0) { // DN
					string key = "";
					string[] keyValues = null;
					object rv;
					if (IsApi() && RouteValues.TryGetValue("key", out object k))
						keyValues = k.ToString().Split('/');
					if (RouteValues.TryGetValue("ID", out rv)) { // ID
						key = Convert.ToString(rv);
					} else if (IsApi() && !Empty(keyValues)) {
						key = keyValues[0];
					} else {
						key = Param("ID");
					}
					keysList.Add(key);
				}

				// Check keys
				foreach (var keys in keysList) {
					if (!IsNumeric(keys)) // ID
						continue;
					result.Add(keys);
				}
				return result;
			}
			#pragma warning restore 168

			// Get filter from record keys
			public string GetFilterFromRecordKeys(bool setCurrent = true) {
				List<string> recordKeys = GetRecordKeys();
				string keyFilter = "";
				foreach (var keys in recordKeys) {
					if (!Empty(keyFilter))
						keyFilter += " OR ";
					if (setCurrent)
						ID.CurrentValue = keys;
					else
						ID.OldValue = keys;
					keyFilter += "(" + GetRecordFilter() + ")";
				}
				return keyFilter;
			}
			#pragma warning disable 618

			// Load rows based on filter // DN
			public async Task<DbDataReader> LoadRs(string filter, DatabaseConnectionBase<SqlConnection, SqlCommand, SqlDataReader, SqlDbType> conn = null) {

				// Set up filter (SQL WHERE clause) and get return SQL
				string sql = GetSql(filter);
				try {
					var dr = await (conn ?? Connection).OpenDataReaderAsync(sql);
					if (dr?.HasRows ?? false)
						return dr;
				} catch {}
				return null;
			}
			#pragma warning restore 618

			// Load row values from recordset
			public void LoadListRowValues(DbDataReader rs) {
				FileName.Upload.DbValue = rs["FileName"];
				FileSize.SetDbValue(rs["FileSize"]);
				FileType.SetDbValue(rs["FileType"]);
				DateUploaded.SetDbValue(rs["DateUploaded"]);
				ID.SetDbValue(rs["ID"]);
			}
			#pragma warning disable 1998

			// Render list row values
			public async Task RenderListRow() {

				// Call Row Rendering event
				Row_Rendering();

				// Common render codes
				// FileName
				// FileSize
				// FileType
				// DateUploaded
				// ID
				// FileName

				if (!IsDBNull(FileName.Upload.DbValue)) {
					FileName.ViewValue = FileName.Upload.DbValue;
				} else {
					FileName.ViewValue = "";
				}
				FileName.ViewCustomAttributes = "";

				// FileSize
				FileSize.ViewValue = Convert.ToString(FileSize.CurrentValue); // DN
				FileSize.ViewValue = FormatNumber(FileSize.ViewValue, 0, -2, -2, -2);
				FileSize.ViewCustomAttributes = "";

				// FileType
				FileType.ViewValue = Convert.ToString(FileType.CurrentValue); // DN
				FileType.ViewCustomAttributes = "";

				// DateUploaded
				DateUploaded.ViewValue = Convert.ToString(DateUploaded.CurrentValue); // DN
				DateUploaded.ViewValue = FormatDateTime(DateUploaded.ViewValue, 0);
				DateUploaded.ViewCustomAttributes = "";

				// ID
				ID.ViewValue = ID.CurrentValue;
				ID.ViewCustomAttributes = "";

				// FileName
				FileName.HrefValue = "";
				FileName.ExportHrefValue = FileName.UploadPath + FileName.Upload.DbValue;
				FileName.TooltipValue = "";

				// FileSize
				FileSize.HrefValue = "";
				FileSize.TooltipValue = "";

				// FileType
				FileType.HrefValue = "";
				FileType.TooltipValue = "";

				// DateUploaded
				DateUploaded.HrefValue = "";
				DateUploaded.TooltipValue = "";

				// ID
				ID.HrefValue = "";
				ID.TooltipValue = "";

				// Call Row Rendered event
				Row_Rendered();

				// Save data for Custom Template
				Rows.Add(CustomTemplateFieldValues());
			}
			#pragma warning restore 1998
			#pragma warning disable 1998

			// Render edit row values
			public async Task RenderEditRow() {

				// Call Row Rendering event
				Row_Rendering();

				// FileName
				FileName.EditAttrs["class"] = "form-control";
				if (!IsDBNull(FileName.Upload.DbValue)) {
					FileName.EditValue = FileName.Upload.DbValue;
				} else {
					FileName.EditValue = "";
				}
				if (!Empty(FileName.CurrentValue))
						FileName.Upload.FileName = Convert.ToString(FileName.CurrentValue);

				// FileSize
				FileSize.EditAttrs["class"] = "form-control";
				FileSize.EditValue = FileSize.CurrentValue; // DN
				FileSize.PlaceHolder = RemoveHtml(FileSize.Caption);

				// FileType
				FileType.EditAttrs["class"] = "form-control";
				if (!FileType.Raw)
					FileType.CurrentValue = HtmlDecode(FileType.CurrentValue);
				FileType.EditValue = FileType.CurrentValue; // DN
				FileType.PlaceHolder = RemoveHtml(FileType.Caption);

				// DateUploaded
				DateUploaded.EditAttrs["class"] = "form-control";
				DateUploaded.EditValue = FormatDateTime(DateUploaded.CurrentValue, 8); // DN
				DateUploaded.PlaceHolder = RemoveHtml(DateUploaded.Caption);

				// ID
				ID.EditAttrs["class"] = "form-control";
				ID.EditValue = ID.CurrentValue;
				ID.ViewCustomAttributes = "";

				// Call Row Rendered event
				Row_Rendered();
			}
			#pragma warning restore 1998

			// Aggregate list row values
			public void AggregateListRowValues() {
			}
			#pragma warning disable 1998

			// Aggregate list row (for rendering)
			public async Task AggregateListRow() {

				// Call Row Rendered event
				Row_Rendered();
			}
			#pragma warning restore 1998

			// Export document
			public dynamic ExportDoc;

			// Export data in HTML/CSV/Word/Excel/Email/PDF format
			public async Task ExportDocument(dynamic doc, DbDataReader dataReader, int startRec, int stopRec, string exportType = "") {
				if (dataReader == null || doc == null)
					return;
				if (!doc.ExportCustom) {

					// Write header
					doc.ExportTableHeader();
					if (doc.Horizontal) { // Horizontal format, write header
						doc.BeginExportRow();
						if (exportType == "view") {
							doc.ExportCaption(FileName);
							doc.ExportCaption(FileSize);
							doc.ExportCaption(FileType);
							doc.ExportCaption(DateUploaded);
							doc.ExportCaption(ID);
						} else {
							doc.ExportCaption(FileName);
							doc.ExportCaption(FileSize);
							doc.ExportCaption(FileType);
							doc.ExportCaption(DateUploaded);
						}
						doc.EndExportRow();
					}
				}

				// Move to first record
				// For List page only. For View page, the recordset is alreay at the start record. // DN

				int recCnt = startRec - 1;
				if (exportType != "view") {
					if (Connection.SelectOffset) {
						await dataReader.ReadAsync();
					} else {
						for (int i = 0; i < startRec; i++) // Move to the start record and use do-while loop
							await dataReader.ReadAsync();
					}
				}
				int rowcnt = 0; // DN
				do { // DN
					recCnt++;
					if (recCnt >= startRec) {
						rowcnt = recCnt - startRec + 1;

						// Page break
						if (ExportPageBreakCount > 0) {
							if (rowcnt > 1 && (rowcnt - 1) % ExportPageBreakCount == 0)
								doc.ExportPageBreak();
						}
						LoadListRowValues(dataReader);

						// Render row
						RowType = Config.RowTypeView; // Render view
						ResetAttributes();
						await RenderListRow();
						if (!doc.ExportCustom) {
							doc.BeginExportRow(rowcnt); // Allow CSS styles if enabled
							if (exportType == "view") {
								await doc.ExportField(FileName);
								await doc.ExportField(FileSize);
								await doc.ExportField(FileType);
								await doc.ExportField(DateUploaded);
								await doc.ExportField(ID);
							} else {
								await doc.ExportField(FileName);
								await doc.ExportField(FileSize);
								await doc.ExportField(FileType);
								await doc.ExportField(DateUploaded);
							}
							doc.EndExportRow(rowcnt);
						}
					}

					// Call Row Export server event
					if (doc.ExportCustom)
						CurrentPage.Row_Export(dataReader);
				} while (recCnt < stopRec && await dataReader.ReadAsync()); // DN
				if (!doc.ExportCustom)
					doc.ExportTableFooter();
			}

			// Table filter
			private string _tableFilter = null;

			public string TableFilter {
				get => _tableFilter ?? "";
				set => _tableFilter = value;
			}

			// TblBasicSearchDefault
			private string _tableBasicSearchDefault = null;

			public string TableBasicSearchDefault {
				get => _tableBasicSearchDefault ?? "";
				set => _tableBasicSearchDefault = value;
			}
			#pragma warning disable 1998

			// Get file data
			public async Task<IActionResult> GetFileData(string fldparm, string key, bool resize, int width = -1, int height = -1) {
				if (width < 0)
					width = Config.ThumbnailDefaultWidth;
				if (height < 0)
					height = Config.ThumbnailDefaultHeight;

				// Set up field names
				string fldName = "", fileNameFld = "", fileTypeFld = "";
				if (SameText(fldparm, "FileName")) {
					fldName = "FileName";
					fileNameFld = "FileName";
					fileTypeFld = "FileType";
				} else {
					return JsonBoolResult.FalseResult; // Incorrect field
				}

				// Set up key values
				var ar = key.Split(Convert.ToChar(Config.CompositeKeySeparator));
				if (ar.Length == 1) {
					ID.CurrentValue = ar[0];
				} else {
					return JsonBoolResult.FalseResult; // Incorrect key
				}

				// Set up filter (WHERE Clause)
				string filter = GetRecordFilter();
				CurrentFilter = filter;
				string sql = CurrentSql;
				using var rs = await Connection.GetDataReaderAsync(sql);
				if (rs != null && await rs.ReadAsync()) {
					var val = rs[fldName];
					if (!Empty(val)) {

						// Binary data
						DbField fld;
						if (Fields.TryGetValue(fldName, out fld) && fld.IsBlob) {
							byte[] data = (byte[])val;
							if (resize && data.Length > 0)
								ResizeBinary(ref data, ref width, ref height);
							string contentType = "";

							// Write file type
							if (!Empty(fileTypeFld) && !Empty(rs[fileTypeFld]))
								contentType = Convert.ToString(rs[fileTypeFld]);
							else
								contentType = ContentType(data);

							// Write file data
							if (data.Take(8).SequenceEqual(new byte[] {0x50, 0x4B, 0x03, 0x04, 0x14, 0x00, 0x06, 0x00}) && // Fix Office 2007 documents
								!data.TakeLast(4).SequenceEqual(new byte[] {0x00, 0x00, 0x00, 0x00}))
									data.Concat(new byte[] {0x00, 0x00, 0x00, 0x00});

							// Clear any debug message
							// Response.Clear();
							// Return file content result // DN

							if (!Empty(fileNameFld) && !Empty(rs[fileNameFld]))
								return Controller.File(data, contentType, Convert.ToString(rs[fileNameFld]));
							else
								return Controller.File(data, contentType);

						// Upload to folder
						} else {
							List<string> files;
							if (fld.UploadMultiple)
								files = Convert.ToString(val).Split(Config.MultipleUploadSeparator).ToList();
							else
								files = new List<string> { Convert.ToString(val) };
							var result = files.ToDictionary(f => f, f => FullUrl(fld.HrefPath + f));
							return new JsonBoolResult(new Dictionary<string, object> { { fld.Param, result } }, true);
						}
					}
				}
				return JsonBoolResult.FalseResult; // Incorrect key
			}
			#pragma warning restore 1998

			// Table level events
			// Recordset Selecting event
			public void Recordset_Selecting(ref string filter) {

				// Enter your code here
			}

			// Recordset Search Validated event
			public void Recordset_SearchValidated() {

				// Enter your code here
			}

			// Recordset Searching event
			public void Recordset_Searching(ref string filter) {

				// Enter your code here
			}

			// Row_Selecting event
			public void Row_Selecting(ref string filter) {

				// Enter your code here
			}

			// Row Selected event
			public void Row_Selected(Dictionary<string, object> row) {

				//Log("Row Selected");
			}

			// Row Inserting event
			public bool Row_Inserting(Dictionary<string, object> rsold, Dictionary<string, object> rsnew) {

				// Enter your code here
				// To cancel, set return value to False and error message to CancelMessage

				return true;
			}

			// Row Inserted event
			public void Row_Inserted(Dictionary<string, object> rsold, Dictionary<string, object> rsnew) {

				//Log("Row Inserted");
			}

			// Row Updating event
			public bool Row_Updating(Dictionary<string, object> rsold, Dictionary<string, object> rsnew) {

				// Enter your code here
				// To cancel, set return value to False and error message to CancelMessage

				return true;
			}

			// Row Updated event
			public void Row_Updated(Dictionary<string, object> rsold, Dictionary<string, object> rsnew) {

				//Log("Row Updated");
			}

			// Row Update Conflict event
			public bool Row_UpdateConflict(Dictionary<string, object> rsold, Dictionary<string, object> rsnew) {

				// Enter your code here
				// To ignore conflict, set return value to false

				return true;
			}

			// Recordset Deleting event
			public bool Row_Deleting(Dictionary<string, object> rs) {

				// Enter your code here
				// To cancel, set return value to False and error message to CancelMessage

				return true;
			}

			// Row Deleted event
			public void Row_Deleted(Dictionary<string, object> rs) {

				//Log("Row Deleted");
			}

			// Email Sending event
			public virtual bool Email_Sending(Email email, dynamic args) {

				//Log(email);
				return true;
			}

			// Lookup Selecting event
			public void Lookup_Selecting(DbField fld, ref string filter) {

				// Enter your code here
			}

			// Row Rendering event
			public void Row_Rendering() {

				// Enter your code here
			}

			// Row Rendered event
			public void Row_Rendered() {

				//VarDump(<FieldName>); // View field properties
			}

			// User ID Filtering event
			public void UserID_Filtering(ref string filter) {

				// Enter your code here
			}
		}
	} // End Partial class
} // End namespace