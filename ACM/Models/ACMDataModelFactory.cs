
using ACM.Models.SystemModelFactory;

using ACM.Models.FormDataModelFactory;

using System.ComponentModel.DataAnnotations.Schema;

namespace ACM.Models.ACMDataModelFactory
{
    public class AcmRole
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid AcmRoleID { get; set; }

        public string Category { get; set; }
        public string Description { get; set; }
        public string EventCode { get; set; }
    }

    public class AcmAccessRole
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid AcmAccessRoleID { get; set; }

        public string Description { get; set; }
    }

    public class LinkAcmAccessRoleAcmRole
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid LinkAcmAccessRoleAcmRoleID { get; set; }

        public Guid AcmRoleID { get; set; }
        public Guid AcmAccessRoleID { get; set; }
        public AcmRole AcmRole { get; set; }
        public AcmAccessRole AcmAccessRole { get; set; }
    }

    public class AcmPersonBasicField
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid AcmPersonBasicFieldID { get; set; }

        public string Description { get; set; }
        public string EventCode { get; set; }
    }

    public class AcmPerson
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid AcmPersonID { get; set; }

        public Guid? CountryID { get; set; }
        public Guid? ProvinceID { get; set; }
        public Guid? GenderID { get; set; }
        public Guid? EthnicityID { get; set; }
        public Guid? CreatedUserID { get; set; }
        public Guid? EditUserID { get; set; }
        public DateTime? CreatedDateTime { get; set; }
        public DateTime? EditDateTime { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string IDNumber { get; set; }
        public string PassportNumber { get; set; }
        public string CurrentPlacement { get; set; }
        public DateTime DateOfBirth { get; set; }

        public Country Country { get; set; }
        public Province Province { get; set; }
        public Gender Gender { get; set; }
        public Ethnicity Ethnicity { get; set; }
    }

    public class AcmPersonMetaData
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid AcmPersonMetaDataID { get; set; }

        public Guid AcmPersonID { get; set; }
        public Guid? CreatedUserID { get; set; }
        public Guid? EditUserID { get; set; }
        public DateTime? CreatedDateTime { get; set; }
        public DateTime? EditDateTime { get; set; }

        public string Category { get; set; }
        public string FieldKey { get; set; }
        public string FieldValue { get; set; }

        public AcmPerson AcmPerson { get; set; }
    }

    public class LinkAcmAccessRoleFormDefinition
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid LinkAcmAccessRoleFormDefinitionID { get; set; }

        public Guid AcmAccessRoleID { get; set; }
        public Guid FormDefinitionID { get; set; }

        public FormDefinition FormDefinition { get; set; }
        public AcmAccessRole AcmAccessRole { get; set; }
    }

}