using ACM.Models.AccountDataModelFactory;
using ACM.Models.ACMDataModelFactory;
using ACM.Models.Auditing.AuditAccountDataModelFactory;
using ACM.Models.SystemModelFactory;
using ACM.Models.UserModelFactory;
using School.Models.HouseModelFactory;

namespace ACM.Models
{
    public class AppDBContext : DbContext
    {
        
        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<LinkUserRole> LinkUserRole { get; set; }
        public DbSet<ApplicationLog> ApplicationLog { get; set; }
        public DbSet<EmailTemplate> EmailTemplates { get; set; }
        public DbSet<SystemConfiguration> SystemConfiguration { get; set; }
        public DbSet<TemporaryTokensType> TemporaryTokensType { get; set; }
        public DbSet<UserTemporaryToken> UserTemporaryToken { get; set; }
        public DbSet<FAQ> FAQ { get; set; }
        public DbSet<UserInAppNotification> UserInAppNotifications { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Province> Provinces { get; set; }
        public DbSet<LanguageCulture> LanguageCultures { get; set; }
        public DbSet<LocalizationValue> LocalizationValues { get; set; }

        public DbSet<UserAudit> UserAudit { get; set; }
        public DbSet<LinkUserRoleAudit> LinkUserRoleAudit { get; set; }

        public DbSet<CalendarEventType> CalendarEventTypes { get; set; }
        public DbSet<CalendarEventTypeMetaField> CalendarEventTypeMetaField { get; set; }
        public DbSet<CalendarEvent> CalendarEvents { get; set; }
        public DbSet<CalendarEventMetaFieldValue> CalendarEventMetaFieldValues { get; set; }

        public DbSet<UserPaymentTransaction> UserPaymentTransactions { get; set; }

        public DbSet<Ethnicity> Ethnicities { get; set; }
        public DbSet<Gender> Genders { get; set; }
		public DbSet<House> Houses { get; set; }
		public DbSet<HouseImage> HouseImages { get; set; }

		public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {
        }
    }
}