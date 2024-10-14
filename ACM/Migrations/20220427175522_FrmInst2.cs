using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ACM.Migrations
{
    public partial class FrmInst2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TenantID",
                table: "FormInstanceItemSliders");

            migrationBuilder.DropColumn(
                name: "TenantID",
                table: "FormInstanceItemQuestions");

            migrationBuilder.RenameColumn(
                name: "CheckListExecutionDate",
                table: "FormInstances",
                newName: "FormExecutionDate");

            migrationBuilder.RenameColumn(
                name: "CheckListCaptureStartDateTime",
                table: "FormInstances",
                newName: "FormCaptureStartDateTime");

            migrationBuilder.RenameColumn(
                name: "CheckListCaptureEndDateTime",
                table: "FormInstances",
                newName: "FormCaptureEndDateTime");

            migrationBuilder.CreateIndex(
                name: "IX_FormInstances_CreatedUserID",
                table: "FormInstances",
                column: "CreatedUserID");

            migrationBuilder.AddForeignKey(
                name: "FK_FormInstances_Users_CreatedUserID",
                table: "FormInstances",
                column: "CreatedUserID",
                principalTable: "Users",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FormInstances_Users_CreatedUserID",
                table: "FormInstances");

            migrationBuilder.DropIndex(
                name: "IX_FormInstances_CreatedUserID",
                table: "FormInstances");

            migrationBuilder.RenameColumn(
                name: "FormExecutionDate",
                table: "FormInstances",
                newName: "CheckListExecutionDate");

            migrationBuilder.RenameColumn(
                name: "FormCaptureStartDateTime",
                table: "FormInstances",
                newName: "CheckListCaptureStartDateTime");

            migrationBuilder.RenameColumn(
                name: "FormCaptureEndDateTime",
                table: "FormInstances",
                newName: "CheckListCaptureEndDateTime");

            migrationBuilder.AddColumn<Guid>(
                name: "TenantID",
                table: "FormInstanceItemSliders",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TenantID",
                table: "FormInstanceItemQuestions",
                type: "uniqueidentifier",
                nullable: true);
        }
    }
}
