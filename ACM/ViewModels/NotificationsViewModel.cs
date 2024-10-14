namespace ACM.ViewModels
{
    public class NotificationsViewModel
    {
        public List<NotificationsViewModelData> NotificationList { get; set; }
        public int TotalNtf { get; set; }
    }

    public class NotificationsViewModelData
    {
        public Guid UserInAppNotificationID { get; set; }
        public string Title { get; set; }
        public string Subject { get; set; }
        public string TimeAgo { get; set; }
        public string IconClass { get; set; }
        public string IconBGColor { get; set; }
    }
}
