using ACM.Models.ACMDataModelFactory;
using Microsoft.AspNetCore.Mvc.Rendering;
using OfficeOpenXml;
using System.Data;
using System.IO;
using System.IO.Compression;
using System.Linq.Expressions;
using System.Reflection;
using System.Security.Cryptography;

namespace ACM.Helpers
{
    public static class HtmlHelperExtensions
    {
        public static bool UserHasRole(string roleEventCode, ClaimsPrincipal user)
        {
            bool returnValue = false;

            var check = user.Identity.IsAuthenticated;
            if (check)
            {
                if (user.Claims.Count() > 0)
                {
                    if (user.Claims.Any(c => c.Type == ClaimTypes.Role && c.Value == roleEventCode))
                    {
                        returnValue = true;
                    }
                }
            }

            return returnValue;
        }

        public static bool UserHasAcmAccessRole(List<AcmRole> userAccess, string roleEventCode, ClaimsPrincipal user)
        {
            bool returnValue = false;

            var check = user.Identity.IsAuthenticated;
            if (check)
            {
                if (userAccess != null && userAccess.Count > 0)
                {
                    if (userAccess.Any(c => c.EventCode == roleEventCode))
                    {
                        returnValue = true;
                    }
                }
            }

            return returnValue;
        }

        public static string DisplayLookupText(List<SelectListItem> lookupList, string value)
        {
            if (lookupList != null && lookupList.Count > 0)
            {
                var item = lookupList.FirstOrDefault(l => l.Value.ToLower() == value.ToLower());

                return item != null
                    ? item.Text
                    : "";
            }
            else
            {
                return "";
            }
        }
        public static string CharacterLimit(string value)
        {
            if (value.Length >= 150)
            {
                value = value.Substring(0, 150) + "...";

                return value;
            }
            else
            {
                return value;
            }
        }
    }

    public static class TypeExtentions
    {
        public static DateTime ToUTCTimezone(this DateTime date, ClaimsPrincipal user)
        {
            string timezone = "";

            if (user.Claims.Any(x => x.Type == "Timezone") && !string.IsNullOrEmpty(user.Claims.Where(x => x.Type == "Timezone").First().Value))
            {
                timezone = user.Claims.Where(x => x.Type == "Timezone").First().Value.ToString();
            }
            else
            {
                timezone = "South Africa Standard Time";
            }

            TimeZoneInfo zone = TimeZoneInfo.FindSystemTimeZoneById(timezone);
            return TimeZoneInfo.ConvertTimeToUtc(date, zone);
        }

        public static DateTime ToTimezoneFromUtc(this DateTime date, ClaimsPrincipal user)
        {
            string timezone = "";

            if (user != null)
            {
                if (user.Claims.Any(x => x.Type == "Timezone") && !string.IsNullOrEmpty(user.Claims.Where(x => x.Type == "Timezone").First().Value))
                {
                    timezone = user.Claims.Where(x => x.Type == "Timezone").First().Value.ToString();
                }
                else
                {
                    timezone = "South Africa Standard Time";
                }
            }
            else
            {
                timezone = "South Africa Standard Time";
            }

            TimeZoneInfo zone = TimeZoneInfo.FindSystemTimeZoneById(timezone);
            var datetimeResult = TimeZoneInfo.ConvertTimeFromUtc(date, zone);

            return datetimeResult;
        }

        public static DateTime? ToUTCTimezone(this DateTime? date, ClaimsPrincipal user)
        {
            if (date.HasValue)
            {
                string timezone = "";

                if (user.Claims.Any(x => x.Type == "Timezone") && !string.IsNullOrEmpty(user.Claims.Where(x => x.Type == "Timezone").First().Value))
                {
                    timezone = user.Claims.Where(x => x.Type == "Timezone").First().Value.ToString();
                }
                else
                {
                    timezone = "South Africa Standard Time";
                }

                TimeZoneInfo zone = TimeZoneInfo.FindSystemTimeZoneById(timezone);
                DateTime currentDate = DateTime.SpecifyKind(date.Value, DateTimeKind.Unspecified);
                return TimeZoneInfo.ConvertTimeToUtc(currentDate, zone);
            }
            else
            {
                return date;
            }
        }

        public static DateTime? ToTimezoneFromUtc(this DateTime? date, ClaimsPrincipal user)
        {
            if (date.HasValue)
            {
                string timezone = "";

                if (user.Claims.Any(x => x.Type == "Timezone") && !string.IsNullOrEmpty(user.Claims.Where(x => x.Type == "Timezone").First().Value))
                {
                    timezone = user.Claims.Where(x => x.Type == "Timezone").First().Value.ToString();
                }
                else
                {
                    timezone = "South Africa Standard Time";
                }

                TimeZoneInfo zone = TimeZoneInfo.FindSystemTimeZoneById(timezone);
                return TimeZoneInfo.ConvertTimeFromUtc(date.Value, zone);
            }
            else
            {
                return date;
            }
        }

        public static DateTimeOffset? ToOffsetTimezone(this DateTime? date, ClaimsPrincipal user)
        {
            if (date.HasValue)
            {
                string timezone = "";

                if (user.Claims.Any(x => x.Type == "Timezone") && !string.IsNullOrEmpty(user.Claims.Where(x => x.Type == "Timezone").First().Value))
                {
                    timezone = user.Claims.Where(x => x.Type == "Timezone").First().Value.ToString();
                }
                else
                {
                    timezone = "South Africa Standard Time";
                }

                DateTimeOffset cstTime = new DateTimeOffset(date.GetValueOrDefault(),
               TimeZoneInfo.FindSystemTimeZoneById(timezone).GetUtcOffset(date.GetValueOrDefault()));


                return cstTime;
            }
            else
            {
                return date;
            }
        }

