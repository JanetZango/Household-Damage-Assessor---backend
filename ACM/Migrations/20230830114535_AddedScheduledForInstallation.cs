using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ACM.Migrations
{
    public partial class AddedScheduledForInstallation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ScheduledForInstallation",
                table: "Leads",
                type: "bit",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ScheduledForInstallation",
                table: "Leads");
        }
    }
}
