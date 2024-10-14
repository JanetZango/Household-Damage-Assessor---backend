using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ACM.Migrations
{
    public partial class FrmLinkUsrRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LinkAcmAccessRoleFormDefinitions",
                columns: table => new
                {
                    LinkAcmAccessRoleFormDefinitionID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AcmAccessRoleID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FormDefinitionID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LinkAcmAccessRoleFormDefinitions", x => x.LinkAcmAccessRoleFormDefinitionID);
                    table.ForeignKey(
                        name: "FK_LinkAcmAccessRoleFormDefinitions_AcmAccessRoles_AcmAccessRoleID",
                        column: x => x.AcmAccessRoleID,
                        principalTable: "AcmAccessRoles",
                        principalColumn: "AcmAccessRoleID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LinkAcmAccessRoleFormDefinitions_FormDefinitions_FormDefinitionID",
                        column: x => x.FormDefinitionID,
                        principalTable: "FormDefinitions",
                        principalColumn: "FormDefinitionID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LinkAcmAccessRoleFormDefinitions_AcmAccessRoleID",
                table: "LinkAcmAccessRoleFormDefinitions",
                column: "AcmAccessRoleID");

            migrationBuilder.CreateIndex(
                name: "IX_LinkAcmAccessRoleFormDefinitions_FormDefinitionID",
                table: "LinkAcmAccessRoleFormDefinitions",
                column: "FormDefinitionID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LinkAcmAccessRoleFormDefinitions");
        }
    }
}