        public static DateTimeOffset ToOffsetTimezone(this DateTime date, ClaimsPrincipal user)
        {
            string timezone = "";

            if (user.Claims.Any(x => x.Type == "Timezone") && !string.IsNullOrEmpty(user.Claims.Where(x => x.Type == "Timezone").First().Value))
            {
                timezone = user.Claims.Where(x => x.Type == "Timezone").First().Value.ToString();
            }
            else
            {
                timezone = "South Africa Standard Time";
            }

            DateTimeOffset cstTime = new DateTimeOffset(date,
           TimeZoneInfo.FindSystemTimeZoneById(timezone).GetUtcOffset(date));


            return cstTime;
        }

        public static IQueryable<T> OrderByName<T>(this IQueryable<T> source, string propertyName, Boolean isDescending)
        {

            if (source == null) throw new ArgumentNullException("source");
            if (propertyName == null) throw new ArgumentNullException("propertyName");

            Type type = typeof(T);
            ParameterExpression arg = Expression.Parameter(type, "x");

            PropertyInfo pi = type.GetProperty(propertyName);
            Expression expr = Expression.Property(arg, pi);
            type = pi.PropertyType;

            Type delegateType = typeof(Func<,>).MakeGenericType(typeof(T), type);
            LambdaExpression lambda = Expression.Lambda(delegateType, expr, arg);

            String methodName = isDescending ? "OrderByDescending" : "OrderBy";
            object result = typeof(Queryable).GetMethods().Single(
                method => method.Name == methodName
                        && method.IsGenericMethodDefinition
                        && method.GetGenericArguments().Length == 2
                        && method.GetParameters().Length == 2)
                .MakeGenericMethod(typeof(T), type)
                .Invoke(null, new object[] { source, lambda });
            return (IQueryable<T>)result;
        }

        public static DataTable FromExcel(this byte[] file, bool hasHeader = true, string worksheet = "")
        {
            MemoryStream stream = new MemoryStream(file);

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (var pck = new OfficeOpenXml.ExcelPackage())
            {
                pck.Load(stream);

                ExcelWorksheet ws = null;

                if (!string.IsNullOrEmpty(worksheet))
                {
                    ws = pck.Workbook.Worksheets[worksheet];
                }
                else
                {
                    ws = pck.Workbook.Worksheets.First();
                }

                DataTable tbl = new DataTable();
                foreach (var firstRowCell in ws.Cells[1, 1, 1, ws.Dimension.End.Column])
                {
                    tbl.Columns.Add(hasHeader ? firstRowCell.Text : string.Format("Column {0}", firstRowCell.Start.Column));
                }
                var startRow = hasHeader ? 2 : 1;
                for (int rowNum = startRow; rowNum <= ws.Dimension.End.Row; rowNum++)
                {
                    var wsRow = ws.Cells[rowNum, 1, rowNum, ws.Dimension.End.Column];
                    DataRow row = tbl.Rows.Add();
                    foreach (var cell in wsRow)
                    {
                        row[cell.Start.Column - 1] = cell.Text;
                    }
                }
                return tbl;
            }
        }

        public static byte[] ToExcel(this DataTable dt)
        {
            byte[] returnValue = null;

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (var pck = new ExcelPackage())
            {
                ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Sheet1");
                ws.Cells["A1"].LoadFromDataTable(dt, true);
                ws.Cells.AutoFitColumns();

                // Add some styling
                using (ExcelRange rng = ws.Cells[1, 1, 1, dt.Columns.Count])
                {
                    rng.Style.Font.Bold = true;
                    rng.Style.Font.Color.SetColor(System.Drawing.Color.Black);
                }

                MemoryStream output = new MemoryStream();
                pck.SaveAs(output);

                returnValue = output.ToArray();
            }

            return returnValue;
        }

        public static byte[] ExtractZipArchiveEntry(this ZipArchiveEntry entry)
        {
            if (entry != null)
            {
                using (var unzippedEntryStream = entry.Open())
                {
                    using (var ms = new MemoryStream())
                    {
                        unzippedEntryStream.CopyTo(ms);
                        var unzippedArray = ms.ToArray();

                        return unzippedArray;
                    }
                }
            }
            else
            {
                return null;
            }
        }

        public static Dictionary<string, string> ToDictionary(object inputObject)
        {
            PropertyInfo[] infos = inputObject.GetType().GetProperties();

            Dictionary<string, string> dix = new Dictionary<string, string>();

            foreach (PropertyInfo info in infos)
            {
                dix.Add(info.Name, info.GetValue(inputObject, null)?.ToString() ?? "");
            }

            return dix;
        }

        public static DateTime FromUnixDate(this long unixDate)
        {
            DateTime start = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            DateTime date = start.AddMilliseconds(unixDate).ToUniversalTime();

            return date;
        }

        public static string ComputeSha256Hash(this byte[] inputBytes)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                return Convert.ToBase64String(sha256.ComputeHash(inputBytes));
            }
        }
    }
}
