using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ACM.Migrations
{
    public partial class FrmInst : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FormInstanceStates",
                columns: table => new
                {
                    FormInstanceStateID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EventCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedUserID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    EditUserID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EditDateTime = table.Column<DateTime>(type: "datetime2", nullable: true)
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
                    Latitude = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Longitude = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Accuracy = table.Column<double>(type: "float", nullable: false),
                    Altitude = table.Column<double>(type: "float", nullable: false),
                    CreatedUserID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    EditUserID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EditDateTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GPSLocations", x => x.GPSLocationID);
                });

            migrationBuilder.CreateTable(
                name: "FormInstances",
                columns: table => new
                {
                    FormInstanceID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CheckListExecutionDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CheckListCaptureStartDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CheckListCaptureEndDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Comments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FormDefinitionID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FormInstanceStateID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InstanceNumber = table.Column<long>(type: "bigint", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GPSLocationID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ApprovedByUserID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ApprovedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ApprovedBySignatureBlobName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsCompleted = table.Column<bool>(type: "bit", nullable: false),
                    IsRemoved = table.Column<bool>(type: "bit", nullable: false),
                    FBItemsTotal = table.Column<int>(type: "int", nullable: true),
                    FBItemsAnswered = table.Column<int>(type: "int", nullable: true),
                    FBItemsInOrder = table.Column<int>(type: "int", nullable: true),
                    FBItemsNotInOrder = table.Column<int>(type: "int", nullable: true),
                    CreatedUserID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EditUserID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EditDateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
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
                });

            migrationBuilder.CreateTable(
                name: "FormInstanceItems",
                columns: table => new
                {
                    FormInstanceItemID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FormInstanceID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FormDefinitionItemID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FormInstanceItemDynamicID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ChildCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FBIsAnswered = table.Column<bool>(type: "bit", nullable: false),
                    FBIsInOrder = table.Column<bool>(type: "bit", nullable: false),
                    FBIsNotInOrder = table.Column<bool>(type: "bit", nullable: false),
                    CreatedUserID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    EditUserID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EditDateTime = table.Column<DateTime>(type: "datetime2", nullable: true)
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
                    FormInstanceItemID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FormDefinitionItemDynamicResponseID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ItemName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false),
                    FormInstanceItemID1 = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
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
                    LocationText = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FormDefinitionItemLocationResponseID = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
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
                    TenantID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
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
                    TenantID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
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
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedUserID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    EditUserID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EditDateTime = table.Column<DateTime>(type: "datetime2", nullable: true)
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
                name: "FormInstanceItems");

            migrationBuilder.DropTable(
                name: "FormInstances");

            migrationBuilder.DropTable(
                name: "FormInstanceStates");

            migrationBuilder.DropTable(
                name: "GPSLocations");
        }
    }
}
