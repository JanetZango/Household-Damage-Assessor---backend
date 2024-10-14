using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ACM.Migrations
{
    public partial class DatabaseCleanup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LinkAcmAccessRoleFormDefinitions_FormDefinitions_FormDefinitionID",
                table: "LinkAcmAccessRoleFormDefinitions");

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
                name: "FormDefinitionItemPostSubmissionActions");

            migrationBuilder.DropTable(
                name: "FormDefinitionItemQuestionResponses");

            migrationBuilder.DropTable(
                name: "FormDefinitionItemSections");

            migrationBuilder.DropTable(
                name: "FormDefinitionItemSignatures");

            migrationBuilder.DropTable(
                name: "FormDefinitionItemSliders");

            migrationBuilder.DropTable(
                name: "FormDefinitionItemText");

            migrationBuilder.DropTable(
                name: "FormInstanceItemDateTime");

            migrationBuilder.DropTable(
                name: "FormInstanceItemDrawings");

            migrationBuilder.DropTable(
                name: "FormInstanceItemDynamic");

            migrationBuilder.DropTable(
                name: "FormInstanceItemLocations");

            migrationBuilder.DropTable(
                name: "FormInstanceItemNumbers");

            migrationBuilder.DropTable(
                name: "FormInstanceItemPictures");

            migrationBuilder.DropTable(
                name: "FormInstanceItemQuestions");

            migrationBuilder.DropTable(
                name: "FormInstanceItemSignatures");

            migrationBuilder.DropTable(
                name: "FormInstanceItemSliders");

            migrationBuilder.DropTable(
                name: "FormInstanceItemSupportingDocuments");

            migrationBuilder.DropTable(
                name: "FormInstanceItemTexts");

            migrationBuilder.DropTable(
                name: "FormBuilderFormDefinitionLocationResponseTypes");

            migrationBuilder.DropTable(
                name: "FormDefinitionItemDynamic");

            migrationBuilder.DropTable(
                name: "FormDefinitionItemLocations");

            migrationBuilder.DropTable(
                name: "FormBuilderQuestionPostSubmissionActions");

            migrationBuilder.DropTable(
                name: "FormDefinitionItemQuestions");

            migrationBuilder.DropTable(
                name: "FormInstanceItems");

            migrationBuilder.DropTable(
                name: "FormDefinitionItems");

            migrationBuilder.DropTable(
                name: "FormInstances");

            migrationBuilder.DropTable(
                name: "FormInstanceStates");

            migrationBuilder.DropTable(
                name: "GPSLocations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FormDefinitions",
                table: "FormDefinitions");

            migrationBuilder.RenameTable(
                name: "FormDefinitions",
                newName: "FormDefinition");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FormDefinition",
                table: "FormDefinition",
                column: "FormDefinitionID");

            migrationBuilder.AddForeignKey(
                name: "FK_LinkAcmAccessRoleFormDefinitions_FormDefinition_FormDefinitionID",
                table: "LinkAcmAccessRoleFormDefinitions",
                column: "FormDefinitionID",
                principalTable: "FormDefinition",
                principalColumn: "FormDefinitionID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LinkAcmAccessRoleFormDefinitions_FormDefinition_FormDefinitionID",
                table: "LinkAcmAccessRoleFormDefinitions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FormDefinition",
                table: "FormDefinition");

            migrationBuilder.RenameTable(
                name: "FormDefinition",
                newName: "FormDefinitions");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FormDefinitions",
                table: "FormDefinitions",
                column: "FormDefinitionID");

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
                    DisplayOrder = table.Column<int>(type: "int", nullable: false),
                    EventCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IconClass = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsAvailable = table.Column<bool>(type: "bit", nullable: false),
                    IsResponse = table.Column<bool>(type: "bit", nullable: false),
                    IsStructure = table.Column<bool>(type: "bit", nullable: false)
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
                name: "FormBuilderQuestionPostSubmissionActions",
                columns: table => new
                {
                    FormBuilderQuestionPostSubmissionActionID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EventCode = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormBuilderQuestionPostSubmissionActions", x => x.FormBuilderQuestionPostSubmissionActionID);
                });

            migrationBuilder.CreateTable(
                name: "FormDefinitionItemCategory",
                columns: table => new
                {
                    FormDefinitionItemCategoryID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedUserID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EditDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EditUserID = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
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
                    AllowPastDates = table.Column<bool>(type: "bit", nullable: false),
                    AllowPastTimes = table.Column<bool>(type: "bit", nullable: false),
                    CaptureDate = table.Column<bool>(type: "bit", nullable: false),
                    CaptureTime = table.Column<bool>(type: "bit", nullable: false),
                    CautionColour = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CautionColour2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CautionDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CautionDescription2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConditionFormDefinitionItemQuestionID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ConditionFormDefinitionItemQuestionResponseID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedUserID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EditDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EditUserID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FormBuilderConditionActionID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GroupingReference = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InstructionImageBlobName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InstructionText = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsCaution = table.Column<bool>(type: "bit", nullable: false),
                    IsCondition = table.Column<bool>(type: "bit", nullable: false),
                    IsInstruction = table.Column<bool>(type: "bit", nullable: false),
                    IsMandatory = table.Column<bool>(type: "bit", nullable: false)
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
                    ConditionFormDefinitionItemQuestionID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ConditionFormDefinitionItemQuestionResponseID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedUserID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EditDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EditUserID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FormBuilderConditionActionID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GroupingReference = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InstructionImageBlobName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InstructionText = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsCondition = table.Column<bool>(type: "bit", nullable: false),
                    IsInstruction = table.Column<bool>(type: "bit", nullable: false),
                    IsMandatory = table.Column<bool>(type: "bit", nullable: false)
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
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedUserID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EditDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EditUserID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    GroupingReference = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsMandatory = table.Column<bool>(type: "bit", nullable: false),
                    IsPredefinedItems = table.Column<bool>(type: "bit", nullable: false),
                    IsRepeatDynamic = table.Column<bool>(type: "bit", nullable: false),
                    MinDynamicRepeat = table.Column<int>(type: "int", nullable: false),
                    NameOfItemRepeat = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                    ConditionFormDefinitionItemQuestionID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ConditionFormDefinitionItemQuestionResponseID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedUserID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    EditDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EditUserID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FormBuilderConditionActionID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InstructionImageBlobName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InstructionText = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsCondition = table.Column<bool>(type: "bit", nullable: false)
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
                    ConditionFormDefinitionItemQuestionID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ConditionFormDefinitionItemQuestionResponseID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedUserID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EditDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EditUserID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FormBuilderConditionActionID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FormBuilderFormDefinitionLocationResponseTypeID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    GroupingReference = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InstructionImageBlobName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InstructionText = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsCondition = table.Column<bool>(type: "bit", nullable: false),
                    IsInstruction = table.Column<bool>(type: "bit", nullable: false),
                    IsLocationList = table.Column<bool>(type: "bit", nullable: false),
                    IsMandatory = table.Column<bool>(type: "bit", nullable: false),
                    IsTextInput = table.Column<bool>(type: "bit", nullable: false)
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
                    CautionColour = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CautionColour2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CautionDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CautionDescription2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConditionFormDefinitionItemQuestionID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ConditionFormDefinitionItemQuestionResponseID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedUserID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DisplayPreviousMeasurement = table.Column<bool>(type: "bit", nullable: false),
                    EditDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EditUserID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FormBuilderConditionActionID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GroupingReference = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InstructionImageBlobName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InstructionText = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsCaution = table.Column<bool>(type: "bit", nullable: false),
                    IsCondition = table.Column<bool>(type: "bit", nullable: false),
                    IsInstruction = table.Column<bool>(type: "bit", nullable: false),
                    IsMandatory = table.Column<bool>(type: "bit", nullable: false),
                    IsSystemMeasurement = table.Column<bool>(type: "bit", nullable: false),
                    IsTolerance = table.Column<bool>(type: "bit", nullable: false),
                    MeasureIdentifier = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ToleranceExceededPrompt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ToleranceRangeFrom = table.Column<double>(type: "float", nullable: false),
                    ToleranceRangeTo = table.Column<double>(type: "float", nullable: false),
                    TolleranceComparisonOperator = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UnitOfMeasure = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                    AllowMultipleUploads = table.Column<bool>(type: "bit", nullable: false),
                    ConditionFormDefinitionItemQuestionID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ConditionFormDefinitionItemQuestionResponseID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedUserID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EditDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EditUserID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FormBuilderConditionActionID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GroupingReference = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InstructionImageBlobName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InstructionText = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsCondition = table.Column<bool>(type: "bit", nullable: false),
                    IsInstruction = table.Column<bool>(type: "bit", nullable: false),
                    IsMandatory = table.Column<bool>(type: "bit", nullable: false)
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
                    CautionColour = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CautionColour2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CautionDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CautionDescription2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConditionFormDefinitionItemQuestionID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ConditionFormDefinitionItemQuestionResponseID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedUserID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EditDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EditUserID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FormBuilderConditionActionID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FormBuilderFormDefinitionStandardResponseTypeID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    GroupingReference = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InstructionImageBlobName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InstructionText = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsCaution = table.Column<bool>(type: "bit", nullable: false),
                    IsCondition = table.Column<bool>(type: "bit", nullable: false),
                    IsInstruction = table.Column<bool>(type: "bit", nullable: false),
                    IsList = table.Column<bool>(type: "bit", nullable: false),
                    IsListMultiSelect = table.Column<bool>(type: "bit", nullable: false),
                    IsMandatory = table.Column<bool>(type: "bit", nullable: false),
                    IsNotify = table.Column<bool>(type: "bit", nullable: false),
                    IsRiskRating = table.Column<bool>(type: "bit", nullable: false),
                    NotificationAnswerID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NotificationEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ScoringWeight = table.Column<double>(type: "float", nullable: false)
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
                    FormDefinitionID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ChildCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChildID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedUserID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    EditDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EditUserID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Grouping = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsMandatory = table.Column<bool>(type: "bit", nullable: false),
                    ParentCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ParentID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SequenceNumber = table.Column<int>(type: "int", nullable: false)
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
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedUserID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EditDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EditUserID = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
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
                    ConditionFormDefinitionItemQuestionID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ConditionFormDefinitionItemQuestionResponseID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedUserID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EditDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EditUserID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FormBuilderConditionActionID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GroupingReference = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InstructionImageBlobName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InstructionText = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsCondition = table.Column<bool>(type: "bit", nullable: false),
                    IsInstruction = table.Column<bool>(type: "bit", nullable: false),
                    IsMandatory = table.Column<bool>(type: "bit", nullable: false)
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
                    CautionColour = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CautionColour2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CautionDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CautionDescription2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConditionFormDefinitionItemQuestionID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ConditionFormDefinitionItemQuestionResponseID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedUserID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EditDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EditUserID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FormBuilderConditionActionID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GroupingReference = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Increments = table.Column<double>(type: "float", nullable: false),
                    InstructionImageBlobName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InstructionText = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsCaution = table.Column<bool>(type: "bit", nullable: false),
                    IsCondition = table.Column<bool>(type: "bit", nullable: false),
                    IsInstruction = table.Column<bool>(type: "bit", nullable: false),
                    IsMandatory = table.Column<bool>(type: "bit", nullable: false),
                    IsRiskRating = table.Column<bool>(type: "bit", nullable: false),
                    MaxValue = table.Column<int>(type: "int", nullable: false),
                    MinValue = table.Column<int>(type: "int", nullable: false)
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
                    ConditionFormDefinitionItemQuestionID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ConditionFormDefinitionItemQuestionResponseID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedUserID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EditDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EditUserID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FormBuilderConditionActionID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GroupingReference = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InstructionImageBlobName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InstructionText = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsCondition = table.Column<bool>(type: "bit", nullable: false),
                    IsInstruction = table.Column<bool>(type: "bit", nullable: false),
                    IsMandatory = table.Column<bool>(type: "bit", nullable: false),
                    IsMultiline = table.Column<bool>(type: "bit", nullable: false),
                    IsPersonLookupField = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormDefinitionItemText", x => x.FormDefinitionItemTextID);
                });

            migrationBuilder.CreateTable(
                name: "FormInstanceStates",
                columns: table => new
                {
                    FormInstanceStateID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedUserID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EditDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EditUserID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    EventCode = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormInstanceStates", x => x.FormInstanceStateID);
                });

            migrationBuilder.CreateTable(
                name: "GPSLocations",
                columns: table => new
                {
                    GPSLocationID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Accuracy = table.Column<double>(type: "float", nullable: false),
                    Altitude = table.Column<double>(type: "float", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedUserID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    EditDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EditUserID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Latitude = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Longitude = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GPSLocations", x => x.GPSLocationID);
                });

            migrationBuilder.CreateTable(
                name: "FormBuilderFormDefinitionStandardResponseTypeAnswers",
                columns: table => new
                {
                    FormBuilderFormDefinitionStandardResponseTypeAnswerID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FormBuilderFormDefinitionLocationResponseTypeID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false),
                    FormBuilderFormDefinitionStandardResponseTypeID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsFailure = table.Column<bool>(type: "bit", nullable: false),
                    WeightScore = table.Column<double>(type: "float", nullable: false)
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
                    DisplayOrder = table.Column<int>(type: "int", nullable: false),
                    IsFailure = table.Column<bool>(type: "bit", nullable: false),
                    WeightScore = table.Column<double>(type: "float", nullable: false)
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

            migrationBuilder.CreateTable(
                name: "FormDefinitionItemPostSubmissionActions",
                columns: table => new
                {
                    FormDefinitionItemPostSubmissionActionID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FormBuilderQuestionPostSubmissionActionID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FormDefinitionItemID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BasicFieldEventCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FieldName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormDefinitionItemPostSubmissionActions", x => x.FormDefinitionItemPostSubmissionActionID);
                    table.ForeignKey(
                        name: "FK_FormDefinitionItemPostSubmissionActions_FormBuilderQuestionPostSubmissionActions_FormBuilderQuestionPostSubmissionActionID",
                        column: x => x.FormBuilderQuestionPostSubmissionActionID,
                        principalTable: "FormBuilderQuestionPostSubmissionActions",
                        principalColumn: "FormBuilderQuestionPostSubmissionActionID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FormDefinitionItemPostSubmissionActions_FormDefinitionItems_FormDefinitionItemID",
                        column: x => x.FormDefinitionItemID,
                        principalTable: "FormDefinitionItems",
                        principalColumn: "FormDefinitionItemID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FormInstances",
                columns: table => new
                {
                    FormInstanceID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedUserID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FormDefinitionID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FormInstanceStateID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GPSLocationID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ApprovedBySignatureBlobName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ApprovedByUserID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ApprovedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Comments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EditDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EditUserID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FBItemsAnswered = table.Column<int>(type: "int", nullable: true),
                    FBItemsInOrder = table.Column<int>(type: "int", nullable: true),
                    FBItemsNotInOrder = table.Column<int>(type: "int", nullable: true),
                    FBItemsTotal = table.Column<int>(type: "int", nullable: true),
                    FormCaptureEndDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FormCaptureStartDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FormExecutionDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    InstanceNumber = table.Column<long>(type: "bigint", nullable: false),
                    IsCompleted = table.Column<bool>(type: "bit", nullable: false),
                    IsRemoved = table.Column<bool>(type: "bit", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormInstances", x => x.FormInstanceID);
                    table.ForeignKey(
                        name: "FK_FormInstances_FormDefinitions_FormDefinitionID",
                        column: x => x.FormDefinitionID,
                        principalTable: "FormDefinitions",
                        principalColumn: "FormDefinitionID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FormInstances_FormInstanceStates_FormInstanceStateID",
                        column: x => x.FormInstanceStateID,
                        principalTable: "FormInstanceStates",
                        principalColumn: "FormInstanceStateID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FormInstances_GPSLocations_GPSLocationID",
                        column: x => x.GPSLocationID,
                        principalTable: "GPSLocations",
                        principalColumn: "GPSLocationID");
                    table.ForeignKey(
                        name: "FK_FormInstances_Users_CreatedUserID",
                        column: x => x.CreatedUserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FormInstanceItems",
                columns: table => new
                {
                    FormInstanceItemID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FormDefinitionItemID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FormInstanceID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ChildCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedUserID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    EditDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EditUserID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FBIsAnswered = table.Column<bool>(type: "bit", nullable: false),
                    FBIsInOrder = table.Column<bool>(type: "bit", nullable: false),
                    FBIsNotInOrder = table.Column<bool>(type: "bit", nullable: false),
                    FormInstanceItemDynamicID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormInstanceItems", x => x.FormInstanceItemID);
                    table.ForeignKey(
                        name: "FK_FormInstanceItems_FormDefinitionItems_FormDefinitionItemID",
                        column: x => x.FormDefinitionItemID,
                        principalTable: "FormDefinitionItems",
                        principalColumn: "FormDefinitionItemID");
                    table.ForeignKey(
                        name: "FK_FormInstanceItems_FormInstances_FormInstanceID",
                        column: x => x.FormInstanceID,
                        principalTable: "FormInstances",
                        principalColumn: "FormInstanceID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FormInstanceItemDateTime",
                columns: table => new
                {
                    FormInstanceItemDateTimeID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FormInstanceItemID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DateValue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TimeValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormInstanceItemDateTime", x => x.FormInstanceItemDateTimeID);
                    table.ForeignKey(
                        name: "FK_FormInstanceItemDateTime_FormInstanceItems_FormInstanceItemID",
                        column: x => x.FormInstanceItemID,
                        principalTable: "FormInstanceItems",
                        principalColumn: "FormInstanceItemID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FormInstanceItemDrawings",
                columns: table => new
                {
                    FormInstanceItemDrawingID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FormInstanceItemID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BlobName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SerializedPoints = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormInstanceItemDrawings", x => x.FormInstanceItemDrawingID);
                    table.ForeignKey(
                        name: "FK_FormInstanceItemDrawings_FormInstanceItems_FormInstanceItemID",
                        column: x => x.FormInstanceItemID,
                        principalTable: "FormInstanceItems",
                        principalColumn: "FormInstanceItemID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FormInstanceItemDynamic",
                columns: table => new
                {
                    FormInstanceItemDynamicID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FormInstanceItemID1 = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false),
                    FormDefinitionItemDynamicResponseID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FormInstanceItemID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ItemName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormInstanceItemDynamic", x => x.FormInstanceItemDynamicID);
                    table.ForeignKey(
                        name: "FK_FormInstanceItemDynamic_FormInstanceItems_FormInstanceItemID1",
                        column: x => x.FormInstanceItemID1,
                        principalTable: "FormInstanceItems",
                        principalColumn: "FormInstanceItemID");
                });

            migrationBuilder.CreateTable(
                name: "FormInstanceItemLocations",
                columns: table => new
                {
                    FormInstanceItemLocationID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FormInstanceItemID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FormDefinitionItemLocationResponseID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LocationText = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormInstanceItemLocations", x => x.FormInstanceItemLocationID);
                    table.ForeignKey(
                        name: "FK_FormInstanceItemLocations_FormInstanceItems_FormInstanceItemID",
                        column: x => x.FormInstanceItemID,
                        principalTable: "FormInstanceItems",
                        principalColumn: "FormInstanceItemID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FormInstanceItemNumbers",
                columns: table => new
                {
                    FormInstanceItemNumberID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FormInstanceItemID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NumberValue = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormInstanceItemNumbers", x => x.FormInstanceItemNumberID);
                    table.ForeignKey(
                        name: "FK_FormInstanceItemNumbers_FormInstanceItems_FormInstanceItemID",
                        column: x => x.FormInstanceItemID,
                        principalTable: "FormInstanceItems",
                        principalColumn: "FormInstanceItemID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FormInstanceItemPictures",
                columns: table => new
                {
                    FormInstanceItemPictureID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FormInstanceItemID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BlobName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormInstanceItemPictures", x => x.FormInstanceItemPictureID);
                    table.ForeignKey(
                        name: "FK_FormInstanceItemPictures_FormInstanceItems_FormInstanceItemID",
                        column: x => x.FormInstanceItemID,
                        principalTable: "FormInstanceItems",
                        principalColumn: "FormInstanceItemID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FormInstanceItemQuestions",
                columns: table => new
                {
                    FormInstanceItemQuestionID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FormInstanceItemID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FormDefinitionItemQuestionResponseID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormInstanceItemQuestions", x => x.FormInstanceItemQuestionID);
                    table.ForeignKey(
                        name: "FK_FormInstanceItemQuestions_FormInstanceItems_FormInstanceItemID",
                        column: x => x.FormInstanceItemID,
                        principalTable: "FormInstanceItems",
                        principalColumn: "FormInstanceItemID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FormInstanceItemSignatures",
                columns: table => new
                {
                    FormInstanceItemSignatureID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FormInstanceItemID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BlobName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormInstanceItemSignatures", x => x.FormInstanceItemSignatureID);
                    table.ForeignKey(
                        name: "FK_FormInstanceItemSignatures_FormInstanceItems_FormInstanceItemID",
                        column: x => x.FormInstanceItemID,
                        principalTable: "FormInstanceItems",
                        principalColumn: "FormInstanceItemID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FormInstanceItemSliders",
                columns: table => new
                {
                    FormInstanceItemSliderID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FormInstanceItemID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SliderValue = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormInstanceItemSliders", x => x.FormInstanceItemSliderID);
                    table.ForeignKey(
                        name: "FK_FormInstanceItemSliders_FormInstanceItems_FormInstanceItemID",
                        column: x => x.FormInstanceItemID,
                        principalTable: "FormInstanceItems",
                        principalColumn: "FormInstanceItemID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FormInstanceItemSupportingDocuments",
                columns: table => new
                {
                    FormInstanceItemSupportingDocumentID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FormInstanceItemID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BlobName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContentType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedUserID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    EditDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EditUserID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormInstanceItemSupportingDocuments", x => x.FormInstanceItemSupportingDocumentID);
                    table.ForeignKey(
                        name: "FK_FormInstanceItemSupportingDocuments_FormInstanceItems_FormInstanceItemID",
                        column: x => x.FormInstanceItemID,
                        principalTable: "FormInstanceItems",
                        principalColumn: "FormInstanceItemID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FormInstanceItemTexts",
                columns: table => new
                {
                    FormInstanceItemTextID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FormInstanceItemID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TextValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormInstanceItemTexts", x => x.FormInstanceItemTextID);
                    table.ForeignKey(
                        name: "FK_FormInstanceItemTexts_FormInstanceItems_FormInstanceItemID",
                        column: x => x.FormInstanceItemID,
                        principalTable: "FormInstanceItems",
                        principalColumn: "FormInstanceItemID",
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
                name: "IX_FormDefinitionItemPostSubmissionActions_FormBuilderQuestionPostSubmissionActionID",
                table: "FormDefinitionItemPostSubmissionActions",
                column: "FormBuilderQuestionPostSubmissionActionID");

            migrationBuilder.CreateIndex(
                name: "IX_FormDefinitionItemPostSubmissionActions_FormDefinitionItemID",
                table: "FormDefinitionItemPostSubmissionActions",
                column: "FormDefinitionItemID");

            migrationBuilder.CreateIndex(
                name: "IX_FormDefinitionItemQuestionResponses_FormDefinitionItemQuestionID",
                table: "FormDefinitionItemQuestionResponses",
                column: "FormDefinitionItemQuestionID");

            migrationBuilder.CreateIndex(
                name: "IX_FormDefinitionItems_FormDefinitionID",
                table: "FormDefinitionItems",
                column: "FormDefinitionID");

            migrationBuilder.CreateIndex(
                name: "IX_FormInstanceItemDateTime_FormInstanceItemID",
                table: "FormInstanceItemDateTime",
                column: "FormInstanceItemID");

            migrationBuilder.CreateIndex(
                name: "IX_FormInstanceItemDrawings_FormInstanceItemID",
                table: "FormInstanceItemDrawings",
                column: "FormInstanceItemID");

            migrationBuilder.CreateIndex(
                name: "IX_FormInstanceItemDynamic_FormInstanceItemID1",
                table: "FormInstanceItemDynamic",
                column: "FormInstanceItemID1");

            migrationBuilder.CreateIndex(
                name: "IX_FormInstanceItemLocations_FormInstanceItemID",
                table: "FormInstanceItemLocations",
                column: "FormInstanceItemID");

            migrationBuilder.CreateIndex(
                name: "IX_FormInstanceItemNumbers_FormInstanceItemID",
                table: "FormInstanceItemNumbers",
                column: "FormInstanceItemID");

            migrationBuilder.CreateIndex(
                name: "IX_FormInstanceItemPictures_FormInstanceItemID",
                table: "FormInstanceItemPictures",
                column: "FormInstanceItemID");

            migrationBuilder.CreateIndex(
                name: "IX_FormInstanceItemQuestions_FormInstanceItemID",
                table: "FormInstanceItemQuestions",
                column: "FormInstanceItemID");

            migrationBuilder.CreateIndex(
                name: "IX_FormInstanceItems_FormDefinitionItemID",
                table: "FormInstanceItems",
                column: "FormDefinitionItemID");

            migrationBuilder.CreateIndex(
                name: "IX_FormInstanceItems_FormInstanceID",
                table: "FormInstanceItems",
                column: "FormInstanceID");

            migrationBuilder.CreateIndex(
                name: "IX_FormInstanceItemSignatures_FormInstanceItemID",
                table: "FormInstanceItemSignatures",
                column: "FormInstanceItemID");

            migrationBuilder.CreateIndex(
                name: "IX_FormInstanceItemSliders_FormInstanceItemID",
                table: "FormInstanceItemSliders",
                column: "FormInstanceItemID");

            migrationBuilder.CreateIndex(
                name: "IX_FormInstanceItemSupportingDocuments_FormInstanceItemID",
                table: "FormInstanceItemSupportingDocuments",
                column: "FormInstanceItemID");

            migrationBuilder.CreateIndex(
                name: "IX_FormInstanceItemTexts_FormInstanceItemID",
                table: "FormInstanceItemTexts",
                column: "FormInstanceItemID");

            migrationBuilder.CreateIndex(
                name: "IX_FormInstances_CreatedUserID",
                table: "FormInstances",
                column: "CreatedUserID");

            migrationBuilder.CreateIndex(
                name: "IX_FormInstances_FormDefinitionID",
                table: "FormInstances",
                column: "FormDefinitionID");

            migrationBuilder.CreateIndex(
                name: "IX_FormInstances_FormInstanceStateID",
                table: "FormInstances",
                column: "FormInstanceStateID");

            migrationBuilder.CreateIndex(
                name: "IX_FormInstances_GPSLocationID",
                table: "FormInstances",
                column: "GPSLocationID");

            migrationBuilder.AddForeignKey(
                name: "FK_LinkAcmAccessRoleFormDefinitions_FormDefinitions_FormDefinitionID",
                table: "LinkAcmAccessRoleFormDefinitions",
                column: "FormDefinitionID",
                principalTable: "FormDefinitions",
                principalColumn: "FormDefinitionID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
