using System.ComponentModel.DataAnnotations.Schema;

namespace ACM.Models.Auditing.AuditAccountDataModelFactory
{
    public class UserAudit
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid UserAuditID { get; set; }
        public Guid UserID { get; set; }
        public string Password { get; set; }
        public string DisplayName { get; set; }
        public string EmailAddress { get; set; }
        public string CellphoneNumber { get; set; }
        public int LoginTries { get; set; }
        public bool IsSuspended { get; set; }
        public bool IsRemoved { get; set; } = false;
        public bool AcceptTermsAndConditions { get; set; } = true;
        public bool IsEmailVerified { get; set; }

        public Guid CreatedUserID { get; set; }
        public Guid EditUserID { get; set; }
        public DateTime? CreatedDateTime { get; set; }
        public DateTime? EditDateTime { get; set; }

        public string Timezone { get; set; }
        public Guid? LanguageCultureID { get; set; }

        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string Title { get; set; }
        public string IDNumber { get; set; }

        public Guid? CountryID { get; set; }

        public DateTime ValidFromDate { get; set; }
        public DateTime ValidToDate { get; set; }
    }

    public class LinkUserRoleAudit
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid LinkUserRoleAuditID { get; set; }
        public Guid LinkUserRoleID { get; set; }
        public Guid UserRoleID { get; set; }
        public Guid UserID { get; set; }

        public Guid CreatedUserID { get; set; }
        public Guid EditUserID { get; set; }
        public DateTime? CreatedDateTime { get; set; }
        public DateTime? EditDateTime { get; set; }

        public DateTime ValidFromDate { get; set; }
        public DateTime ValidToDate { get; set; }
    }
}
