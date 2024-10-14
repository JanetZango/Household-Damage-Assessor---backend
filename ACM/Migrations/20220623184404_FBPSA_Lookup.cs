using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ACM.Migrations
{
    public partial class FBPSA_Lookup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsPersonLookupField",
                table: "FormDefinitionItemText",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "BasicFieldEventCode",
                table: "FormDefinitionItemPostSubmissionActions",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPersonLookupField",
                table: "FormDefinitionItemText");

            migrationBuilder.DropColumn(
                name: "BasicFieldEventCode",
                table: "FormDefinitionItemPostSubmissionActions");
        }
    }
}
