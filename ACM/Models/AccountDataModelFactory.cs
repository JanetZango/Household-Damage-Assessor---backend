using ACM.Models.ACMDataModelFactory;
using ACM.Models.SystemModelFactory;
using System.ComponentModel.DataAnnotations.Schema;
namespace ACM.Models.AccountDataModelFactory
{
    public class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid UserID { get; set; }
        public Guid? LanguageCultureID { get; set; }
        public Guid? CountryID { get; set; }
        public Guid? ProvinceID { get; set; }
        public Guid CreatedUserID { get; set; }
        public Guid EditUserID { get; set; }

        public string Password { get; set; }
        public string DisplayName { get; set; }
        public string EmailAddress { get; set; }
        public string CellphoneNumber { get; set; }
        public int LoginTries { get; set; } = 0;
        public bool IsSuspended { get; set; }
        public bool IsRemoved { get; set; } = false;
        public bool AcceptTermsAndConditions { get; set; } = true;
        public bool IsEmailVerified { get; set; } = true;
        public DateTime? CreatedDateTime { get; set; }
        public DateTime? EditDateTime { get; set; }
        public string Timezone { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string Title { get; set; }
        public string IDNumber { get; set; }
        public bool ReceiveEmailNotification { get; set; }
        public bool IsAdminApproved { get; set; } = true;
		public string ProfileImageName { get; set; }

        public LanguageCulture LanguageCulture { get; set; }
        public Country Country { get; set; }
        public Province Province { get; set; }

    }
    public class UserRole
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid UserRoleID { get; set; }
        public string Description { get; set; }
        public string EventCode { get; set; }
    }
    public class LinkUserRole
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid LinkUserRoleID { get; set; }
        public Guid UserRoleID { get; set; }
        public Guid UserID { get; set; }
        public Guid CreatedUserID { get; set; }
        public Guid EditUserID { get; set; }
        public DateTime? CreatedDateTime { get; set; }
        public DateTime? EditDateTime { get; set; }
        public UserRole UserRole { get; set; }
        public User User { get; set; }
    }
    public class TemporaryTokensType
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid TemporaryTokensTypeID { get; set; }
        public string Description { get; set; }
        public string EventCode { get; set; }
    }
    public class UserTemporaryToken
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid UserTemporaryTokenID { get; set; }
        public Guid TemporaryTokensTypeID { get; set; }
        public Guid UserID { get; set; }

        public DateTime TokenExpiryDate { get; set; }

        public Guid? CreatedUserID { get; set; }
        public Guid? EditUserID { get; set; }
        public DateTime? CreatedDateTime { get; set; }
        public DateTime? EditDateTime { get; set; }

        public User User { get; set; }
        public TemporaryTokensType TemporaryTokensType { get; set; }
    }
}
