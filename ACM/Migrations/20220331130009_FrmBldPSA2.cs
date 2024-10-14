using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ACM.Migrations
{
    public partial class FrmBldPSA2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FormDefinitionItemPostSubmissionActions",
                columns: table => new
                {
                    FormDefinitionItemPostSubmissionActionID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FormDefinitionItemID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FieldName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormDefinitionItemPostSubmissionActions", x => x.FormDefinitionItemPostSubmissionActionID);
                    table.ForeignKey(
                        name: "FK_FormDefinitionItemPostSubmissionActions_FormDefinitionItems_FormDefinitionItemID",
                        column: x => x.FormDefinitionItemID,
                        principalTable: "FormDefinitionItems",
                        principalColumn: "FormDefinitionItemID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FormDefinitionItemPostSubmissionActions_FormDefinitionItemID",
                table: "FormDefinitionItemPostSubmissionActions",
                column: "FormDefinitionItemID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FormDefinitionItemPostSubmissionActions");
        }
    }
}
