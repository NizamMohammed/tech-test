using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using static CDRApi.Models.Aurora;

// Controllers
namespace CDRApi.Controllers
{

    // Partial class
    public partial class HomeController : Controller
 
    {
        CultureInfo culture = new CultureInfo(Configuration.GetValue<string>("Culture"));
        string strInvalidDateFormat = "Request is in invalid format, ";
        string strNoRecord = "No calls found in this date range";
        string strDBName = Configuration.GetValue<string>("Databases:DB:dbname");
        string strFields = CDR.caller_id.Name + "," + CDR.recipient.Name + "," + CDR.call_date.Name + "," + CDR.end_time.Name + "," + CDR.duration.Name + "," + CDR.cost.Name + "," + CDR.currency.Name;

        [Route("api/loadcdr")]
        [Route("Home/api/loadcdr")]
        public async Task<IActionResult> cdrload()

        {
            try
            {
                string strUserName = Configuration.GetValue<string>("Databases:DB:username");
                string strPassword = Configuration.GetValue<string>("Databases:DB:password");
                string connstr = Configuration.GetValue<string>("Databases:DB:connectionstring");
                connstr = connstr.Replace("{uid}", strUserName).Replace("{pwd}", strPassword);
                string filepath = UploadPath(true); //Configuration.GetValue<string>("FolderPath");
                string  strclearcdrtable = Configuration.GetValue<string>("ClearCDRTableBeforeLoading"); 
                if (strclearcdrtable.ToUpper() == "YES")
                {
                    Execute("Truncate table cdr");
                }
                
                DirectoryInfo d = new DirectoryInfo(filepath);
               
                bool flag = !Directory.EnumerateFileSystemEntries(filepath).Any();
                if (!flag)
                    foreach (var file in d.GetFiles("*.csv"))
                    {
                        

                        var lines = System.IO.File.ReadAllLines(file.FullName);
                        if (lines.Count() == 0) RedirectToAction("cdrlist");
                        var columns = lines[0].Split(',');
                        var table = new DataTable();
                        table.Columns.Add("caller_id", typeof(string));
                        table.Columns.Add("recipient", typeof(string));
                        table.Columns.Add("call_date", typeof(string));
                        table.Columns.Add("end_time", typeof(string));
                        table.Columns.Add("duration", typeof(string));
                        table.Columns.Add("cost", typeof(string));
                        table.Columns.Add("reference", typeof(string));
                        table.Columns.Add("currency", typeof(string));


                        // foreach (var c in columns)

                        //     table.Columns.Add(c);

                        for (int i = 1; i < lines.Count() - 1; i++)
                            table.Rows.Add(lines[i].Split(','));


                        var sqlBulk = new SqlBulkCopy(connstr);
                        sqlBulk.DestinationTableName = "CDR";
                        sqlBulk.BatchSize = 10000;
                        await sqlBulk.WriteToServerAsync(table);

                    }

                string ProcessedFileFolder = filepath + @"\Processed";
                // If directory does not exist, don't even try 
                if (!Directory.Exists(ProcessedFileFolder))
                {
                    Directory.CreateDirectory(ProcessedFileFolder);
                }

                string[] sourcefiles = Directory.GetFiles(filepath);

                foreach (string sourcefile in sourcefiles)
                {
                    string fileName = Path.GetFileName(sourcefile);
                    string destFile = Path.Combine(ProcessedFileFolder, fileName);

                    System.IO.File.Move(sourcefile, destFile);
                }

                return Json(new { status = "CDRs are inserted into the database" });
            }
            catch (Exception ex)
            {
                var custmessage = "";

                if (ex.Message.Contains("Input array is longer"))
                {
                    custmessage = ex.Message + " (Please check the file folder and the files for cdr format non-compliance)";
                    Logger.LogError(ex.Message + custmessage + ex.StackTrace);
                    return Json(custmessage);
                }
                Logger.LogError(ex.Message + ex.StackTrace);
                return Json(ex.Message);
            }

        }
        
        [Route("api/topcost/{dblNumberOfcalls}/{strStartDate}/{strEndDate}/{strPhonenumber?}")]
        [Route("api/topcall/{dblNumberOfcalls}/{strStartDate}/{strEndDate}/{strPhonenumber?}")]
        public async Task<string> TopCost(double dblNumberOfcalls, string strStartDate, string strEndDate, string strPhonenumber = "")

