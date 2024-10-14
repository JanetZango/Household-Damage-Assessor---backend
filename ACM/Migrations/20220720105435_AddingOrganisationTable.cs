using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ACM.Migrations
{
    public partial class AddingOrganisationTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AcmPersonBasicFields");

            migrationBuilder.DropTable(
                name: "AcmPersonMetaData");

            migrationBuilder.DropTable(
                name: "AcmPersons");

            migrationBuilder.CreateTable(
                name: "Organisations",
                columns: table => new
                {
                    OrganisationID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrganisationName = table.Column<string>(type: "nvarchar(120)", nullable: true),
                    OrganisationAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContactNo = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    Latitude = table.Column<string>(type: "nvarchar(80)", nullable: true),
                    Longitude = table.Column<string>(type: "nvarchar(80)", nullable: true),
                    Meters = table.Column<string>(type: "nvarchar(80)", nullable: true),
                    EditUserID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EditDateTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organisations", x => x.OrganisationID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Organisations");

            migrationBuilder.CreateTable(
                name: "AcmPersonBasicFields",
                columns: table => new
                {
                    AcmPersonBasicFieldID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EventCode = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AcmPersonBasicFields", x => x.AcmPersonBasicFieldID);
                });

            migrationBuilder.CreateTable(
                name: "AcmPersons",
                columns: table => new
                {
                    AcmPersonID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CountryID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    EthnicityID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    GenderID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ProvinceID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedUserID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CurrentPlacement = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EditDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EditUserID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IDNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PassportNumber = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AcmPersons", x => x.AcmPersonID);
                    table.ForeignKey(
                        name: "FK_AcmPersons_Countries_CountryID",
                        column: x => x.CountryID,
                        principalTable: "Countries",
                        principalColumn: "CountryID");
                    table.ForeignKey(
                        name: "FK_AcmPersons_Ethnicities_EthnicityID",
                        column: x => x.EthnicityID,
                        principalTable: "Ethnicities",
                        principalColumn: "EthnicityID");
                    table.ForeignKey(
                        name: "FK_AcmPersons_Genders_GenderID",
                        column: x => x.GenderID,
                        principalTable: "Genders",
                        principalColumn: "GenderID");
                    table.ForeignKey(
                        name: "FK_AcmPersons_Provinces_ProvinceID",
                        column: x => x.ProvinceID,
                        principalTable: "Provinces",
                        principalColumn: "ProvinceID");
                });

            migrationBuilder.CreateTable(
                name: "AcmPersonMetaData",
                columns: table => new
                {
                    AcmPersonMetaDataID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AcmPersonID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedUserID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    EditDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EditUserID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FieldKey = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FieldValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AcmPersonMetaData", x => x.AcmPersonMetaDataID);
                    table.ForeignKey(
                        name: "FK_AcmPersonMetaData_AcmPersons_AcmPersonID",
                        column: x => x.AcmPersonID,
                        principalTable: "AcmPersons",
                        principalColumn: "AcmPersonID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AcmPersonMetaData_AcmPersonID",
                table: "AcmPersonMetaData",
                column: "AcmPersonID");

            migrationBuilder.CreateIndex(
                name: "IX_AcmPersons_CountryID",
                table: "AcmPersons",
                column: "CountryID");

            migrationBuilder.CreateIndex(
                name: "IX_AcmPersons_EthnicityID",
                table: "AcmPersons",
                column: "EthnicityID");

            migrationBuilder.CreateIndex(
                name: "IX_AcmPersons_GenderID",
                table: "AcmPersons",
                column: "GenderID");

            migrationBuilder.CreateIndex(
                name: "IX_AcmPersons_ProvinceID",
                table: "AcmPersons",
                column: "ProvinceID");
        }
    }
}
