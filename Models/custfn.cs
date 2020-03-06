using Microsoft.Extensions.Configuration;
using System;
using System.Globalization;

// Models
namespace CDRApi.Models
{

    // Partial class
    public partial class Aurora {

        public static string? ChangeDateFormatToDBCulture(string strInputdate)
        {

            CultureInfo culture = new CultureInfo(Configuration.GetValue<string>("Culture"));
            bool flag = DateTime.TryParseExact(strInputdate, "dd-MM-yyyy", culture, DateTimeStyles.None, out DateTime DatetimeOut);
            if (flag)
                return DatetimeOut.ToString("yyyy-MM-dd");
            return null;
        }


    } // End Partial class
} // End namespace