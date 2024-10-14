using ACM.Models.AccountDataModelFactory;
using System.ComponentModel.DataAnnotations.Schema;

namespace ACM.Models.FormDataModelFactory
{
    public class FormDefinition
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid FormDefinitionID { get; set; }
        public string Description { get; set; }
        public DateTime EffectiveStartDate { get; set; }
        public DateTime EffectiveEndDate { get; set; }
        public string InstructionsFormatted { get; set; }
        public Int64 InstanceNumber { get; set; }
        public bool AllowSignatures { get; set; }
        public bool IsApprovalRequired { get; set; }
        public bool IsLocationRelevant { get; set; }
        public Guid? DynamicTemplateInstanceID { get; set; }
        public Guid? DynamicTemplateInstanceIDCLInst { get; set; }
        public int Version { get; set; }
        public bool IsActive { get; set; }
        public string Tags { get; set; }
        public bool IsLatest { get; set; }

        public Guid? CreatedUserID { get; set; }
        public Guid? EditUserID { get; set; }
        public DateTime? CreatedDateTime { get; set; }
        public DateTime? EditDateTime { get; set; }
    }

    public class FormDefinitionItem
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid FormDefinitionItemID { get; set; }
        public int SequenceNumber { get; set; }
        public string Grouping { get; set; }
        public Guid FormDefinitionID { get; set; }
        public bool IsMandatory { get; set; }

        public string ParentCode { get; set; }
        public Guid? ParentID { get; set; }
        public string ChildCode { get; set; }
        public Guid ChildID { get; set; }

        public Guid? CreatedUserID { get; set; }
        public Guid? EditUserID { get; set; }
        public DateTime? CreatedDateTime { get; set; }
        public DateTime? EditDateTime { get; set; }

        public FormDefinition FormDefinition { get; set; }
    }

    public class FormDefinitionItemPostSubmissionAction
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid FormDefinitionItemPostSubmissionActionID { get; set; }
        public Guid FormDefinitionItemID { get; set; }
        public Guid FormBuilderQuestionPostSubmissionActionID { get; set; }

        public string BasicFieldEventCode { get; set; }
        public string Category { get; set; }
        public string FieldName { get; set; }

        public FormDefinitionItem FormDefinitionItem { get; set; }
        public FormBuilderQuestionPostSubmissionAction FormBuilderQuestionPostSubmissionAction { get; set; }
    }

    public class FormBuilderFormDefinitionLibrary
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid FormBuilderFormDefinitionLibraryID { get; set; }
        public string Description { get; set; }
        public string EventCode { get; set; }
        public bool IsStructure { get; set; }
        public bool IsResponse { get; set; }
        public int DisplayOrder { get; set; }
        public bool IsAvailable { get; set; }
        public string IconClass { get; set; }
    }

    public class FormBuilderQuestionPostSubmissionAction
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid FormBuilderQuestionPostSubmissionActionID { get; set; }
        public string Description { get; set; }
        public string EventCode { get; set; }
    }

    public class FormBuilderFormDefinitionStandardResponseType
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid FormBuilderFormDefinitionStandardResponseTypeID { get; set; }
        public string Description { get; set; }
        public string EventCode { get; set; }
    }

    public class FormBuilderFormDefinitionLocationResponseType
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid FormBuilderFormDefinitionLocationResponseTypeID { get; set; }
        public string Description { get; set; }
        public string EventCode { get; set; }
    }

    public class FormBuilderConditionAction
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid FormBuilderConditionActionID { get; set; }
        public string Description { get; set; }
        public string EventCode { get; set; }
    }

    public class FormBuilderFormDefinitionNumberUOM
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid FormBuilderFormDefinitionNumberUOMID { get; set; }
        public string Description { get; set; }
        public string EventCode { get; set; }
    }

    public class FormBuilderFormDefinitionStandardResponseTypeAnswer
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid FormBuilderFormDefinitionStandardResponseTypeAnswerID { get; set; }
        public Guid FormBuilderFormDefinitionStandardResponseTypeID { get; set; }
        public string Description { get; set; }
        public bool IsFailure { get; set; }
        public double WeightScore { get; set; }
        public int DisplayOrder { get; set; }

        public FormBuilderFormDefinitionLocationResponseType FormBuilderFormDefinitionLocationResponseType { get; set; }
    }

    public class FormDefinitionItemSection
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid FormDefinitionItemSectionID { get; set; }
        public string Description { get; set; }

        public Guid? CreatedUserID { get; set; }
        public Guid? EditUserID { get; set; }
        public DateTime? CreatedDateTime { get; set; }
        public DateTime? EditDateTime { get; set; }
    }

    public class FormDefinitionItemCategory
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid FormDefinitionItemCategoryID { get; set; }
        public string Description { get; set; }

        public Guid? CreatedUserID { get; set; }
        public Guid? EditUserID { get; set; }
        public DateTime? CreatedDateTime { get; set; }
        public DateTime? EditDateTime { get; set; }
    }

    public class FormDefinitionItemDynamic
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid FormDefinitionItemDynamicID { get; set; }
        public string Description { get; set; }
        public bool IsMandatory { get; set; }
        public bool IsRepeatDynamic { get; set; }
        public bool IsPredefinedItems { get; set; }
        public int MinDynamicRepeat { get; set; }
        public string NameOfItemRepeat { get; set; }
        public string GroupingReference { get; set; }

        public Guid? CreatedUserID { get; set; }
        public Guid? EditUserID { get; set; }
        public DateTime? CreatedDateTime { get; set; }
        public DateTime? EditDateTime { get; set; }
    }

    public class FormDefinitionItemDynamicResponse
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid FormDefinitionItemDynamicResponseID { get; set; }
        public Guid FormDefinitionItemDynamicID { get; set; }
        public string Description { get; set; }
        public int DisplayOrder { get; set; }

        public FormDefinitionItemDynamic FormDefinitionItemDynamic { get; set; }
    }

    public class FormDefinitionItemQuestion
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid FormDefinitionItemQuestionID { get; set; }
        public string Description { get; set; }
        public bool IsList { get; set; }
        public bool IsListMultiSelect { get; set; }
        public bool IsMandatory { get; set; }
        public bool IsCaution { get; set; }
        public string CautionDescription { get; set; }
        public string CautionColour { get; set; }
        public string CautionDescription2 { get; set; }
        public string CautionColour2 { get; set; }
        public bool IsInstruction { get; set; }
        public string InstructionText { get; set; }
        public string InstructionImageBlobName { get; set; }
        public bool IsNotify { get; set; }
        public Guid NotificationAnswerID { get; set; }
        public string NotificationEmail { get; set; }
        public double ScoringWeight { get; set; }
        public bool IsRiskRating { get; set; }
        public Guid? FormBuilderFormDefinitionStandardResponseTypeID { get; set; }
        public string GroupingReference { get; set; }

        public bool IsCondition { get; set; }
        public Guid ConditionFormDefinitionItemQuestionID { get; set; }
        public Guid ConditionFormDefinitionItemQuestionResponseID { get; set; }
        public Guid FormBuilderConditionActionID { get; set; }

        public Guid? CreatedUserID { get; set; }
        public Guid? EditUserID { get; set; }
        public DateTime? CreatedDateTime { get; set; }
        public DateTime? EditDateTime { get; set; }
    }

    public class FormDefinitionItemQuestionResponse
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid FormDefinitionItemQuestionResponseID { get; set; }
        public Guid FormDefinitionItemQuestionID { get; set; }
        public string Description { get; set; }
        public bool IsFailure { get; set; }
        public double WeightScore { get; set; }
        public int DisplayOrder { get; set; }

        public FormDefinitionItemQuestion FormDefinitionItemQuestion { get; set; }
    }

    public class FormDefinitionItemNumber
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid FormDefinitionItemNumberID { get; set; }
        public string Description { get; set; }
        public bool IsMandatory { get; set; }
        public bool IsCaution { get; set; }
        public string CautionDescription { get; set; }
        public string CautionColour { get; set; }
        public string CautionDescription2 { get; set; }
        public string CautionColour2 { get; set; }
        public bool IsInstruction { get; set; }
        public string InstructionText { get; set; }
        public string InstructionImageBlobName { get; set; }
        public bool DisplayPreviousMeasurement { get; set; }
        public bool IsTolerance { get; set; }
        public double ToleranceRangeFrom { get; set; }
        public double ToleranceRangeTo { get; set; }
        public string ToleranceExceededPrompt { get; set; }
        public string UnitOfMeasure { get; set; }
        public bool IsSystemMeasurement { get; set; }
        public string MeasureIdentifier { get; set; }
        public string TolleranceComparisonOperator { get; set; }
        public string GroupingReference { get; set; }

        public bool IsCondition { get; set; }
        public Guid ConditionFormDefinitionItemQuestionID { get; set; }
        public Guid ConditionFormDefinitionItemQuestionResponseID { get; set; }
        public Guid FormBuilderConditionActionID { get; set; }

        public Guid? CreatedUserID { get; set; }
        public Guid? EditUserID { get; set; }
        public DateTime? CreatedDateTime { get; set; }
        public DateTime? EditDateTime { get; set; }
    }

    public class FormDefinitionItemDateTime
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid FormDefinitionItemDateTimeID { get; set; }
        public string Description { get; set; }
        public bool IsMandatory { get; set; }
        public bool IsCaution { get; set; }
        public string CautionDescription { get; set; }
        public string CautionColour { get; set; }
        public string CautionDescription2 { get; set; }
        public string CautionColour2 { get; set; }
        public bool IsInstruction { get; set; }
        public string InstructionText { get; set; }
        public string InstructionImageBlobName { get; set; }
        public bool AllowPastDates { get; set; }
        public bool AllowPastTimes { get; set; }
        public bool CaptureDate { get; set; }
        public bool CaptureTime { get; set; }
        public string GroupingReference { get; set; }

        public bool IsCondition { get; set; }
        public Guid ConditionFormDefinitionItemQuestionID { get; set; }
        public Guid ConditionFormDefinitionItemQuestionResponseID { get; set; }
        public Guid FormBuilderConditionActionID { get; set; }

        public Guid? CreatedUserID { get; set; }
        public Guid? EditUserID { get; set; }
        public DateTime? CreatedDateTime { get; set; }
        public DateTime? EditDateTime { get; set; }
    }

    public class FormDefinitionItemInformation
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid FormDefinitionItemInformationID { get; set; }

        public string InstructionText { get; set; }
        public string InstructionImageBlobName { get; set; }

        public bool IsCondition { get; set; }
        public Guid ConditionFormDefinitionItemQuestionID { get; set; }
        public Guid ConditionFormDefinitionItemQuestionResponseID { get; set; }
        public Guid FormBuilderConditionActionID { get; set; }

        public Guid? CreatedUserID { get; set; }
        public Guid? EditUserID { get; set; }
        public DateTime? CreatedDateTime { get; set; }
        public DateTime? EditDateTime { get; set; }
    }

    public class FormDefinitionItemLocation
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid FormDefinitionItemLocationID { get; set; }
        public string Description { get; set; }
        public bool IsMandatory { get; set; }
        public bool IsInstruction { get; set; }
        public string InstructionText { get; set; }
        public string InstructionImageBlobName { get; set; }
        public Guid? FormBuilderFormDefinitionLocationResponseTypeID { get; set; }
        public bool IsTextInput { get; set; }
        public bool IsLocationList { get; set; }
        public string GroupingReference { get; set; }

        public bool IsCondition { get; set; }
        public Guid ConditionFormDefinitionItemQuestionID { get; set; }
        public Guid ConditionFormDefinitionItemQuestionResponseID { get; set; }
        public Guid FormBuilderConditionActionID { get; set; }

        public Guid? CreatedUserID { get; set; }
        public Guid? EditUserID { get; set; }
        public DateTime? CreatedDateTime { get; set; }
        public DateTime? EditDateTime { get; set; }
    }

    public class FormDefinitionItemLocationResponse
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid FormDefinitionItemLocationResponseID { get; set; }
        public Guid FormDefinitionItemLocationID { get; set; }
        public string Description { get; set; }
        public int DisplayOrder { get; set; }

        public FormDefinitionItemLocation FormDefinitionItemLocation { get; set; }
    }

    public class FormDefinitionItemText
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid FormDefinitionItemTextID { get; set; }
        public string Description { get; set; }
        public bool IsMultiline { get; set; }
        public bool IsMandatory { get; set; }
        public bool IsInstruction { get; set; }
        public string InstructionText { get; set; }
        public string InstructionImageBlobName { get; set; }
        public string GroupingReference { get; set; }
        public bool IsPersonLookupField { get; set; }

        public bool IsCondition { get; set; }
        public Guid ConditionFormDefinitionItemQuestionID { get; set; }
        public Guid ConditionFormDefinitionItemQuestionResponseID { get; set; }
        public Guid FormBuilderConditionActionID { get; set; }

        public Guid? CreatedUserID { get; set; }
        public Guid? EditUserID { get; set; }
        public DateTime? CreatedDateTime { get; set; }
        public DateTime? EditDateTime { get; set; }
    }

    public class FormDefinitionItemSlider
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid FormDefinitionItemSliderID { get; set; }
        public string Description { get; set; }
        public bool IsMandatory { get; set; }
        public bool IsInstruction { get; set; }
        public string InstructionText { get; set; }
        public string InstructionImageBlobName { get; set; }
        public bool IsCaution { get; set; }
        public string CautionDescription { get; set; }
        public string CautionColour { get; set; }
        public string CautionDescription2 { get; set; }
        public string CautionColour2 { get; set; }
        public bool IsRiskRating { get; set; }
        public int MinValue { get; set; }
        public int MaxValue { get; set; }
        public double Increments { get; set; }
        public string GroupingReference { get; set; }

        public bool IsCondition { get; set; }
        public Guid ConditionFormDefinitionItemQuestionID { get; set; }
        public Guid ConditionFormDefinitionItemQuestionResponseID { get; set; }
        public Guid FormBuilderConditionActionID { get; set; }

        public Guid? CreatedUserID { get; set; }
        public Guid? EditUserID { get; set; }
        public DateTime? CreatedDateTime { get; set; }
        public DateTime? EditDateTime { get; set; }
    }

    public class FormDefinitionItemSignature
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid FormDefinitionItemSignatureID { get; set; }
        public string Description { get; set; }
        public bool IsMandatory { get; set; }
        public bool IsInstruction { get; set; }
        public string InstructionText { get; set; }
        public string InstructionImageBlobName { get; set; }
        public string GroupingReference { get; set; }

        public bool IsCondition { get; set; }
        public Guid ConditionFormDefinitionItemQuestionID { get; set; }
        public Guid ConditionFormDefinitionItemQuestionResponseID { get; set; }
        public Guid FormBuilderConditionActionID { get; set; }

        public Guid? CreatedUserID { get; set; }
        public Guid? EditUserID { get; set; }
        public DateTime? CreatedDateTime { get; set; }
        public DateTime? EditDateTime { get; set; }
    }

    public class FormDefinitionItemDrawing
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid FormDefinitionItemDrawingID { get; set; }
        public string Description { get; set; }
        public bool IsMandatory { get; set; }
        public bool IsInstruction { get; set; }
        public string InstructionText { get; set; }
        public string InstructionImageBlobName { get; set; }
        public string GroupingReference { get; set; }

        public bool IsCondition { get; set; }
        public Guid ConditionFormDefinitionItemQuestionID { get; set; }
        public Guid ConditionFormDefinitionItemQuestionResponseID { get; set; }
        public Guid FormBuilderConditionActionID { get; set; }

        public Guid? CreatedUserID { get; set; }
        public Guid? EditUserID { get; set; }
        public DateTime? CreatedDateTime { get; set; }
        public DateTime? EditDateTime { get; set; }
    }

    public class FormDefinitionItemPicture
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid FormDefinitionItemPictureID { get; set; }
        public string Description { get; set; }
        public bool IsMandatory { get; set; }
        public bool IsInstruction { get; set; }
        public string InstructionText { get; set; }
        public string InstructionImageBlobName { get; set; }
        public bool AllowMultipleUploads { get; set; }
        public string GroupingReference { get; set; }

        public bool IsCondition { get; set; }
        public Guid ConditionFormDefinitionItemQuestionID { get; set; }
        public Guid ConditionFormDefinitionItemQuestionResponseID { get; set; }
        public Guid FormBuilderConditionActionID { get; set; }

        public Guid? CreatedUserID { get; set; }
        public Guid? EditUserID { get; set; }
        public DateTime? CreatedDateTime { get; set; }
        public DateTime? EditDateTime { get; set; }
    }

    public class FormInstanceState
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid FormInstanceStateID { get; set; }
        public string Description { get; set; }
        public string EventCode { get; set; }

        public Guid? CreatedUserID { get; set; }
        public Guid? EditUserID { get; set; }
        public DateTime? CreatedDateTime { get; set; }
        public DateTime? EditDateTime { get; set; }
    }

    public class GPSLocation
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid GPSLocationID { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public double Accuracy { get; set; }
        public double Altitude { get; set; }

        public Guid? CreatedUserID { get; set; }
        public Guid? EditUserID { get; set; }
        public DateTime? CreatedDateTime { get; set; }
        public DateTime? EditDateTime { get; set; }
    }

    public class FormInstance
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid FormInstanceID { get; set; }
        public DateTime? FormExecutionDate { get; set; }
        public DateTime? FormCaptureStartDateTime { get; set; }
        public DateTime? FormCaptureEndDateTime { get; set; }
        public string Comments { get; set; }
        public Guid FormDefinitionID { get; set; }
        public Guid FormInstanceStateID { get; set; }
        public Int64 InstanceNumber { get; set; }
        public string Location { get; set; }
        public Guid? GPSLocationID { get; set; }
        public Guid? ApprovedByUserID { get; set; }
        public DateTime ApprovedDateTime { get; set; }
        public string ApprovedBySignatureBlobName { get; set; }
        public bool IsCompleted { get; set; }
        public bool IsRemoved { get; set; }

        public int? FBItemsTotal { get; set; }
        public int? FBItemsAnswered { get; set; }
        public int? FBItemsInOrder { get; set; }
        public int? FBItemsNotInOrder { get; set; }

        [ForeignKey("CreatedUser")]
        public Guid CreatedUserID { get; set; }
        public Guid EditUserID { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public DateTime EditDateTime { get; set; }

        public FormDefinition FormDefinition { get; set; }
        public FormInstanceState FormInstanceState { get; set; }
        public GPSLocation GPSLocation { get; set; }
        public User CreatedUser { get; set; }
    }

    public class FormInstanceItem
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid FormInstanceItemID { get; set; }
        public Guid FormInstanceID { get; set; }
        public Guid? FormDefinitionItemID { get; set; }
        public Guid? FormInstanceItemDynamicID { get; set; }
        public string ChildCode { get; set; }
        public string Remarks { get; set; }

        public bool FBIsAnswered { get; set; }
        public bool FBIsInOrder { get; set; }
        public bool FBIsNotInOrder { get; set; }

        public Guid? CreatedUserID { get; set; }
        public Guid? EditUserID { get; set; }
        public DateTime? CreatedDateTime { get; set; }
        public DateTime? EditDateTime { get; set; }

        public FormInstance FormInstance { get; set; }
        public FormDefinitionItem FormDefinitionItem { get; set; }
    }

    public class FormInstanceItemQuestion
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid FormInstanceItemQuestionID { get; set; }
        public Guid FormInstanceItemID { get; set; }
        public Guid FormDefinitionItemQuestionResponseID { get; set; }

        public FormInstanceItem FormInstanceItem { get; set; }
    }

    public class FormInstanceItemDynamic
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid FormInstanceItemDynamicID { get; set; }
        public Guid FormInstanceItemID { get; set; }
        public Guid? FormDefinitionItemDynamicResponseID { get; set; }
        public string ItemName { get; set; }
        public int DisplayOrder { get; set; }

        public FormInstanceItem FormInstanceItem { get; set; }
    }

    public class FormInstanceItemNumber
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid FormInstanceItemNumberID { get; set; }
        public Guid FormInstanceItemID { get; set; }
        public double NumberValue { get; set; }

        public FormInstanceItem FormInstanceItem { get; set; }
    }

    public class FormInstanceItemText
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid FormInstanceItemTextID { get; set; }
        public Guid FormInstanceItemID { get; set; }
        public string TextValue { get; set; }

        public FormInstanceItem FormInstanceItem { get; set; }
    }

    public class FormInstanceItemSlider
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid FormInstanceItemSliderID { get; set; }
        public Guid FormInstanceItemID { get; set; }
        public double SliderValue { get; set; }

        public FormInstanceItem FormInstanceItem { get; set; }
    }

    public class FormInstanceItemSignature
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid FormInstanceItemSignatureID { get; set; }
        public Guid FormInstanceItemID { get; set; }
        public string BlobName { get; set; }

        public FormInstanceItem FormInstanceItem { get; set; }
    }

    public class FormInstanceItemDateTime
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid FormInstanceItemDateTimeID { get; set; }
        public Guid FormInstanceItemID { get; set; }
        public string DateValue { get; set; }
        public string TimeValue { get; set; }

        public FormInstanceItem FormInstanceItem { get; set; }
    }

    public class FormInstanceItemLocation
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid FormInstanceItemLocationID { get; set; }
        public Guid FormInstanceItemID { get; set; }
        public string LocationText { get; set; }
        public Guid? FormDefinitionItemLocationResponseID { get; set; }

        public FormInstanceItem FormInstanceItem { get; set; }
    }

    public class FormInstanceItemPicture
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid FormInstanceItemPictureID { get; set; }
        public Guid FormInstanceItemID { get; set; }
        public string BlobName { get; set; }

        public FormInstanceItem FormInstanceItem { get; set; }
    }

    public class FormInstanceItemDrawing
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid FormInstanceItemDrawingID { get; set; }
        public Guid FormInstanceItemID { get; set; }
        public string BlobName { get; set; }
        public string SerializedPoints { get; set; }

        public FormInstanceItem FormInstanceItem { get; set; }
    }

    public class FormInstanceItemSupportingDocument
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid FormInstanceItemSupportingDocumentID { get; set; }
        public Guid FormInstanceItemID { get; set; }

        public string BlobName { get; set; }
        public string ContentType { get; set; }
        public string FileName { get; set; }

        public Guid? CreatedUserID { get; set; }
        public Guid? EditUserID { get; set; }
        public DateTime? CreatedDateTime { get; set; }
        public DateTime? EditDateTime { get; set; }

        public FormInstanceItem FormInstanceItem { get; set; }
    }


}
