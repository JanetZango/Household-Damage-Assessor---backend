using ACM.Models.SystemModelFactory;
using Microsoft.Extensions.Localization;

namespace ACM.ViewModels
{
    public class GenderViewModel
    {
        internal AppDBContext _context;
        internal SecurityOptions _securityOptions;
        internal ClaimsPrincipal _user;
        internal JwtIssuerOptions _jwtOptions;
        internal string errorMessage = "";
        internal IStringLocalizer<Helpers.Localization.SessionStringLocalizer> _localizer;

        public Guid GenderID { get; set; }
        public string Description { get; set; }

        internal async Task PopulateDetails()
        {
            if (GenderID != Guid.Empty)
            {
                var item = _context.Genders.FirstOrDefault(x => x.GenderID == GenderID);
                if (item != null)
                {
                    GenderID = item.GenderID;
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
                var item = _context.Genders.FirstOrDefault(x => x.GenderID == GenderID);
                if (item == null)
                {
                    item = new Gender();
                    isNew = true;
                    item.GenderID = Guid.NewGuid();
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
                GenderID = item.GenderID;
            }

            return GenderID;
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

            var item = _context.Genders.FirstOrDefault(x => x.GenderID == GenderID);

            if (item != null)
            {
                _context.Genders.Remove(item);

                await _context.SaveChangesAsync();
            }
        }
    }

    public class GendersListViewModel
    {
        internal AppDBContext _context;
        internal SecurityOptions _securityOptions;

        public string SearchValue { get; set; }
        public PaginationViewModel Pagination { get; set; }

        public List<GenderViewModel> GenderList { get; set; }

        internal async Task PopulateLists()
        {
            if (Pagination == null)
            {
                Pagination = new PaginationViewModel();
                Pagination.Top = 10;
            }

            var list = (from t in _context.Genders
                        where (!string.IsNullOrEmpty(SearchValue) && (t.Description.Contains(SearchValue)) || string.IsNullOrEmpty(SearchValue))
                        orderby t.Description
                        select new GenderViewModel
                        {
                            GenderID = t.GenderID,
                            Description = t.Description,
                        });

            Pagination.TotalRecords = list.Count();
            if (!string.IsNullOrEmpty(Pagination.SortBy))
            {
                list = list.OrderByName(Pagination.SortBy, Pagination.Descending);
            }
            GenderList = list.Skip(Pagination.Skip).Take(Pagination.Top).ToList();
        }
    }
}