        {
            try
            {
                if (ChangeDateFormatToDBCulture(strStartDate) != null && ChangeDateFormatToDBCulture(strEndDate) != null)
                {
                    string result = await ExecuteJsonAsync("SELECT top " + dblNumberOfcalls + " (convert(float," + CDR.cost.Name + ")) as Costs," + strFields + " FROM [" + strDBName + "].[dbo].[CDR]where convert(datetime, convert(varchar(30), call_date), 103) between '"
                        + ChangeDateFormatToDBCulture(strStartDate) + "' AND '" + ChangeDateFormatToDBCulture(strEndDate) + "' AND caller_id like '%" + strPhonenumber + "%'  order by cost desc");
                    if (result != "[]")
                    {
                        return result;
                    }
                    return strNoRecord;
                }
                else
                {
                    return strInvalidDateFormat + (" (Format : api/topcost/100/dd-mm-yyyy / dd-mm-yyyy / Phonenumber)");
                }

            }
            catch (Exception ex)
            {
                Logger.LogError(ex.Message + ex.StackTrace);
                return ex.Message;
            }

        }

        [Route("api/topdur/{dblNumberOfcalls}/{strStartDate}/{strEndDate}/{strPhonenumber?}")]
        public async Task<string> TopDuration(double dblNumberOfcalls, string strStartDate, string strEndDate, string strPhonenumber = "")
        {
            try
            {
                if (ChangeDateFormatToDBCulture(strStartDate) != null && ChangeDateFormatToDBCulture(strEndDate) != null)
                {
                    TimeSpan NumberofDays = Convert.ToDateTime(ChangeDateFormatToDBCulture(strEndDate)).AddDays(1) - Convert.ToDateTime(ChangeDateFormatToDBCulture(strStartDate));
                    string result = await ExecuteJsonAsync("SELECT top " + dblNumberOfcalls + " (convert(bigint,duration)) as Dur," + strFields + " FROM [" + strDBName + "].[dbo].[CDR]where convert(datetime, convert(varchar(30), call_date), 103) between '"
                        + ChangeDateFormatToDBCulture(strStartDate) + "' AND '" + ChangeDateFormatToDBCulture(strEndDate) + "' AND caller_id like '%" + strPhonenumber + "%'  order by duration desc");
                    if (result !="[]")
                    {
                        return result;
                    }
                    return strNoRecord;
                }
                else
                {
                    return strInvalidDateFormat + (" (Format : api/topdur/100/dd-mm-yyyy / dd-mm-yyyy / Phonenumber)");
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.Message + ex.StackTrace);
                return ex.Message;
            }
        }

        [Route("api/list/{strStartDate}/{strEndDate}/{strPhonenumber?}")]
        public async Task<string> CallList(string strStartDate, string strEndDate, string strPhonenumber = "")

        {
            try
            {
                if (ChangeDateFormatToDBCulture(strStartDate) != null && ChangeDateFormatToDBCulture(strEndDate) != null)

                {
                    string sql = "Select " + strFields + " FROM [" + strDBName + "].[dbo].[CDR]where convert(datetime, convert(varchar(30), call_date), 103) between '"
                        + ChangeDateFormatToDBCulture(strStartDate) + "' AND '" + ChangeDateFormatToDBCulture(strEndDate)
                        + "' AND caller_id like '%" + strPhonenumber + "%'  order by caller_id, call_date,end_time";
                    string result = await ExecuteJsonAsync(sql);
                    if (!result.Contains("[]"))
                    {
                        return result;
                    }
                    return strNoRecord;
                }
                else
                {
                    return strInvalidDateFormat + (" (Format : api/list/dd-mm-yyyy / dd-mm-yyyy / Phonenumber)");
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.Message + ex.StackTrace);
                if (ex.Message.Contains("timeout period"))
                return ex.Message + " (Too much data requested, Please change to a lower range)";
                return ex.Message;
            }
        }

