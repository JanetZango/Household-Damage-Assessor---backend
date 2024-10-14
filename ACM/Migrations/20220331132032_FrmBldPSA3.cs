using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ACM.Migrations
{
    public partial class FrmBldPSA3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "FormBuilderQuestionPostSubmissionActionID",
                table: "FormDefinitionItemPostSubmissionActions",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_FormDefinitionItemPostSubmissionActions_FormBuilderQuestionPostSubmissionActionID",
                table: "FormDefinitionItemPostSubmissionActions",
                column: "FormBuilderQuestionPostSubmissionActionID");

            migrationBuilder.AddForeignKey(
                name: "FK_FormDefinitionItemPostSubmissionActions_FormBuilderQuestionPostSubmissionActions_FormBuilderQuestionPostSubmissionActionID",
                table: "FormDefinitionItemPostSubmissionActions",
                column: "FormBuilderQuestionPostSubmissionActionID",
                principalTable: "FormBuilderQuestionPostSubmissionActions",
                principalColumn: "FormBuilderQuestionPostSubmissionActionID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FormDefinitionItemPostSubmissionActions_FormBuilderQuestionPostSubmissionActions_FormBuilderQuestionPostSubmissionActionID",
                table: "FormDefinitionItemPostSubmissionActions");

            migrationBuilder.DropIndex(
                name: "IX_FormDefinitionItemPostSubmissionActions_FormBuilderQuestionPostSubmissionActionID",
                table: "FormDefinitionItemPostSubmissionActions");

            migrationBuilder.DropColumn(
                name: "FormBuilderQuestionPostSubmissionActionID",
                table: "FormDefinitionItemPostSubmissionActions");
        }
    }
}
