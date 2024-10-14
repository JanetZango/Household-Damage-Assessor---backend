using ACM.Helpers.EmailServiceFactory;

namespace ACM.ViewModels
{
    public class FAQDisplayViewModel
    {
        internal AppDBContext _context;
        internal SecurityOptions _securityOptions;
        internal IEmailService _emailService;
        internal ClaimsPrincipal _user;

        public List<FAQDisplayViewModelCategory> Categories { get; set; }

        public async Task Populate()
        {
            var categoryList = (from c in _context.FAQ
                                select c.Catergory).Distinct().OrderBy(x => x).ToList();

            Categories = (from c in categoryList
                          select new FAQDisplayViewModelCategory
                          {
                              CategoryName = c,
                              FAQList = (from f in _context.FAQ
                                         where f.Catergory == c
                                         orderby f.Title
                                         select new FAQDisplayViewModelFAQ
                                         {
                                             Content = f.Content,
                                             FAQID = f.FAQID,
                                             Title = f.Title
                                         }).ToList()
                          }).ToList();
        }

    }
    public class FAQDisplayViewModelCategory
    {
        public string CategoryName { get; set; }
        public List<FAQDisplayViewModelFAQ> FAQList { get; set; }

    }
    public class FAQDisplayViewModelFAQ
    {
        public Guid FAQID { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
    }
}
