using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ACM.Migrations
{
    public partial class PersonNullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AcmPersons_Ethnicities_EthnicityID",
                table: "AcmPersons");

            migrationBuilder.DropForeignKey(
                name: "FK_AcmPersons_Genders_GenderID",
                table: "AcmPersons");

            migrationBuilder.AlterColumn<Guid>(
                name: "GenderID",
                table: "AcmPersons",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "EthnicityID",
                table: "AcmPersons",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_AcmPersons_Ethnicities_EthnicityID",
                table: "AcmPersons",
                column: "EthnicityID",
                principalTable: "Ethnicities",
                principalColumn: "EthnicityID");

            migrationBuilder.AddForeignKey(
                name: "FK_AcmPersons_Genders_GenderID",
                table: "AcmPersons",
                column: "GenderID",
                principalTable: "Genders",
                principalColumn: "GenderID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AcmPersons_Ethnicities_EthnicityID",
                table: "AcmPersons");

            migrationBuilder.DropForeignKey(
                name: "FK_AcmPersons_Genders_GenderID",
                table: "AcmPersons");

            migrationBuilder.AlterColumn<Guid>(
                name: "GenderID",
                table: "AcmPersons",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "EthnicityID",
                table: "AcmPersons",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

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
    }
}
