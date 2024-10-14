using ACM.Models.AccountDataModelFactory;
using System.ComponentModel.DataAnnotations.Schema;

namespace ACM.Models.UserModelFactory
{
    public class UserInAppNotification
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid UserInAppNotificationID { get; set; }
        public Guid UserID { get; set; }
        public bool IsRead { get; set; }
        public string Title { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string ActionCode { get; set; }
        public Guid? ActionID { get; set; }
        public Guid? OrganisationID { get; set; }


        public Guid? CreatedUserID { get; set; }
        public Guid? EditUserID { get; set; }
        public DateTime? CreatedDateTime { get; set; }
        public DateTime? EditDateTime { get; set; }

        public User User { get; set; }
    }

    public class CalendarEventType
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid CalendarEventTypeID { get; set; }
        public Guid UserID { get; set; }
        public string Description { get; set; }
        public string ColorCode { get; set; }

        public User User { get; set; }
    }

    public class CalendarEventTypeMetaField
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid CalendarEventTypeMetaFieldID { get; set; }
        public Guid CalendarEventTypeID { get; set; }
        public string Description { get; set; }

        public CalendarEventType CalendarEventType { get; set; }
    }

    public class CalendarEvent
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid CalendarEventID { get; set; }
        public Guid UserID { get; set; }
        public Guid? CalendarEventTypeID { get; set; }
        public string Description { get; set; }
        public string ColorCode { get; set; }
        public string Url { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public bool IsAllDay { get; set; }
        public bool EnableReminder { get; set; }

        public Guid? CreatedUserID { get; set; }
        public Guid? EditUserID { get; set; }
        public DateTime? CreatedDateTime { get; set; }
        public DateTime? EditDateTime { get; set; }

        public User User { get; set; }
        public CalendarEventType CalendarEventType { get; set; }
    }

    public class CalendarEventMetaFieldValue
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid CalendarEventMetaFieldValueID { get; set; }
        public Guid CalendarEventID { get; set; }
        public Guid CalendarEventTypeMetaFieldID { get; set; }
        public string MetaValue { get; set; }

        public CalendarEvent CalendarEvent { get; set; }
        public CalendarEventTypeMetaField CalendarEventTypeMetaField { get; set; }
    }

    public class UserPaymentTransaction
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid UserPaymentTransactionID { get; set; }
        public DateTime TransactionDate { get; set; }
        public Guid? UserID { get; set; }
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
