using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ACM.Migrations
{
    public partial class FBItem_505 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FormBuilderConditionActions",
                columns: table => new
                {
                    FormBuilderConditionActionID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EventCode = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormBuilderConditionActions", x => x.FormBuilderConditionActionID);
                });

            migrationBuilder.CreateTable(
                name: "FormBuilderFormDefinitionLibrary",
                columns: table => new
                {
                    FormBuilderFormDefinitionLibraryID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EventCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsStructure = table.Column<bool>(type: "bit", nullable: false),
                    IsResponse = table.Column<bool>(type: "bit", nullable: false),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false),
                    IsAvailable = table.Column<bool>(type: "bit", nullable: false),
                    IconClass = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormBuilderFormDefinitionLibrary", x => x.FormBuilderFormDefinitionLibraryID);
                });

            migrationBuilder.CreateTable(
                name: "FormBuilderFormDefinitionLocationResponseTypes",
                columns: table => new
                {
                    FormBuilderFormDefinitionLocationResponseTypeID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EventCode = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormBuilderFormDefinitionLocationResponseTypes", x => x.FormBuilderFormDefinitionLocationResponseTypeID);
                });

            migrationBuilder.CreateTable(
                name: "FormBuilderFormDefinitionNumberUOM",
                columns: table => new
                {
                    FormBuilderFormDefinitionNumberUOMID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EventCode = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormBuilderFormDefinitionNumberUOM", x => x.FormBuilderFormDefinitionNumberUOMID);
                });

            migrationBuilder.CreateTable(
                name: "FormBuilderFormDefinitionStandardResponseTypes",
                columns: table => new
                {
                    FormBuilderFormDefinitionStandardResponseTypeID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EventCode = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormBuilderFormDefinitionStandardResponseTypes", x => x.FormBuilderFormDefinitionStandardResponseTypeID);
                });

            migrationBuilder.CreateTable(
                name: "FormDefinitionItemCategory",
                columns: table => new
                {
                    FormDefinitionItemCategoryID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedUserID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    EditUserID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EditDateTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormDefinitionItemCategory", x => x.FormDefinitionItemCategoryID);
                });

            migrationBuilder.CreateTable(
                name: "FormDefinitionItemDateTimes",
                columns: table => new
                {
                    FormDefinitionItemDateTimeID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsMandatory = table.Column<bool>(type: "bit", nullable: false),
                    IsCaution = table.Column<bool>(type: "bit", nullable: false),
                    CautionDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CautionColour = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CautionDescription2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CautionColour2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsInstruction = table.Column<bool>(type: "bit", nullable: false),
                    InstructionText = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InstructionImageBlobName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AllowPastDates = table.Column<bool>(type: "bit", nullable: false),
                    AllowPastTimes = table.Column<bool>(type: "bit", nullable: false),
                    CaptureDate = table.Column<bool>(type: "bit", nullable: false),
                    CaptureTime = table.Column<bool>(type: "bit", nullable: false),
                    GroupingReference = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsCondition = table.Column<bool>(type: "bit", nullable: false),
                    ConditionFormDefinitionItemQuestionID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ConditionFormDefinitionItemQuestionResponseID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FormBuilderConditionActionID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedUserID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    EditUserID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EditDateTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormDefinitionItemDateTimes", x => x.FormDefinitionItemDateTimeID);
                });

            migrationBuilder.CreateTable(
                name: "FormDefinitionItemDrawings",
                columns: table => new
                {
                    FormDefinitionItemDrawingID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsMandatory = table.Column<bool>(type: "bit", nullable: false),
                    IsInstruction = table.Column<bool>(type: "bit", nullable: false),
                    InstructionText = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InstructionImageBlobName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GroupingReference = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsCondition = table.Column<bool>(type: "bit", nullable: false),
                    ConditionFormDefinitionItemQuestionID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ConditionFormDefinitionItemQuestionResponseID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FormBuilderConditionActionID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedUserID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    EditUserID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EditDateTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormDefinitionItemDrawings", x => x.FormDefinitionItemDrawingID);
                });

            migrationBuilder.CreateTable(
                name: "FormDefinitionItemDynamic",
                columns: table => new
                {
                    FormDefinitionItemDynamicID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsMandatory = table.Column<bool>(type: "bit", nullable: false),
                    IsRepeatDynamic = table.Column<bool>(type: "bit", nullable: false),
                    IsPredefinedItems = table.Column<bool>(type: "bit", nullable: false),
                    MinDynamicRepeat = table.Column<int>(type: "int", nullable: false),
                    NameOfItemRepeat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GroupingReference = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedUserID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    EditUserID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EditDateTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormDefinitionItemDynamic", x => x.FormDefinitionItemDynamicID);
                });

            migrationBuilder.CreateTable(
                name: "FormDefinitionItemInformations",
                columns: table => new
                {
                    FormDefinitionItemInformationID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InstructionText = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InstructionImageBlobName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsCondition = table.Column<bool>(type: "bit", nullable: false),
                    ConditionFormDefinitionItemQuestionID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ConditionFormDefinitionItemQuestionResponseID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FormBuilderConditionActionID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedUserID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    EditUserID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EditDateTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormDefinitionItemInformations", x => x.FormDefinitionItemInformationID);
                });

            migrationBuilder.CreateTable(
                name: "FormDefinitionItemLocations",
                columns: table => new
                {
                    FormDefinitionItemLocationID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsMandatory = table.Column<bool>(type: "bit", nullable: false),
                    IsInstruction = table.Column<bool>(type: "bit", nullable: false),
                    InstructionText = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InstructionImageBlobName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FormBuilderFormDefinitionLocationResponseTypeID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsTextInput = table.Column<bool>(type: "bit", nullable: false),
                    IsLocationList = table.Column<bool>(type: "bit", nullable: false),
                    GroupingReference = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsCondition = table.Column<bool>(type: "bit", nullable: false),
                    ConditionFormDefinitionItemQuestionID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ConditionFormDefinitionItemQuestionResponseID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FormBuilderConditionActionID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedUserID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    EditUserID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EditDateTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormDefinitionItemLocations", x => x.FormDefinitionItemLocationID);
                });

            migrationBuilder.CreateTable(
                name: "FormDefinitionItemNumbers",
                columns: table => new
                {
                    FormDefinitionItemNumberID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsMandatory = table.Column<bool>(type: "bit", nullable: false),
                    IsCaution = table.Column<bool>(type: "bit", nullable: false),
                    CautionDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CautionColour = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CautionDescription2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CautionColour2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsInstruction = table.Column<bool>(type: "bit", nullable: false),
                    InstructionText = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InstructionImageBlobName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DisplayPreviousMeasurement = table.Column<bool>(type: "bit", nullable: false),
                    IsTolerance = table.Column<bool>(type: "bit", nullable: false),
                    ToleranceRangeFrom = table.Column<double>(type: "float", nullable: false),
                    ToleranceRangeTo = table.Column<double>(type: "float", nullable: false),
                    ToleranceExceededPrompt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UnitOfMeasure = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsSystemMeasurement = table.Column<bool>(type: "bit", nullable: false),
                    MeasureIdentifier = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TolleranceComparisonOperator = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GroupingReference = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsCondition = table.Column<bool>(type: "bit", nullable: false),
                    ConditionFormDefinitionItemQuestionID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ConditionFormDefinitionItemQuestionResponseID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FormBuilderConditionActionID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedUserID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    EditUserID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EditDateTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormDefinitionItemNumbers", x => x.FormDefinitionItemNumberID);
                });

            migrationBuilder.CreateTable(
                name: "FormDefinitionItemPictures",
                columns: table => new
                {
                    FormDefinitionItemPictureID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsMandatory = table.Column<bool>(type: "bit", nullable: false),
                    IsInstruction = table.Column<bool>(type: "bit", nullable: false),
                    InstructionText = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InstructionImageBlobName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AllowMultipleUploads = table.Column<bool>(type: "bit", nullable: false),
                    GroupingReference = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsCondition = table.Column<bool>(type: "bit", nullable: false),
                    ConditionFormDefinitionItemQuestionID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ConditionFormDefinitionItemQuestionResponseID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FormBuilderConditionActionID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedUserID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    EditUserID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EditDateTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormDefinitionItemPictures", x => x.FormDefinitionItemPictureID);
                });

            migrationBuilder.CreateTable(
                name: "FormDefinitionItemQuestions",
                columns: table => new
                {
                    FormDefinitionItemQuestionID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsList = table.Column<bool>(type: "bit", nullable: false),
                    IsListMultiSelect = table.Column<bool>(type: "bit", nullable: false),
                    IsMandatory = table.Column<bool>(type: "bit", nullable: false),
                    IsCaution = table.Column<bool>(type: "bit", nullable: false),
                    CautionDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CautionColour = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CautionDescription2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CautionColour2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsInstruction = table.Column<bool>(type: "bit", nullable: false),
                    InstructionText = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InstructionImageBlobName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsNotify = table.Column<bool>(type: "bit", nullable: false),
                    NotificationAnswerID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NotificationEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ScoringWeight = table.Column<double>(type: "float", nullable: false),
                    IsRiskRating = table.Column<bool>(type: "bit", nullable: false),
                    FormBuilderFormDefinitionStandardResponseTypeID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    GroupingReference = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsCondition = table.Column<bool>(type: "bit", nullable: false),
                    ConditionFormDefinitionItemQuestionID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ConditionFormDefinitionItemQuestionResponseID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FormBuilderConditionActionID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedUserID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    EditUserID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EditDateTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormDefinitionItemQuestions", x => x.FormDefinitionItemQuestionID);
                });

            migrationBuilder.CreateTable(
                name: "FormDefinitionItems",
                columns: table => new
                {
                    FormDefinitionItemID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SequenceNumber = table.Column<int>(type: "int", nullable: false),
                    Grouping = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FormDefinitionID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsMandatory = table.Column<bool>(type: "bit", nullable: false),
                    ParentCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ParentID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ChildCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChildID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedUserID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    EditUserID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EditDateTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormDefinitionItems", x => x.FormDefinitionItemID);
                    table.ForeignKey(
                        name: "FK_FormDefinitionItems_FormDefinitions_FormDefinitionID",
                        column: x => x.FormDefinitionID,
                        principalTable: "FormDefinitions",
                        principalColumn: "FormDefinitionID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FormDefinitionItemSections",
                columns: table => new
                {
                    FormDefinitionItemSectionID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedUserID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    EditUserID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EditDateTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormDefinitionItemSections", x => x.FormDefinitionItemSectionID);
                });

            migrationBuilder.CreateTable(
                name: "FormDefinitionItemSignatures",
                columns: table => new
                {
                    FormDefinitionItemSignatureID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsMandatory = table.Column<bool>(type: "bit", nullable: false),
                    IsInstruction = table.Column<bool>(type: "bit", nullable: false),
                    InstructionText = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InstructionImageBlobName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GroupingReference = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsCondition = table.Column<bool>(type: "bit", nullable: false),
                    ConditionFormDefinitionItemQuestionID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ConditionFormDefinitionItemQuestionResponseID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FormBuilderConditionActionID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedUserID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    EditUserID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EditDateTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormDefinitionItemSignatures", x => x.FormDefinitionItemSignatureID);
                });

            migrationBuilder.CreateTable(
                name: "FormDefinitionItemSliders",
                columns: table => new
                {
                    FormDefinitionItemSliderID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsMandatory = table.Column<bool>(type: "bit", nullable: false),
                    IsInstruction = table.Column<bool>(type: "bit", nullable: false),
                    InstructionText = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InstructionImageBlobName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsCaution = table.Column<bool>(type: "bit", nullable: false),
                    CautionDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CautionColour = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CautionDescription2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CautionColour2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsRiskRating = table.Column<bool>(type: "bit", nullable: false),
                    MinValue = table.Column<int>(type: "int", nullable: false),
                    MaxValue = table.Column<int>(type: "int", nullable: false),
                    Increments = table.Column<double>(type: "float", nullable: false),
                    GroupingReference = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsCondition = table.Column<bool>(type: "bit", nullable: false),
                    ConditionFormDefinitionItemQuestionID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ConditionFormDefinitionItemQuestionResponseID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FormBuilderConditionActionID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedUserID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    EditUserID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EditDateTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormDefinitionItemSliders", x => x.FormDefinitionItemSliderID);
                });

            migrationBuilder.CreateTable(
                name: "FormDefinitionItemText",
                columns: table => new
                {
                    FormDefinitionItemTextID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsMultiline = table.Column<bool>(type: "bit", nullable: false),
                    IsMandatory = table.Column<bool>(type: "bit", nullable: false),
                    IsInstruction = table.Column<bool>(type: "bit", nullable: false),
                    InstructionText = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InstructionImageBlobName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GroupingReference = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsCondition = table.Column<bool>(type: "bit", nullable: false),
                    ConditionFormDefinitionItemQuestionID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ConditionFormDefinitionItemQuestionResponseID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FormBuilderConditionActionID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedUserID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    EditUserID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EditDateTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormDefinitionItemText", x => x.FormDefinitionItemTextID);
                });

            migrationBuilder.CreateTable(
                name: "FormBuilderFormDefinitionStandardResponseTypeAnswers",
                columns: table => new
                {
                    FormBuilderFormDefinitionStandardResponseTypeAnswerID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FormBuilderFormDefinitionStandardResponseTypeID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsFailure = table.Column<bool>(type: "bit", nullable: false),
                    WeightScore = table.Column<double>(type: "float", nullable: false),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false),
                    FormBuilderFormDefinitionLocationResponseTypeID = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormBuilderFormDefinitionStandardResponseTypeAnswers", x => x.FormBuilderFormDefinitionStandardResponseTypeAnswerID);
                    table.ForeignKey(
                        name: "FK_FormBuilderFormDefinitionStandardResponseTypeAnswers_FormBuilderFormDefinitionLocationResponseTypes_FormBuilderFormDefinitio~",
                        column: x => x.FormBuilderFormDefinitionLocationResponseTypeID,
                        principalTable: "FormBuilderFormDefinitionLocationResponseTypes",
                        principalColumn: "FormBuilderFormDefinitionLocationResponseTypeID");
                });

            migrationBuilder.CreateTable(
                name: "FormDefinitionItemDynamicResponses",
                columns: table => new
                {
                    FormDefinitionItemDynamicResponseID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FormDefinitionItemDynamicID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormDefinitionItemDynamicResponses", x => x.FormDefinitionItemDynamicResponseID);
                    table.ForeignKey(
                        name: "FK_FormDefinitionItemDynamicResponses_FormDefinitionItemDynamic_FormDefinitionItemDynamicID",
                        column: x => x.FormDefinitionItemDynamicID,
                        principalTable: "FormDefinitionItemDynamic",
                        principalColumn: "FormDefinitionItemDynamicID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FormDefinitionItemLocationResponses",
                columns: table => new
                {
                    FormDefinitionItemLocationResponseID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FormDefinitionItemLocationID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormDefinitionItemLocationResponses", x => x.FormDefinitionItemLocationResponseID);
                    table.ForeignKey(
                        name: "FK_FormDefinitionItemLocationResponses_FormDefinitionItemLocations_FormDefinitionItemLocationID",
                        column: x => x.FormDefinitionItemLocationID,
                        principalTable: "FormDefinitionItemLocations",
                        principalColumn: "FormDefinitionItemLocationID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FormDefinitionItemQuestionResponses",
                columns: table => new
                {
                    FormDefinitionItemQuestionResponseID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FormDefinitionItemQuestionID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsFailure = table.Column<bool>(type: "bit", nullable: false),
                    WeightScore = table.Column<double>(type: "float", nullable: false),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormDefinitionItemQuestionResponses", x => x.FormDefinitionItemQuestionResponseID);
                    table.ForeignKey(
                        name: "FK_FormDefinitionItemQuestionResponses_FormDefinitionItemQuestions_FormDefinitionItemQuestionID",
                        column: x => x.FormDefinitionItemQuestionID,
                        principalTable: "FormDefinitionItemQuestions",
                        principalColumn: "FormDefinitionItemQuestionID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FormBuilderFormDefinitionStandardResponseTypeAnswers_FormBuilderFormDefinitionLocationResponseTypeID",
                table: "FormBuilderFormDefinitionStandardResponseTypeAnswers",
                column: "FormBuilderFormDefinitionLocationResponseTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_FormDefinitionItemDynamicResponses_FormDefinitionItemDynamicID",
                table: "FormDefinitionItemDynamicResponses",
                column: "FormDefinitionItemDynamicID");

            migrationBuilder.CreateIndex(
                name: "IX_FormDefinitionItemLocationResponses_FormDefinitionItemLocationID",
                table: "FormDefinitionItemLocationResponses",
                column: "FormDefinitionItemLocationID");

            migrationBuilder.CreateIndex(
                name: "IX_FormDefinitionItemQuestionResponses_FormDefinitionItemQuestionID",
                table: "FormDefinitionItemQuestionResponses",
                column: "FormDefinitionItemQuestionID");

            migrationBuilder.CreateIndex(
                name: "IX_FormDefinitionItems_FormDefinitionID",
                table: "FormDefinitionItems",
                column: "FormDefinitionID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FormBuilderConditionActions");

            migrationBuilder.DropTable(
                name: "FormBuilderFormDefinitionLibrary");

            migrationBuilder.DropTable(
                name: "FormBuilderFormDefinitionNumberUOM");

            migrationBuilder.DropTable(
                name: "FormBuilderFormDefinitionStandardResponseTypeAnswers");

            migrationBuilder.DropTable(
                name: "FormBuilderFormDefinitionStandardResponseTypes");

            migrationBuilder.DropTable(
                name: "FormDefinitionItemCategory");

            migrationBuilder.DropTable(
                name: "FormDefinitionItemDateTimes");

            migrationBuilder.DropTable(
                name: "FormDefinitionItemDrawings");

            migrationBuilder.DropTable(
                name: "FormDefinitionItemDynamicResponses");

            migrationBuilder.DropTable(
                name: "FormDefinitionItemInformations");

            migrationBuilder.DropTable(
                name: "FormDefinitionItemLocationResponses");

            migrationBuilder.DropTable(
                name: "FormDefinitionItemNumbers");

            migrationBuilder.DropTable(
                name: "FormDefinitionItemPictures");

            migrationBuilder.DropTable(
                name: "FormDefinitionItemQuestionResponses");

            migrationBuilder.DropTable(
                name: "FormDefinitionItems");

            migrationBuilder.DropTable(
                name: "FormDefinitionItemSections");

            migrationBuilder.DropTable(
                name: "FormDefinitionItemSignatures");

            migrationBuilder.DropTable(
                name: "FormDefinitionItemSliders");

            migrationBuilder.DropTable(
                name: "FormDefinitionItemText");

            migrationBuilder.DropTable(
                name: "FormBuilderFormDefinitionLocationResponseTypes");

            migrationBuilder.DropTable(
                name: "FormDefinitionItemDynamic");

            migrationBuilder.DropTable(
                name: "FormDefinitionItemLocations");

            migrationBuilder.DropTable(
                name: "FormDefinitionItemQuestions");
        }
    }
}
