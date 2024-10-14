using ACM.Helpers.EmailServiceFactory;

namespace ACM.ViewModels.HomePageViewModelFactory
{
    public class HomePageViewModel
    {
        internal AppDBContext _context;
        internal SecurityOptions _securityOptions;
        internal IEmailService _emailService;
        internal ClaimsPrincipal _user;

        public Int64 RegisteredUsers { get; set; }
        public Int64 UsersNeedsApproval { get; set; }
        public decimal WalletAmount { get; set; }
        public Int64 Leads { get; set; }
        public Int64 CompletedJobs { get; set; }
        public Int64 Technicians { get; set; }

        public async Task PopulateModel()
        {
            UserHelperFunctions userHelper = new UserHelperFunctions()
            {
                _context = _context,
                _securityOptions = _securityOptions,
                _user = _user
            };
            userHelper.Populate();

            //RegisteredUsers = _context.Users.Where(x => x.IsRemoved == false && x.IsEmailVerified == true).Count();
            Leads = 0;
            CompletedJobs = 0;
            Technicians = _context.Users.Where(x => x.IsRemoved == false
                                                                         && _context.LinkUserRole.Include(k => k.UserRole).Any(j => j.UserID == x.UserID && (j.UserRole.EventCode == PublicEnums.UserRoleList.ROLE_TECHNICIAN_USER))).Select(x => x.UserID).Count();

            //UsersNeedsApproval = _context.Users.Where(x => x.IsRemoved == false && x.IsAdminApproved == false
            //    && _context.LinkUserRole.Include(k => k.UserRole).Any(j => j.UserID == x.UserID && (j.UserRole.EventCode == PublicEnums.UserRoleList.ROLE_COACH))).Select(x => x.UserID).Count();

            WalletAmount = _context.UserPaymentTransactions.Where(x => x.UserID == userHelper.loggedInUserID).Select(x => x.AmountGross).ToList().Select(x => decimal.Parse(x)).Sum();
        }
    }
}
