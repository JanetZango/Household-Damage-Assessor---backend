using ACM.Models.AccountDataModelFactory;
using System.ComponentModel.DataAnnotations.Schema;

namespace ACM.Models.SystemModelFactory
{
    public class ApplicationLog
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid ApplicationLogID { get; set; }

        public Guid? UserID { get; set; }

        public DateTime LogDate { get; set; }
        public string Level { get; set; }
        public string LogOriginator { get; set; }
        public string Message { get; set; }
        public string Exception { get; set; }

        public User User { get; set; }
    }

    public class EmailTemplate
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid EmailTemplateID { get; set; }

        public Guid? CreatedUserID { get; set; }
        public Guid? EditUserID { get; set; }

        public string Description { get; set; }
        public string EventCode { get; set; }
        public string TemplateBody { get; set; }
        public string TemplateBodyTags { get; set; }
        public string SMSTemplateBody { get; set; }

        public DateTime? CreatedDateTime { get; set; }
        public DateTime? EditDateTime { get; set; }
    }

    public class SystemConfiguration
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid SystemConfigurationID { get; set; }

        public Guid? CreatedUserID { get; set; }
        public Guid? EditUserID { get; set; }

        public string Description { get; set; }
        public string EventCode { get; set; }
        public string ConfigValue { get; set; }
        public DateTime? CreatedDateTime { get; set; }
        public DateTime? EditDateTime { get; set; }
    }

    public class FAQ
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid FAQID { get; set; }

        public string Title { get; set; }
        public string Content { get; set; }
        public string Catergory { get; set; }
    }

    public class Country
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid CountryID { get; set; }

        public string Description { get; set; }
        public string IsoAlpha2Code { get; set; }
        public string IsoAlpha3Code { get; set; }
        public string CallingCodePrefix { get; set; }
        public int IDNumberValidationLength { get; set; }
        public bool IsDefault { get; set; }
    }

    public class Province
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid ProvinceID { get; set; }

        public Guid? CountryID { get; set; }
        public string Description { get; set; }
        public string ProvIsoCode { get; set; }
        public Country Country { get; set; }
    }

    public class LanguageCulture
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid LanguageCultureID { get; set; }

        public string CultureNameCode { get; set; }
        public string Description { get; set; }
    }

    public class LocalizationValue
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid LocalizationValueID { get; set; }

        public Guid LanguageCultureID { get; set; }

        public string KeyName { get; set; }
        public string Value { get; set; }

        public LanguageCulture LanguageCulture { get; set; }
    }

    public class Gender
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid GenderID { get; set; }

        public string Description { get; set; }
    }

    public class Ethnicity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid EthnicityID { get; set; }

        public string Description { get; set; }
    }
}