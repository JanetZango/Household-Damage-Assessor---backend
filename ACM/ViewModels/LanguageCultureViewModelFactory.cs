using ACM.Helpers.EmailServiceFactory;
using ACM.Models.SystemModelFactory;
using System.ComponentModel.DataAnnotations;

namespace ACM.ViewModels.LanguageCultureViewModelFactory
{
    public class LanguageCultureViewModel
    {
        internal AppDBContext _context;
        internal SecurityOptions _securityOptions;
        internal string errorMessage = "";
        internal ClaimsPrincipal _user;

        public Guid LanguageCultureID { get; set; }
        [Required]
        [Display(Name = "Description")]
        public string Description { get; set; }
        [Required]
        [Display(Name = "Code")]
        public string CultureNameCode { get; set; }

        internal async Task PopulateModel()
        {
            UserHelperFunctions userHelper = new UserHelperFunctions()
            {
                _context = _context,
                _securityOptions = _securityOptions,
                _user = _user
            };
            userHelper.Populate();

            if (LanguageCultureID != Guid.Empty)
            {
                var item = _context.LanguageCultures.Where(x => x.LanguageCultureID == LanguageCultureID).FirstOrDefault();
                if (item != null)
                {
                    Description = item.Description;
                    CultureNameCode = item.CultureNameCode;
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

            bool isAdd = false;
            var item = _context.LanguageCultures.Where(x => x.LanguageCultureID == LanguageCultureID).FirstOrDefault();
            if (item == null)
            {
                //Dup check
                item = _context.LanguageCultures.FirstOrDefault(x => (x.Description.ToLower() == Description.ToLower() || x.CultureNameCode.ToLower() == CultureNameCode.ToLower()));
                if (item == null)
                {
                    item = new LanguageCulture();
                    item.LanguageCultureID = Guid.NewGuid();
                    isAdd = true;
                }
                else
                {
                    errorMessage = "The business unit with this description or Codde already exists.";
                    return Guid.Empty;
                }
            }

            item.Description = Description;
            item.CultureNameCode = CultureNameCode;

            if (isAdd)
            {
                _context.LanguageCultures.Add(item);
            }
            else
            {
                _context.LanguageCultures.Update(item);
            }

            _context.SaveChanges();

            return item.LanguageCultureID;
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

            var item = _context.LanguageCultures.Where(x => x.LanguageCultureID == LanguageCultureID).FirstOrDefault();

            _context.LanguageCultures.Remove(item);

            await _context.SaveChangesAsync();
        }
    }

    public class LanguageCultureListViewModel
    {
        internal AppDBContext _context;
        internal SecurityOptions _securityOptions;
        internal IEmailService _emailService;
        internal ClaimsPrincipal _user;

        public string SearchValue { get; set; }
        public PaginationViewModel Pagination { get; set; }

        public List<LanguageCultureViewModel> LanguageList { get; set; }

        internal void PopulateList()
        {
            UserHelperFunctions userHelper = new UserHelperFunctions()
            {
                _context = _context,
                _securityOptions = _securityOptions,
                _user = _user
            };
            userHelper.Populate();

            if (Pagination == null)
            {
                Pagination = new PaginationViewModel();
                Pagination.Top = 10;
            }

            var list = (from u in _context.LanguageCultures
                      where (!string.IsNullOrEmpty(SearchValue) && (u.Description.Contains(SearchValue)) || string.IsNullOrEmpty(SearchValue))
                      select new LanguageCultureViewModel()
                      {
                          LanguageCultureID = u.LanguageCultureID,
                          CultureNameCode = u.CultureNameCode,
                          Description = u.Description
                      });

            Pagination.TotalRecords = list.Count();
            if (!string.IsNullOrEmpty(Pagination.SortBy))
            {
                list = list.OrderByName(Pagination.SortBy, Pagination.Descending);
            }
            LanguageList = list.Skip(Pagination.Skip).Take(Pagination.Top).ToList();
        }
    }
}
