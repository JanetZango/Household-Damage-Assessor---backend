using ACM.Models.SystemModelFactory;
using Microsoft.Extensions.Localization;

namespace ACM.ViewModels
{
    public class EthnicityViewModel
    {
        internal AppDBContext _context;
        internal SecurityOptions _securityOptions;
        internal ClaimsPrincipal _user;
        internal JwtIssuerOptions _jwtOptions;
        internal string errorMessage = "";
        internal IStringLocalizer<Helpers.Localization.SessionStringLocalizer> _localizer;

        public Guid EthnicityID { get; set; }
        public string Description { get; set; }

        internal async Task PopulateDetails()
        {
            if (EthnicityID != Guid.Empty)
            {
                var item = _context.Ethnicities.FirstOrDefault(x => x.EthnicityID == EthnicityID);
                if (item != null)
                {
                    EthnicityID = item.EthnicityID;
                    Description = item.Description;
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
                var item = _context.Ethnicities.FirstOrDefault(x => x.EthnicityID == EthnicityID);
                if (item == null)
                {
                    item = new Ethnicity();
                    isNew = true;
                    item.EthnicityID = Guid.NewGuid();
                }

                item.Description = Description;

                if (isNew)
                {
                    _context.Add(item);
                }
                else
                {
                    _context.Update(item);
                }

                await _context.SaveChangesAsync();
                EthnicityID = item.EthnicityID;
            }

            return EthnicityID;
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

            var ethenicity = _context.Ethnicities.FirstOrDefault(x => x.EthnicityID == EthnicityID);

            if (ethenicity != null)
            {
                _context.Ethnicities.Remove(ethenicity);

                await _context.SaveChangesAsync();
            }
        }
    }

    public class EthenicityListViewModel
    {
        internal AppDBContext _context;
        internal SecurityOptions _securityOptions;

        public string SearchValue { get; set; }
        public PaginationViewModel Pagination { get; set; }

        public List<EthnicityViewModel> EthnicitiesList { get; set; }

        internal async Task PopulateLists()
        {
            if (Pagination == null)
            {
                Pagination = new PaginationViewModel();
                Pagination.Top = 10;
            }

            var list = (from t in _context.Ethnicities
                        where (!string.IsNullOrEmpty(SearchValue) && (t.Description.Contains(SearchValue)) || string.IsNullOrEmpty(SearchValue))
                        orderby t.Description
                        select new EthnicityViewModel
                        {
                            EthnicityID = t.EthnicityID,
                            Description = t.Description,
                        });

            Pagination.TotalRecords = list.Count();
            if (!string.IsNullOrEmpty(Pagination.SortBy))
            {
                list = list.OrderByName(Pagination.SortBy, Pagination.Descending);
            }
            EthnicitiesList = list.Skip(Pagination.Skip).Take(Pagination.Top).ToList();
        }
    }
}