        [Route("api/avgcost/{strStartDate}/{strEndDate}/{strPhonenumber?}")]
        public async Task<string> AvgCost(string strStartDate, string strEndDate, string strPhonenumber = "")
        {
            try
            {
                if (ChangeDateFormatToDBCulture(strStartDate) != null && ChangeDateFormatToDBCulture(strEndDate) != null)

                {
                    string result = await ExecuteJsonAsync("SELECT AVG(convert(money,cost))as [Avg Call Cost per day is] FROM [" + strDBName + "].[dbo].cdr where convert(datetime, convert(varchar(30), call_date), 103) between '"
                        + ChangeDateFormatToDBCulture(strStartDate) + "' AND '" + ChangeDateFormatToDBCulture(strEndDate) + "' AND caller_id like '%" + strPhonenumber + "%'");

                    if (!result.Contains("null"))
                    {
                        return result + " for the period between " 
                            + strStartDate + " And " + strEndDate;
                    }
                    return strNoRecord;
                }
                else
                {
                    return strInvalidDateFormat + (" (Format : api/avgcost/dd-mm-yyyy / dd-mm-yyyy / Phonenumber)");
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.Message + ex.StackTrace);
                return ex.Message;
            }
        }

        [Route("api/avgcall/{strStartDate}/{strEndDate}/{strPhonenumber?}")]
        public async Task<string> AvgCall(string strStartDate, string strEndDate, string strPhonenumber = "")

        {
            try
            {             
                if (ChangeDateFormatToDBCulture(strStartDate) != null && ChangeDateFormatToDBCulture(strEndDate) != null)
                {
                    TimeSpan NumberofDays = Convert.ToDateTime(ChangeDateFormatToDBCulture(strEndDate)).AddDays(1) - Convert.ToDateTime(ChangeDateFormatToDBCulture(strStartDate));
                    string result = await ExecuteJsonAsync("SELECT (count(*)/" + NumberofDays.Days + ") as [Avg number of Calls per day] FROM [" + strDBName + "].[dbo].cdr where convert(datetime, convert(varchar(30), call_date), 103) between '"
                        + ChangeDateFormatToDBCulture(strStartDate) + "' AND '" + ChangeDateFormatToDBCulture(strEndDate) + "' AND caller_id like '%" + strPhonenumber + "%'");

                    if (!result.Contains("day\":0"))
                    {
                        return result + " for the period between " + strStartDate + " And " + strEndDate;
                    }
                    return strNoRecord;
                }
                else
                {
                    return strInvalidDateFormat + (" (Format : api/avgcall/dd-mm-yyyy / dd-mm-yyyy / Phonenumber)");
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.Message + ex.StackTrace);
                return ex.Message;
            }

        }

        [Route("api/cost/{strStartDate}/{strEndDate}/{strPhonenumber?}")]
        public async Task<string> TotalCost(string strStartDate, string strEndDate, string strPhonenumber = "")
        {
            try
            {
                if (ChangeDateFormatToDBCulture(strStartDate) != null && ChangeDateFormatToDBCulture(strEndDate) != null)
                {
                    
                    string result = await ExecuteJsonAsync("SELECT sum(cast(cost as money)) as [Total Cost] FROM [" + strDBName + "].[dbo].cdr where convert(datetime, convert(varchar(30), call_date), 103) between '"
                        + ChangeDateFormatToDBCulture(strStartDate) + "' AND '" + ChangeDateFormatToDBCulture(strEndDate) + "' AND caller_id like '%" + strPhonenumber + "%'" );

                    if (!result.Contains("null"))
                    {
                        return result + " for the period between " + (strStartDate) + " And " + (strEndDate);
                    }
                    return strNoRecord;
                }
                else
                {
                    return strInvalidDateFormat + (" (Format : api/cost/dd-mm-yyyy / dd-mm-yyyy / Phonenumber)");
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.Message + ex.StackTrace);
                return ex.Message;
            }

        }
        
        [Route("api/delete")]
        public async Task<IActionResult> Delete()
        {
            try
            {
                int result = await ExecuteAsync("Truncate table dbo.cdr", "DB");      
                        
                    if (result == -1)
                    {
                        return Json("CDR table is cleared");
                    }
                        return Json("Delete Unsuccess");

            }
            catch (Exception ex)
            {
                Logger.LogError(ex.Message + ex.StackTrace);
                return Json(ex.Message);
            }
        }
    
    }

   
}