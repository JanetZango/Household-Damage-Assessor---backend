using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ACM.Migrations
{
    public partial class PersCountryOpt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AcmPersons_Countries_CountryID",
                table: "AcmPersons");

            migrationBuilder.DropForeignKey(
                name: "FK_AcmPersons_Provinces_ProvinceID",
                table: "AcmPersons");

            migrationBuilder.AlterColumn<Guid>(
                name: "ProvinceID",
                table: "AcmPersons",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "CountryID",
                table: "AcmPersons",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_AcmPersons_Countries_CountryID",
                table: "AcmPersons",
                column: "CountryID",
                principalTable: "Countries",
                principalColumn: "CountryID");

            migrationBuilder.AddForeignKey(
                name: "FK_AcmPersons_Provinces_ProvinceID",
                table: "AcmPersons",
                column: "ProvinceID",
                principalTable: "Provinces",
                principalColumn: "ProvinceID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AcmPersons_Countries_CountryID",
                table: "AcmPersons");

            migrationBuilder.DropForeignKey(
                name: "FK_AcmPersons_Provinces_ProvinceID",
                table: "AcmPersons");

            migrationBuilder.AlterColumn<Guid>(
                name: "ProvinceID",
                table: "AcmPersons",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "CountryID",
                table: "AcmPersons",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AcmPersons_Countries_CountryID",
                table: "AcmPersons",
                column: "CountryID",
                principalTable: "Countries",
                principalColumn: "CountryID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AcmPersons_Provinces_ProvinceID",
                table: "AcmPersons",
                column: "ProvinceID",
                principalTable: "Provinces",
                principalColumn: "ProvinceID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
