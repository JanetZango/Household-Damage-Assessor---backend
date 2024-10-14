using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ACM.Migrations
{
    public partial class T532 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AcmPersons",
                columns: table => new
                {
                    AcmPersonID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CountryID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProvinceID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedUserID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    EditUserID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EditDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IDNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CurrentPlacement = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AcmPersons", x => x.AcmPersonID);
                    table.ForeignKey(
                        name: "FK_AcmPersons_Countries_CountryID",
                        column: x => x.CountryID,
                        principalTable: "Countries",
                        principalColumn: "CountryID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AcmPersons_Provinces_ProvinceID",
                        column: x => x.ProvinceID,
                        principalTable: "Provinces",
                        principalColumn: "ProvinceID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AcmPersonMetaData",
                columns: table => new
                {
                    AcmPersonMetaDataID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AcmPersonID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedUserID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    EditUserID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EditDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                name: "IX_AcmPersons_ProvinceID",
                table: "AcmPersons",
                column: "ProvinceID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AcmPersonMetaData");

            migrationBuilder.DropTable(
                name: "AcmPersons");
        }
    }
}
