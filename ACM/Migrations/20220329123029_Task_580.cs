using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ACM.Migrations
{
    public partial class Task_580 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "EthnicityID",
                table: "AcmPersons",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "GenderID",
                table: "AcmPersons",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Ethnicities",
                columns: table => new
                {
                    EthnicityID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ethnicities", x => x.EthnicityID);
                });

            migrationBuilder.CreateTable(
                name: "Genders",
                columns: table => new
                {
                    GenderID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genders", x => x.GenderID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AcmPersons_EthnicityID",
                table: "AcmPersons",
                column: "EthnicityID");

            migrationBuilder.CreateIndex(
                name: "IX_AcmPersons_GenderID",
                table: "AcmPersons",
                column: "GenderID");

            migrationBuilder.AddForeignKey(
                name: "FK_AcmPersons_Ethnicities_EthnicityID",
                table: "AcmPersons",
                column: "EthnicityID",
                principalTable: "Ethnicities",
                principalColumn: "EthnicityID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AcmPersons_Genders_GenderID",
                table: "AcmPersons",
                column: "GenderID",
                principalTable: "Genders",
                principalColumn: "GenderID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AcmPersons_Ethnicities_EthnicityID",
                table: "AcmPersons");

            migrationBuilder.DropForeignKey(
                name: "FK_AcmPersons_Genders_GenderID",
                table: "AcmPersons");

            migrationBuilder.DropTable(
                name: "Ethnicities");

            migrationBuilder.DropTable(
                name: "Genders");

            migrationBuilder.DropIndex(
                name: "IX_AcmPersons_EthnicityID",
                table: "AcmPersons");

            migrationBuilder.DropIndex(
                name: "IX_AcmPersons_GenderID",
                table: "AcmPersons");

            migrationBuilder.DropColumn(
                name: "EthnicityID",
                table: "AcmPersons");

            migrationBuilder.DropColumn(
                name: "GenderID",
                table: "AcmPersons");
        }
    }
}
