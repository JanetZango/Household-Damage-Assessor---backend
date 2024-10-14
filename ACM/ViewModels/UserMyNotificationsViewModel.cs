using ACM.Helpers.EmailServiceFactory;
using ACM.SignalRHubs;
using Microsoft.AspNetCore.SignalR;

namespace ACM.ViewModels.UserMyNotificationsViewModel
{
    public class UserMyNotificationsListViewModel
    {
        internal AppDBContext _context;
        internal SecurityOptions _securityOptions;
        internal IEmailService _emailService;
        internal ClaimsPrincipal _user;

        public string SearchValue { get; set; }

        public PaginationViewModel Pagination { get; set; }

        public List<UserMyNotificationsListViewModelData> NotificationList { get; set; }

        public async Task PopulateList()
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

            var list = (from u in _context.UserInAppNotifications
                        where (!string.IsNullOrEmpty(SearchValue) && (u.Title.Contains(SearchValue) || u.Subject.Contains(SearchValue)) || string.IsNullOrEmpty(SearchValue))
                        && u.UserID == userHelper.loggedInUserID
                        orderby u.IsRead, u.CreatedDateTime descending
                        select new UserMyNotificationsListViewModelData
                        {
                            ActionCode = u.ActionCode,
                            ActionID = u.ActionID,
                            CreatedDateTime = u.CreatedDateTime.ToTimezoneFromUtc(_user).Value.ToString("yyyy/MM/dd HH:mm"),
                            IsRead = u.IsRead,
                            Subject = u.Subject,
                            Title = u.Title,
                            UserInAppNotificationID = u.UserInAppNotificationID
                        });

            Pagination.TotalRecords = list.Count();
            if (!string.IsNullOrEmpty(Pagination.SortBy))
            {
                list = list.OrderByName(Pagination.SortBy, Pagination.Descending);
            }
            NotificationList = list.Skip(Pagination.Skip).Take(Pagination.Top).ToList();
        }
    }

    public class UserMyNotificationsListViewModelData
    {
        public string ActionCode { get; set; }
        public Guid? ActionID { get; set; }
        public string CreatedDateTime { get; set; }
        public bool IsRead { get; set; }
        public string Subject { get; set; }
        public string Title { get; set; }
        public Guid UserInAppNotificationID { get; set; }
    }

    public class UserMyNotificationDetailsViewModel
    {
        internal AppDBContext _context;
        internal SecurityOptions _securityOptions;
        internal IEmailService _emailService;
        internal ClaimsPrincipal _user;
        internal IHubContext<UIUpdateHub> _uiUpdateHub;

        public Guid UserInAppNotificationID { get; set; }
        public bool IsRead { get; set; }
        public string Title { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string Received { get; set; }
        public string ActionCode { get; set; }
        public Guid? ActionID { get; set; }

        public async Task Populate()
        {
            UserHelperFunctions userHelper = new UserHelperFunctions()
            {
                _context = _context,
                _emailService = _emailService,
                _securityOptions = _securityOptions,
                _user = _user
            };
            userHelper.Populate();

            if (UserInAppNotificationID != Guid.Empty)
            {
                var item = _context.UserInAppNotifications.FirstOrDefault(x => x.UserInAppNotificationID == UserInAppNotificationID);
                if (item != null)
                {
                    IsRead = item.IsRead;
                    Title = item.Title;
                    Subject = item.Subject;
                    Body = item.Body;
                    ActionCode = item.ActionCode;
                    ActionID = item.ActionID;
                    Received = item.CreatedDateTime.ToTimezoneFromUtc(_user).Value.ToString("yyyy/MM/dd HH:mm");

                    item.IsRead = true;
                    item.EditDateTime = DateTime.UtcNow;
                    item.EditUserID = userHelper.loggedInUserID;

                    _context.Update(item);
                    await _context.SaveChangesAsync();

                    UIUpdateHubHelper helper = new UIUpdateHubHelper()
                    {
                        EventCode = PublicEnums.UIUpdateList.NOTIFICATIONS.ToString(),
                        _uiUpdateHub = _uiUpdateHub
                    };

                    await helper.SendUserUIUpdateNotification(userHelper.loggedInUserID);
                }
            }
        }

        public async Task MarkAllRead()
        {
            UserHelperFunctions userHelper = new UserHelperFunctions()
            {
                _context = _context,
                _emailService = _emailService,
                _securityOptions = _securityOptions,
                _user = _user
            };
            userHelper.Populate();

            var items = _context.UserInAppNotifications.Where(x => x.IsRead == false && x.UserID == userHelper.loggedInUserID);
            foreach (var item in items)
            {
                item.IsRead = true;
                item.EditDateTime = DateTime.UtcNow;
                item.EditUserID = userHelper.loggedInUserID;

                _context.Update(item);
            }

            await _context.SaveChangesAsync();

            UIUpdateHubHelper helper = new UIUpdateHubHelper()
            {
                EventCode = PublicEnums.UIUpdateList.NOTIFICATIONS.ToString(),
                _uiUpdateHub = _uiUpdateHub
            };

            await helper.SendUserUIUpdateNotification(userHelper.loggedInUserID);
        }
    }
}
