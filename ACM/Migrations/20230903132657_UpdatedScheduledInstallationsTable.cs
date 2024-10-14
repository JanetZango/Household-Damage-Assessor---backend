using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ACM.Migrations
{
    public partial class UpdatedScheduledInstallationsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TechnicianId",
                table: "ScheduledInstallations",
                newName: "TechnicianID");

            migrationBuilder.RenameColumn(
                name: "LeadId",
                table: "ScheduledInstallations",
                newName: "LeadID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "ScheduledInstallations",
                newName: "ScheduledInstallationID");

            migrationBuilder.AddColumn<string>(
                name: "Comments",
                table: "ScheduledInstallations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "InstallationDate",
                table: "ScheduledInstallations",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsInstalled",
                table: "ScheduledInstallations",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Comments",
                table: "ScheduledInstallations");

            migrationBuilder.DropColumn(
                name: "InstallationDate",
                table: "ScheduledInstallations");

            migrationBuilder.DropColumn(
                name: "IsInstalled",
                table: "ScheduledInstallations");

            migrationBuilder.RenameColumn(
                name: "TechnicianID",
                table: "ScheduledInstallations",
                newName: "TechnicianId");

            migrationBuilder.RenameColumn(
                name: "LeadID",
                table: "ScheduledInstallations",
                newName: "LeadId");

            migrationBuilder.RenameColumn(
                name: "ScheduledInstallationID",
                table: "ScheduledInstallations",
                newName: "Id");
        }
    }
}
