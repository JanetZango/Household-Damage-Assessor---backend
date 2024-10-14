using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ACM.Migrations
{
    public partial class RemoveTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_AcmAccessRoles_AcmAccessRoleID",
                table: "Users");

            migrationBuilder.DropTable(
                name: "EquipmentChecklistDetails");

            migrationBuilder.DropTable(
                name: "EquipmentChecklistHeaders");

            migrationBuilder.DropTable(
                name: "Equipments");

            migrationBuilder.DropTable(
                name: "Establishments");

            migrationBuilder.DropTable(
                name: "Leads");

            migrationBuilder.DropTable(
                name: "LinkAcmAccessRoleAcmRoles");

            migrationBuilder.DropTable(
                name: "LinkAcmAccessRoleFormDefinitions");

            migrationBuilder.DropTable(
                name: "Organisations");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "ScheduledInstallations");

            migrationBuilder.DropTable(
                name: "VehicleChecklistDescriptions");

            migrationBuilder.DropTable(
                name: "VehicleChecklistDetails");

            migrationBuilder.DropTable(
                name: "VehicleChecklistHeaders");

            migrationBuilder.DropTable(
                name: "Vehicles");

            migrationBuilder.DropTable(
                name: "Vouchers");

            migrationBuilder.DropTable(
                name: "AcmRoles");

            migrationBuilder.DropTable(
                name: "AcmAccessRoles");

            migrationBuilder.DropTable(
                name: "FormDefinition");

            migrationBuilder.DropIndex(
                name: "IX_Users_AcmAccessRoleID",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "AcmAccessRoleID",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "OrganisationID",
                table: "Users");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "AcmAccessRoleID",
                table: "Users",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "OrganisationID",
                table: "Users",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AcmAccessRoles",
                columns: table => new
                {
                    AcmAccessRoleID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AcmAccessRoles", x => x.AcmAccessRoleID);
                });

            migrationBuilder.CreateTable(
                name: "AcmRoles",
                columns: table => new
                {
                    AcmRoleID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EventCode = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AcmRoles", x => x.AcmRoleID);
                });

            migrationBuilder.CreateTable(
                name: "EquipmentChecklistDetails",
                columns: table => new
                {
                    EquipmentChecklistDetailID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedUserID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    EditDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EditUserID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    EquipmentChecklistHeaderID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    EquipmentID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EquipmentChecklistDetails", x => x.EquipmentChecklistDetailID);
                });

            migrationBuilder.CreateTable(
                name: "EquipmentChecklistHeaders",
                columns: table => new
                {
                    EquipmentChecklistHeaderID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Comments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedUserID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    EditDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EditUserID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VehicleID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EquipmentChecklistHeaders", x => x.EquipmentChecklistHeaderID);
                });

            migrationBuilder.CreateTable(
                name: "Equipments",
                columns: table => new
                {
                    EquipmentID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedUserID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EditDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EditUserID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Minimum = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Equipments", x => x.EquipmentID);
                });

            migrationBuilder.CreateTable(
                name: "Establishments",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContactName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ContactNo = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    DateCaptured = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    DcName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EstablishmentDesc = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    EstablishmentName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Latitude = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Longitude = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Meters = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MnName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Establishments", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "FormDefinition",
                columns: table => new
                {
                    FormDefinitionID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AllowSignatures = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedUserID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DynamicTemplateInstanceID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DynamicTemplateInstanceIDCLInst = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    EditDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EditUserID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    EffectiveEndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EffectiveStartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    InstanceNumber = table.Column<long>(type: "bigint", nullable: false),
                    InstructionsFormatted = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsApprovalRequired = table.Column<bool>(type: "bit", nullable: false),
                    IsLatest = table.Column<bool>(type: "bit", nullable: false),
                    IsLocationRelevant = table.Column<bool>(type: "bit", nullable: false),
                    Tags = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Version = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormDefinition", x => x.FormDefinitionID);
                });

            migrationBuilder.CreateTable(
                name: "Leads",
                columns: table => new
                {
                    LeadsID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AMID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CellphoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Complex = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FttrEnable = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GatedCategories = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HpCount = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsOnJobCard = table.Column<bool>(type: "bit", nullable: true),
                    Latitude = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Longitude = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PropertyType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Province = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ScheduledForInstallation = table.Column<bool>(type: "bit", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Str_Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Street = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Street_Number = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Suburn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Town = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WebConnect = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Leads", x => x.LeadsID);
                });

            migrationBuilder.CreateTable(
                name: "Organisations",
                columns: table => new
                {
                    OrganisationID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ContactNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EditDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EditUserID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Latitude = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Longitude = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Meters = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrganisationAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrganisationName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organisations", x => x.OrganisationID);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Createdby = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    ProductDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Userid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductID);
                });

            migrationBuilder.CreateTable(
                name: "ScheduledInstallations",
                columns: table => new
                {
                    ScheduledInstallationID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Comments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InstallationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsInstalled = table.Column<bool>(type: "bit", nullable: false),
                    LeadID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReferenceNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TechnicianID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScheduledInstallations", x => x.ScheduledInstallationID);
                });

            migrationBuilder.CreateTable(
                name: "VehicleChecklistDescriptions",
                columns: table => new
                {
                    VehicleChecklistDescriptionID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedUserID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EditDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EditUserID = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleChecklistDescriptions", x => x.VehicleChecklistDescriptionID);
                });

            migrationBuilder.CreateTable(
                name: "VehicleChecklistDetails",
                columns: table => new
                {
                    VehicleChecklistDetailID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Comments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedUserID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    EditDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EditUserID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    VehicleChecklistDescriptionID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VehicleChecklistHeaderID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleChecklistDetails", x => x.VehicleChecklistDetailID);
                });

            migrationBuilder.CreateTable(
                name: "VehicleChecklistHeaders",
                columns: table => new
                {
                    VehicleChecklistHeaderID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Comments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedUserID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    EditDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EditUserID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VehicleID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleChecklistHeaders", x => x.VehicleChecklistHeaderID);
                });

            migrationBuilder.CreateTable(
                name: "Vehicles",
                columns: table => new
                {
                    VehicleID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedUserID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    EditDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EditUserID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Make = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RegistrationNumber = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicles", x => x.VehicleID);
                });

            migrationBuilder.CreateTable(
                name: "Vouchers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CapturedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ExpiryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Reference = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VoucherCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VoucherPlan = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vouchers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LinkAcmAccessRoleAcmRoles",
                columns: table => new
                {
                    LinkAcmAccessRoleAcmRoleID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AcmAccessRoleID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AcmRoleID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LinkAcmAccessRoleAcmRoles", x => x.LinkAcmAccessRoleAcmRoleID);
                    table.ForeignKey(
                        name: "FK_LinkAcmAccessRoleAcmRoles_AcmAccessRoles_AcmAccessRoleID",
                        column: x => x.AcmAccessRoleID,
                        principalTable: "AcmAccessRoles",
                        principalColumn: "AcmAccessRoleID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LinkAcmAccessRoleAcmRoles_AcmRoles_AcmRoleID",
                        column: x => x.AcmRoleID,
                        principalTable: "AcmRoles",
                        principalColumn: "AcmRoleID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LinkAcmAccessRoleFormDefinitions",
                columns: table => new
                {
                    LinkAcmAccessRoleFormDefinitionID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AcmAccessRoleID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FormDefinitionID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LinkAcmAccessRoleFormDefinitions", x => x.LinkAcmAccessRoleFormDefinitionID);
                    table.ForeignKey(
                        name: "FK_LinkAcmAccessRoleFormDefinitions_AcmAccessRoles_AcmAccessRoleID",
                        column: x => x.AcmAccessRoleID,
                        principalTable: "AcmAccessRoles",
                        principalColumn: "AcmAccessRoleID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LinkAcmAccessRoleFormDefinitions_FormDefinition_FormDefinitionID",
                        column: x => x.FormDefinitionID,
                        principalTable: "FormDefinition",
                        principalColumn: "FormDefinitionID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_AcmAccessRoleID",
                table: "Users",
                column: "AcmAccessRoleID");

            migrationBuilder.CreateIndex(
                name: "IX_LinkAcmAccessRoleAcmRoles_AcmAccessRoleID",
                table: "LinkAcmAccessRoleAcmRoles",
                column: "AcmAccessRoleID");

            migrationBuilder.CreateIndex(
                name: "IX_LinkAcmAccessRoleAcmRoles_AcmRoleID",
                table: "LinkAcmAccessRoleAcmRoles",
                column: "AcmRoleID");

            migrationBuilder.CreateIndex(
                name: "IX_LinkAcmAccessRoleFormDefinitions_AcmAccessRoleID",
                table: "LinkAcmAccessRoleFormDefinitions",
                column: "AcmAccessRoleID");

            migrationBuilder.CreateIndex(
                name: "IX_LinkAcmAccessRoleFormDefinitions_FormDefinitionID",
                table: "LinkAcmAccessRoleFormDefinitions",
                column: "FormDefinitionID");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_AcmAccessRoles_AcmAccessRoleID",
                table: "Users",
                column: "AcmAccessRoleID",
                principalTable: "AcmAccessRoles",
                principalColumn: "AcmAccessRoleID");
        }
    }
}
