using ACM.Helpers.EmailServiceFactory;
using ACM.Models.SystemModelFactory;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace ACM.ViewModels.LocalizationValueViewModelFactory
{
    public class LocalizationValueViewModel
    {
        internal AppDBContext _context;
        internal SecurityOptions _securityOptions;
        internal string errorMessage = "";
        internal ClaimsPrincipal _user;

        public Guid LocalizationValueID { get; set; }
        [Display(Name = "Language Culture")]
        public string SelectedLanguageCultureID { get; set; }
        [Display(Name = "Key Name")]
        public string KeyName { get; set; }
        [Display(Name = "Value")]
        public string Value { get; set; }

        public List<SelectListItem> LanguageCultureList { get; set; }

        internal async Task PopulateModel()
        {
            UserHelperFunctions userHelper = new UserHelperFunctions()
            {
                _context = _context,
                _securityOptions = _securityOptions,
                _user = _user
            };
            userHelper.Populate();

            if (LocalizationValueID != Guid.Empty)
            {
                var item = _context.LocalizationValues.Where(x => x.LocalizationValueID == LocalizationValueID).FirstOrDefault();
                if (item != null)
                {
                    KeyName = item.KeyName;
                    SelectedLanguageCultureID = item.LanguageCultureID.ToString();
                    Value = item.Value;
                }
            }
            else
            {
                SelectedLanguageCultureID = Guid.Empty.ToString();
            }
        }

        internal async Task PopulateLists()
        {
            UserHelperFunctions userHelper = new UserHelperFunctions()
            {
                _context = _context,
                _securityOptions = _securityOptions,
                _user = _user
            };
            userHelper.Populate();

            LanguageCultureList = (from x in _context.LanguageCultures
                                   orderby x.Description
                                   select new SelectListItem
                                   {
                                       Text = x.Description,
                                       Value = x.LanguageCultureID.ToString()
                                   }).ToList();
        }

        internal async Task<Guid> Save()
        {
            UserHelperFunctions userHelper = new UserHelperFunctions()
            {
                _context = _context,
                _securityOptions = _securityOptions,
                _user = _user
            };
            userHelper.Populate();

            bool isAdd = false;
            var item = _context.LocalizationValues.Where(x => x.LocalizationValueID == LocalizationValueID).FirstOrDefault();
            if (item == null)
            {
                //Ensure no duplicates
                item = _context.LocalizationValues.Where(x => x.KeyName == KeyName && x.LanguageCultureID == Guid.Parse(SelectedLanguageCultureID)).FirstOrDefault();
                if (item == null)
                {
                    item = new LocalizationValue();
                    item.LocalizationValueID = Guid.NewGuid();
                    isAdd = true;
                }
            }

            item.KeyName = KeyName;
            item.Value = Value;
            item.LanguageCultureID = Guid.Parse(SelectedLanguageCultureID);

            if (isAdd)
            {
                _context.LocalizationValues.Add(item);
            }
            else
            {
                _context.LocalizationValues.Update(item);
            }

            _context.SaveChanges();

            return item.LocalizationValueID;
        }

        internal async Task Remove()
        {
            UserHelperFunctions userHelper = new UserHelperFunctions()
            {
                _context = _context,
                _securityOptions = _securityOptions,
                _user = _user
            };
            userHelper.Populate();

            var item = _context.LocalizationValues.Where(x => x.LocalizationValueID == LocalizationValueID).FirstOrDefault();

            _context.Remove(item);

            await _context.SaveChangesAsync();
        }
    }

    public class LocalizationValueListViewModel
    {
        internal AppDBContext _context;
        internal SecurityOptions _securityOptions;
        internal IEmailService _emailService;
        internal ClaimsPrincipal _user;
        internal string errorMessage = "";

        [Display(Name = "Search")]
        public string SearchValue { get; set; }
        [Display(Name = "Language - Culture")]
        public string SelectedLanguageCultureID { get; set; }

        public PaginationViewModel Pagination { get; set; }
        public List<LocalizationValueListViewModelData> ItemList { get; set; }
        public List<SelectListItem> LanguageCultureList { get; set; }

        internal async Task PopulateList(bool selectAll = false)
        {
            UserHelperFunctions userHelper = new UserHelperFunctions()
            {
                _context = _context,
                _securityOptions = _securityOptions,
                _user = _user
            };
            userHelper.Populate();

            if (string.IsNullOrEmpty(SelectedLanguageCultureID))
            {
                SelectedLanguageCultureID = Guid.Empty.ToString();
            }

            if (Pagination == null)
            {
                Pagination = new PaginationViewModel();
                Pagination.Top = 10;
            }

            var list = (from u in _context.LocalizationValues.Include(x => x.LanguageCulture)
                      where (!string.IsNullOrEmpty(SearchValue) && (u.KeyName.Contains(SearchValue) || u.Value.Contains(SearchValue)) || string.IsNullOrEmpty(SearchValue))
                      && ((SelectedLanguageCultureID != Guid.Empty.ToString() && u.LanguageCultureID == Guid.Parse(SelectedLanguageCultureID)) || SelectedLanguageCultureID == Guid.Empty.ToString())
                      select new LocalizationValueListViewModelData()
                      {
                          LocalizationValueID = u.LocalizationValueID,
                          KeyName = u.KeyName,
                          Value = u.Value,
                          LanguageCulture = u.LanguageCulture.Description,
                          LanguageCultureCode = u.LanguageCulture.CultureNameCode
                      });

            LanguageCultureList = (from x in _context.LanguageCultures
                                   orderby x.Description
                                   select new SelectListItem
                                   {
                                       Text = x.Description,
                                       Value = x.LanguageCultureID.ToString()
                                   }).ToList();

            LanguageCultureList.Insert(0, new SelectListItem()
            {
                Text = "All",
                Value = Guid.Empty.ToString()
            });

            Pagination.TotalRecords = list.Count();
            if (!string.IsNullOrEmpty(Pagination.SortBy))
            {
                list = list.OrderByName(Pagination.SortBy, Pagination.Descending);
            }

            if (selectAll == false)
            {
                ItemList = list.Skip(Pagination.Skip).Take(Pagination.Top).ToList();
            }
            else
            {
                ItemList = list.ToList();
            }
        }

        internal async Task<byte[]> DownloadLocalizationValueListToExcel()
        {
            //Create Data Table
            DataTable dt = new DataTable();

            dt.Columns.Add("LocalizationValueID");
            dt.Columns.Add("KeyName");
            dt.Columns.Add("Value");
            dt.Columns.Add("LanguageCulture");
            dt.Columns.Add("LanguageCultureCode");

            foreach (var item in ItemList)
            {
                DataRow dtRow = dt.NewRow();
                dtRow["LocalizationValueID"] = item.LocalizationValueID;
                dtRow["KeyName"] = item.KeyName;
                dtRow["Value"] = item.Value;
                dtRow["LanguageCulture"] = item.LanguageCulture;
                dtRow["LanguageCultureCode"] = item.LanguageCultureCode;
                dt.Rows.Add(dtRow);
            }
            return dt.ToExcel();
        }

        internal async Task<bool> UploadBulkFile(DataTable content)
        {
            bool returnValue = false;

            UserHelperFunctions userHelper = new UserHelperFunctions()
            {
                _context = _context,
                _securityOptions = _securityOptions,
                _user = _user
            };
            userHelper.Populate();

            if (content != null && content.Rows.Count > 0)
            {
                bool isValid = true;
                List<string> validationErrors = new List<string>();

                //Validate Fields
                int lineNumber = 1;
                var allTimezones = TimeZoneInfo.GetSystemTimeZones();

                var languageCultureList = _context.LanguageCultures.ToList();

                foreach (DataRow row in content.Rows)
                {
                    if (string.IsNullOrEmpty(row["KeyName"].ToString()))
                    {
                        isValid = false;
                        validationErrors.Add($"Line {lineNumber}: KeyName is invalid or empty.");
                    }
                    var keyName = row["KeyName"].ToString();

                    if (string.IsNullOrEmpty(row["Value"].ToString()))
                    {
                        isValid = false;
                        validationErrors.Add($"Line {lineNumber}: Value is invalid or empty.");
                    }
                    var value = row["Value"].ToString();

                    if (string.IsNullOrEmpty(row["LanguageCulture"].ToString()))
                    {
                        isValid = false;
                        validationErrors.Add($"Line {lineNumber}: LanguageCulture is invalid or empty.");
                    }
                    var languageCulture = row["LanguageCulture"].ToString();

                    if (string.IsNullOrEmpty(row["LanguageCultureCode"].ToString()))
                    {
                        isValid = false;
                        validationErrors.Add($"Line {lineNumber}: LanguageCultureCode is invalid or empty.");
                    }
                    var languageCultureCode = row["LanguageCultureCode"].ToString();

                    if (!languageCultureList.Any(x => x.CultureNameCode.ToLower() == languageCultureCode.ToLower()))
                    {
                        isValid = false;
                        validationErrors.Add($"Line {lineNumber}: LanguageCultureCode is invalid.");
                    }

                    lineNumber++;
                }

                if (isValid)
                {
                    foreach (DataRow row in content.Rows)
                    {
                        var keyName = row["KeyName"].ToString();
                        var value = row["Value"].ToString();
                        var languageCultureCode = row["LanguageCultureCode"].ToString();
                        var languageCulture = languageCultureList.Where(x => x.CultureNameCode.ToLower() == languageCultureCode.ToLower()).FirstOrDefault();

                        bool isAdd = false;
                        var item = _context.LocalizationValues.Where(x => x.KeyName.ToLower() == keyName.ToLower() && x.LanguageCultureID == languageCulture.LanguageCultureID).FirstOrDefault();
                        if (item == null)
                        {
                            item = new LocalizationValue();
                            item.LocalizationValueID = Guid.NewGuid();
                            isAdd = true;
                        }

                        item.KeyName = keyName;
                        item.Value = value;
                        item.LanguageCultureID = languageCulture.LanguageCultureID;

                        if (isAdd)
                        {
                            _context.LocalizationValues.Add(item);
                        }
                        else
                        {
                            _context.LocalizationValues.Update(item);
                        }
                        _context.SaveChanges();
                        returnValue = true;
                    }
                }
                else
                {
                    errorMessage = "Field validation failed - " + validationErrors.Take(10).Aggregate((a, b) => a + " " + b);
                }
            }

            return returnValue;
        }
    }

    public class LocalizationValueListViewModelData
    {
        public Guid LocalizationValueID { get; set; }
        public string KeyName { get; set; }
        public string Value { get; set; }
        public string LanguageCulture { get; set; }
        public string LanguageCultureCode { get; set; }
    }
}
