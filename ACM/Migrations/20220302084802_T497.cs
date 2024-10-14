using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ACM.Migrations
{
    public partial class T497 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "AcmAccessRoleID",
                table: "Users",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AcmAccessRoles",
                columns: table => new
                {
                    AcmAccessRoleID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AcmAccessRoles", x => x.AcmAccessRoleID);
                });

            migrationBuilder.CreateTable(
                name: "AcmRoles",
                columns: table => new
                {
                    AcmRoleID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EventCode = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AcmRoles", x => x.AcmRoleID);
                });

            migrationBuilder.CreateTable(
                name: "LinkAcmAccessRoleAcmRoles",
                columns: table => new
                {
                    LinkAcmAccessRoleAcmRoleID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AcmRoleID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AcmAccessRoleID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LinkAcmAccessRoleAcmRoles", x => x.LinkAcmAccessRoleAcmRoleID);
                    table.ForeignKey(
                        name: "FK_LinkAcmAccessRoleAcmRoles_AcmAccessRoles_AcmAccessRoleID",
                        column: x => x.AcmAccessRoleID,
                        principalTable: "AcmAccessRoles",
                        principalColumn: "AcmAccessRoleID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LinkAcmAccessRoleAcmRoles_AcmRoles_AcmRoleID",
                        column: x => x.AcmRoleID,
                        principalTable: "AcmRoles",
                        principalColumn: "AcmRoleID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_AcmAccessRoleID",
                table: "Users",
                column: "AcmAccessRoleID");

            migrationBuilder.CreateIndex(
                name: "IX_LinkAcmAccessRoleAcmRoles_AcmAccessRoleID",
                table: "LinkAcmAccessRoleAcmRoles",
                column: "AcmAccessRoleID");

            migrationBuilder.CreateIndex(
                name: "IX_LinkAcmAccessRoleAcmRoles_AcmRoleID",
                table: "LinkAcmAccessRoleAcmRoles",
                column: "AcmRoleID");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_AcmAccessRoles_AcmAccessRoleID",
                table: "Users",
                column: "AcmAccessRoleID",
                principalTable: "AcmAccessRoles",
                principalColumn: "AcmAccessRoleID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_AcmAccessRoles_AcmAccessRoleID",
                table: "Users");

            migrationBuilder.DropTable(
                name: "LinkAcmAccessRoleAcmRoles");

            migrationBuilder.DropTable(
                name: "AcmAccessRoles");

            migrationBuilder.DropTable(
                name: "AcmRoles");

            migrationBuilder.DropIndex(
                name: "IX_Users_AcmAccessRoleID",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "AcmAccessRoleID",
                table: "Users");
        }
    }
}
