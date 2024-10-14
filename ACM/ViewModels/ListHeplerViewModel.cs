using ACM.Helpers.Localization;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Localization;

namespace ACM.ViewModels
{
    public class ListHeplerViewModel
    {
        internal IStringLocalizer<SessionStringLocalizer> _localizer;
        internal AppDBContext _context { get; set; }

        public string CountryID { get; set; }
        public string ProvinceID { get; set; }
        public string GenderID { get; set; }
        public string EthnicityID { get; set; }

        public List<SelectListItem> CountriesList { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> GenderList { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> EthnicityList { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> ProvincesList { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> TimeZoneList { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> UserCultureList { get; set; } = new List<SelectListItem>();

        public void PopulateLists(bool loadAll = false)
        {
            CountriesList.Clear();
            ProvincesList.Clear();
            GenderList.Clear();
            EthnicityList.Clear();

            CountriesList = (from c in _context.Countries
                             where c.IsDefault == false
                             orderby c.Description
                             select new SelectListItem
                             {
                                 Value = c.CountryID.ToString(),
                                 Text = c.Description
                             }).ToList();

            var defaultCountry = _context.Countries.FirstOrDefault(x => x.IsDefault == true);

            if (defaultCountry != null)
            {
                CountriesList.Insert(0, new SelectListItem()
                {
                    Text = defaultCountry.Description,
                    Value = defaultCountry.CountryID.ToString()
                });
            }

            if (!string.IsNullOrEmpty(CountryID) && CountryID != Guid.Empty.ToString())
            {
                ProvincesList = (from c in _context.Provinces.Where(x => x.CountryID == Guid.Parse(CountryID))
                                 orderby c.Description
                                 select new SelectListItem
                                 {
                                     Value = c.ProvinceID.ToString(),
                                     Text = c.Description
                                 }).ToList();
            }
            else if (loadAll)
            {
                ProvincesList = (from c in _context.Provinces
                                 orderby c.Description
                                 select new SelectListItem
                                 {
                                     Value = c.ProvinceID.ToString(),
                                     Text = c.Description
                                 }).ToList();
            }

            TimeZoneList = (from t in TimeZoneInfo.GetSystemTimeZones()
                            select new SelectListItem
                            {
                                Value = t.Id,
                                Text = t.DisplayName
                            }).ToList();

            UserCultureList = (from t in _context.LanguageCultures
                               orderby t.Description
                               select new SelectListItem
                               {
                                   Value = t.LanguageCultureID.ToString(),
                                   Text = t.Description
                               }).ToList();

            GenderList = (from t in _context.Genders
                          orderby t.Description
                          select new SelectListItem
                          {
                              Value = t.GenderID.ToString(),
                              Text = t.Description
                          }).ToList();

            GenderList.Insert(0, new SelectListItem()
            {
                Text = "",
                Value = Guid.Empty.ToString()
            });

            EthnicityList = (from t in _context.Ethnicities
                             orderby t.Description
                             select new SelectListItem
                             {
                                 Value = t.EthnicityID.ToString(),
                                 Text = t.Description
                             }).ToList();

            EthnicityList.Insert(0, new SelectListItem()
            {
                Text = "",
                Value = Guid.Empty.ToString()
            });
        }
    }
}