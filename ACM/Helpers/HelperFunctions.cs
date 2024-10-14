using ImageMagick;
using ACM.Models.SystemModelFactory;
using ACM.Models.UserModelFactory;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using ACM.SignalRHubs;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json.Converters;
using System.Globalization;
using System.Data;
using OfficeOpenXml;

namespace ACM.Helpers
{
    public static class HelperFunctions
    {
        public static string GeneratePassword(int length, bool numberOnly = false)
        {
            int maxSize = length;
            char[] chars = new char[30];
            string a;

            if (numberOnly)
            {
                a = "1234567890";
            }
            else
            {
                a = "1234567890abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ*%$#@";
            }

            chars = a.ToCharArray();
            int size = maxSize;
            byte[] data = new byte[1];

            RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider();
            crypto.GetNonZeroBytes(data);
            size = maxSize;
            data = new byte[size];
            crypto.GetNonZeroBytes(data);

            StringBuilder result = new StringBuilder(size);
            foreach (byte b in data) { result.Append(chars[b % (chars.Length)]); }

            return result.ToString();
        }

        public static string GenerateUniqueReference()
        {
            var ticks = new DateTime(2021, 11, 1).Ticks;
            var ans = DateTime.UtcNow.Ticks - ticks;
            string uniqueId = ans.ToString("x");

            return uniqueId;
        }

        public static MemoryStream ResizeImagePreportional(byte[] blobData, int maxWidth, int maxHeight, int quality)
        {
            if (blobData != null)
            {
                MemoryStream outputStream = new MemoryStream();

                using (var image = new MagickImage(blobData))
                {
                    image.Resize(maxWidth, maxHeight);
                    image.Strip();
                    image.Quality = quality;
                    image.Write(outputStream);
                }

                return outputStream;
            }
            else
            {
                return null;
            }
        }

        public static void Log(AppDBContext _context, PublicEnums.LogLevel logLevel, string originator, string message, ClaimsPrincipal user = null, Exception ex = null)
        {
            try
            {
                Guid? userID = null;

                if (user != null && user.Identity != null && user.Identities.First().IsAuthenticated == true)
                {
                    userID = Guid.Parse(user.Claims.Where(x => x.Type == "http://schemas.microsoft.com/accesscontrolservice/2010/07/claims/UserID").First().Value);
                }

                ApplicationLog logEntry = new ApplicationLog()
                {
                    ApplicationLogID = Guid.NewGuid(),
                    Exception = (ex != null) ? ex.ToString() : "",
                    Level = logLevel.ToString(),
                    LogDate = DateTime.Now,
                    LogOriginator = originator,
                    Message = message,
                    UserID = userID
                };

                _context.Add(logEntry);
                _context.SaveChanges();
            }
            catch
            {
                //Log to some other location
            }
        }

        public static async Task AddUserNotification(AppDBContext _context, Guid userID, string title, string subject, string body, PublicEnums.UserNotificationAction action, IHubContext<UIUpdateHub> _uiUpdateHub, Guid? actionID,
            bool saveContext = true)
        {
            _context.UserInAppNotifications.Add(new UserInAppNotification()
            {
                ActionCode = action.ToString(),
                ActionID = actionID,
                Body = body,
                CreatedDateTime = DateTime.UtcNow,
                CreatedUserID = userID,
                EditDateTime = DateTime.UtcNow,
                EditUserID = userID,
                IsRead = false,
                Subject = subject,
                Title = title,
                UserID = userID,
                UserInAppNotificationID = Guid.NewGuid()
            });

            if (saveContext)
            {
                _context.SaveChanges();
            }

            if (_uiUpdateHub != null)
            {
                UIUpdateHubHelper helper = new UIUpdateHubHelper()
                {
                    EventCode = PublicEnums.UIUpdateList.NOTIFICATIONS.ToString(),
                    _uiUpdateHub = _uiUpdateHub
                };

                await helper.SendUserUIUpdateNotification(userID);
            }
        }

        public static string Sha256HashText(string inputValue)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                return Convert.ToBase64String(sha256.ComputeHash(Encoding.UTF8.GetBytes(inputValue)));
            }
        }

        public static DataTable GetDataTableFromExcel(MemoryStream stream, bool hasHeader = true, string worksheet = "")
        {
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

        public static class JsonVideoConverter
        {
            public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
            {
                MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
                DateParseHandling = DateParseHandling.None,
                Converters =
                {
                    new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
                },
            };
        }

    }
}
