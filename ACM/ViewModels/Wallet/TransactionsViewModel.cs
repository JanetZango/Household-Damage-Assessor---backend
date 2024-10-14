using ACM.Helpers.EmailServiceFactory;
using ACM.Helpers.Localization;
using Microsoft.Extensions.Localization;

namespace ACM.ViewModels.Wallet
{
    public class TransactionsViewModel
    {
        internal AppDBContext _context;
        internal IEmailService _emailService;
        internal SecurityOptions _securityOptions;
        internal ClaimsPrincipal _user;
        internal IStringLocalizer<SessionStringLocalizer> _localizer;

        public PaginationViewModel Pagination { get; set; }
        public List<TransactionsViewModelData> Items { get; set; }

        internal async Task PopulateList()
        {
            UserHelperFunctions userHelper = new UserHelperFunctions()
            {
                _context = _context,
                _emailService = _emailService,
                _securityOptions = _securityOptions,
                _user = _user
            };
            userHelper.Populate();

            if (Pagination == null)
            {
                Pagination = new PaginationViewModel();
                Pagination.Top = 10;
            }

            var list = from t in _context.UserPaymentTransactions
                        where t.UserID == userHelper.loggedInUserID
                        orderby t.TransactionDate descending
                        select new TransactionsViewModelData
                        {
                            TransactionDate = t.TransactionDate,
                            AmountFee = t.AmountFee,
                            AmountGross = t.AmountGross,
                            AmountNet = t.AmountNet,
                            ItemName = t.ItemName,
                            ParentRefID = t.ParentRefID,
                            PaymentType = t.PaymentType,
                            PFPaymentID = t.PFPaymentID,
                            PFPaymentStatus = t.PFPaymentStatus,
                            PFReferenceID = t.PFReferenceID,
                            TransactionType = t.TransactionType,
                            UserPaymentTransactionID = t.UserPaymentTransactionID,
                        };

            Pagination.TotalRecords = list.Count();
            if (!string.IsNullOrEmpty(Pagination.SortBy))
            {
                list = list.OrderByName(Pagination.SortBy, Pagination.Descending);
            }
            Items = list.Skip(Pagination.Skip).Take(Pagination.Top).ToList();
        }
    }

    public class TransactionsViewModelData
    {
        public Guid UserPaymentTransactionID { get; set; }
        public DateTime TransactionDate { get; set; }
        public string PaymentType { get; set; }
        public string TransactionType { get; set; }
        public string AmountGross { get; set; }
        public string AmountFee { get; set; }
        public string AmountNet { get; set; }
        public string PFPaymentID { get; set; }
        public string PFReferenceID { get; set; }
        public string PFPaymentStatus { get; set; }
        public string ItemName { get; set; }
        public string ParentRefID { get; set; }
    }
}
