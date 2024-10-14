using ACM.Helpers.EmailServiceFactory;
using ACM.Models.SystemModelFactory;
using System.ComponentModel.DataAnnotations;

namespace ACM.ViewModels.CountriesViewModelFactory
{
    public class CountriesViewModel
    {
        internal AppDBContext _context;
        internal SecurityOptions _securityOptions;
        internal IEmailService _emailService;
        internal ClaimsPrincipal _user;
        internal JwtIssuerOptions _jwtOptions;
        internal string errorMessage = "";

        public Guid CountryID { get; set; }
        [Required]
        [Display(Name = "Description")]
        public string Description { get; set; }
        [Display(Name = "Country ISO Code 3 Letter")]
        public string IsoAlpha3Code { get; set; }
        [Required]
        [Display(Name = "Country ISO Code 2 Letter")]
        public string IsoAlpha2Code { get; set; }
        [Required]
        [Display(Name = "Phone Prefix;")]
        public string PhonePrefix { get; set; }
        public bool IsDefault { get; set; }
        [Required]
        [Display(Name ="ID Number Length")]
        public int IDNumberValidationLength { get; set; }


        internal async Task PopulateDetails()
        {
            if (CountryID != Guid.Empty)
            {
                var item = _context.Countries.Where(x => x.CountryID == CountryID).FirstOrDefault();
                if (item != null)
                {
                    Description = item.Description;
                    IsoAlpha2Code = item.IsoAlpha2Code;
                    IsoAlpha3Code = item.IsoAlpha3Code;
                    PhonePrefix = item.CallingCodePrefix;
                    IDNumberValidationLength = item.IDNumberValidationLength;
                    IsDefault = item.IsDefault;
                }
            }
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
                //Update all other countries to not be default
                if(IsDefault)
                {
                    var countriesUpdate = _context.Countries.ToList();
                    foreach(var country in countriesUpdate)
                    {
                        country.IsDefault = false;
                        _context.Update(country);
                    }
                    await _context.SaveChangesAsync();
                }

                var countries = _context.Countries.Where(x => x.CountryID == CountryID).FirstOrDefault();
                if (countries == null)
                {
                    countries = new Country();
                    isNew = true;
                    countries.CountryID = Guid.NewGuid();
                }

                countries.Description = Description;
                countries.IsoAlpha2Code = IsoAlpha2Code;
                countries.IsoAlpha3Code = IsoAlpha3Code;
                countries.CallingCodePrefix = PhonePrefix;
                countries.IDNumberValidationLength = IDNumberValidationLength;
                countries.IsDefault = IsDefault;

                if (isNew)
                {
                    _context.Add(countries);
                }
                else
                {
                    _context.Update(countries);
                }

                await _context.SaveChangesAsync();
                CountryID = countries.CountryID;
            }

            return CountryID;
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

            var Countries = _context.Countries.FirstOrDefault(x => x.CountryID == CountryID);

            if (Countries != null)
            {
                _context.Countries.Remove(Countries);

                await _context.SaveChangesAsync();
            }
        }
    }

    public class CountriesListViewModel
    {
        internal AppDBContext _context;
        internal SecurityOptions _securityOptions;
        internal IEmailService _emailService;

        public string SearchValue { get; set; }
        public PaginationViewModel Pagination { get; set; }

        public List<CountriesViewModel> CountriesList { get; set; }

        internal async Task PopulateLists()
        {
            if (Pagination == null)
            {
                Pagination = new PaginationViewModel();
                Pagination.Top = 10;
            }

            var list = (from t in _context.Countries
                        where (!string.IsNullOrEmpty(SearchValue) && (t.Description.Contains(SearchValue)) || string.IsNullOrEmpty(SearchValue))
                        orderby t.Description
                        select new CountriesViewModel
                        {
                            Description = t.Description,
                            CountryID = t.CountryID,
                            IsoAlpha2Code = t.IsoAlpha2Code,
                            IsoAlpha3Code = t.IsoAlpha3Code,
                            PhonePrefix = t.CallingCodePrefix,
                            IDNumberValidationLength = t.IDNumberValidationLength,
                            IsDefault = t.IsDefault
                        }) ;
            

            Pagination.TotalRecords = list.Count();
            if (!string.IsNullOrEmpty(Pagination.SortBy))
            {
                list = list.OrderByName(Pagination.SortBy, Pagination.Descending);
            }
            CountriesList = list.Skip(Pagination.Skip).Take(Pagination.Top).ToList();
        }
    }
}
