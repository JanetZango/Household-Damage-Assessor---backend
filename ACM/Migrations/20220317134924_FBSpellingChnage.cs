using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ACM.Migrations
{
    public partial class FBSpellingChnage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ISLocationRelevant",
                table: "FormDefinitions",
                newName: "IsLocationRelevant");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsLocationRelevant",
                table: "FormDefinitions",
                newName: "ISLocationRelevant");
        }
    }
}
