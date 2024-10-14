using ACM.Helpers.EmailServiceFactory;
using ACM.Models.SystemModelFactory;
using System.ComponentModel.DataAnnotations;

namespace ACM.ViewModels.FAQViewModelFactory
{
    public class FAQViewModel
    {
        internal AppDBContext _context;
        internal SecurityOptions _securityOptions;
        internal IEmailService _emailService;
        internal ClaimsPrincipal _user;
        internal JwtIssuerOptions _jwtOptions;
        internal string errorMessage = "";

        public Guid FAQID { get; set; }
        [Required]
        [Display(Name = "Title")]
        public string Title { get; set; }
        [Required]
        [Display(Name = "Description")]
        public string Content { get; set; }
        public string Catergory { get; set; }
        internal async Task PopulateDetails()
        {
            if (FAQID != Guid.Empty)
            {
                var item = _context.FAQ.Where(x => x.FAQID == FAQID).FirstOrDefault();
                if (item != null)
                {
                    Title = item.Title;
                    Content = item.Content;
                    Catergory = item.Catergory;
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
                var faq = _context.FAQ.Where(x => x.FAQID == FAQID).FirstOrDefault();
                if (faq == null)
                {
                    faq = new FAQ();
                    isNew = true;
                    faq.FAQID = Guid.NewGuid();
                }

                faq.Title = Title;
                faq.Content = Content;
                faq.Catergory = Catergory;

                if (isNew)
                {
                    _context.Add(faq);
                }
                else
                {
                    _context.Update(faq);
                }

                await _context.SaveChangesAsync();
                FAQID = faq.FAQID;
            }

            return FAQID;
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

            var faq = _context.FAQ.FirstOrDefault(x => x.FAQID == FAQID);

            if (faq != null)
            {
                _context.FAQ.Remove(faq);

                await _context.SaveChangesAsync();
            }
        }
    }

    public class FAQListViewModel
    {
        internal AppDBContext _context;
        internal SecurityOptions _securityOptions;
        internal IEmailService _emailService;

        public string SearchValue { get; set; }
        public PaginationViewModel Pagination { get; set; }

        public List<FAQViewModel> FAQList { get; set; }

        internal async Task PopulateLists()
        {
            if (Pagination == null)
            {
                Pagination = new PaginationViewModel();
                Pagination.Top = 10;
            }

            var list = (from t in _context.FAQ
                        where (!string.IsNullOrEmpty(SearchValue) && (t.Title.Contains(SearchValue) || t.Catergory.Contains(SearchValue)) || string.IsNullOrEmpty(SearchValue))
                        select new FAQViewModel
                        {
                            Title = t.Title,
                            Catergory = t.Catergory,
                            Content = t.Content,
                            FAQID = t.FAQID
                        });

            Pagination.TotalRecords = list.Count();
            if (!string.IsNullOrEmpty(Pagination.SortBy))
            {
                list = list.OrderByName(Pagination.SortBy, Pagination.Descending);
            }
            FAQList = list.Skip(Pagination.Skip).Take(Pagination.Top).ToList();
        }
    }
}
