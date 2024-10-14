using ACM.Helpers.EmailServiceFactory;
using ACM.Models.SystemModelFactory;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
namespace ACM.ViewModels.ProvincesViewModelFactory
{
    public class ProvincesViewModel
    {
        internal AppDBContext _context;
        internal SecurityOptions _securityOptions;
        internal IEmailService _emailService;
        internal ClaimsPrincipal _user;
        internal JwtIssuerOptions _jwtOptions;

        internal string errorMessage = "";

        public List<SelectListItem> Countries { get; set; }

        public Guid ProvinceID { get; set; }
        public string SelectedCountry { get; set; }

        [Required]
        [Display(Name = "Description")]
        public string Description { get; set; }
        [Display(Name = "Province ISO Code")]
        public string ProvIsoCode { get; set; }

        internal async Task PopulateDetails()
        {
            if (ProvinceID != Guid.Empty)
            {
                var item = _context.Provinces.Where(x => x.ProvinceID == ProvinceID).FirstOrDefault();
                if (item != null)
                {
                    Description = item.Description;
                    ProvIsoCode = item.ProvIsoCode;
                    SelectedCountry = item.CountryID.ToString();
                }
            }
            else
            {
                SelectedCountry = Guid.Empty.ToString();
            }
        }

        internal async Task PopulateLists()
        {
            ListHeplerViewModel listHeplerViewModel = new ListHeplerViewModel()
            {
                CountryID = SelectedCountry,
                _context = _context,
            };
            listHeplerViewModel.PopulateLists();

            Countries = listHeplerViewModel.CountriesList;
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
            bool isNew = false;
            bool isValid = true;
            errorMessage = "";
            //Validate inputs
            if (isValid)
            {
                var provinces = _context.Provinces.Where(x => x.ProvinceID == ProvinceID).FirstOrDefault();
                if (provinces == null)
                {
                    provinces = new Province();
                    isNew = true;
                    provinces.ProvinceID = Guid.NewGuid();
                }
                provinces.Description = Description;
                provinces.ProvIsoCode = ProvIsoCode;
                provinces.CountryID = Guid.Parse(SelectedCountry);
                if (isNew)
                {
                    _context.Add(provinces);
                }
                else
                {
                    _context.Update(provinces);
                }
                await _context.SaveChangesAsync();
                ProvinceID = provinces.ProvinceID;
            }
            return ProvinceID;
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
            var Provinces = _context.Provinces.FirstOrDefault(x => x.ProvinceID == ProvinceID);
            if (Provinces != null)
            {
                _context.Provinces.Remove(Provinces);
                await _context.SaveChangesAsync();
            }
        }
    }
    public class ProvincesListViewModel
    {
        internal AppDBContext _context;
        internal SecurityOptions _securityOptions;
        internal IEmailService _emailService;
        public string CountryID { get; set; }
        public string SearchValue { get; set; }
        public PaginationViewModel Pagination { get; set; }
        public List<ProvinceListViewModelData> ProvincesList { get; set; }
        internal async Task PopulateLists()
        {
            if (Pagination == null)
            {
                Pagination = new PaginationViewModel();
                Pagination.Top = 10;
            }
            var list = (from t in _context.Provinces.Include(x => x.Country)
                        where (!string.IsNullOrEmpty(SearchValue) && (t.Description.Contains(SearchValue)) || string.IsNullOrEmpty(SearchValue))
                        orderby t.Description
                        select new ProvinceListViewModelData
                        {
                            Description = t.Description,
                            CountryName = (t.Country != null) ? t.Country.Description : "",
                            ProvinceID = t.ProvinceID,
                            Iso = t.ProvIsoCode,
                        });

            Pagination.TotalRecords = list.Count();
            if (!string.IsNullOrEmpty(Pagination.SortBy))
            {
                list = list.OrderByName(Pagination.SortBy, Pagination.Descending);
            }
            ProvincesList = list.Skip(Pagination.Skip).Take(Pagination.Top).ToList();
        }
    }
    public class ProvinceListViewModelData
    {
        public Guid ProvinceID { get; set; }
        public string Description { get; set; }
        public string Iso { get; set; }
        public string CountryName { get; set; }
    }
}
