using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ACM.Migrations
{
    public partial class FrmDef : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FormDefinitions",
                columns: table => new
                {
                    FormDefinitionID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EffectiveStartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EffectiveEndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    InstructionsFormatted = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InstanceNumber = table.Column<long>(type: "bigint", nullable: false),
                    AllowSignatures = table.Column<bool>(type: "bit", nullable: false),
                    IsApprovalRequired = table.Column<bool>(type: "bit", nullable: false),
                    ISLocationRelevant = table.Column<bool>(type: "bit", nullable: false),
                    DynamicTemplateInstanceID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DynamicTemplateInstanceIDCLInst = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Version = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    Tags = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsLatest = table.Column<bool>(type: "bit", nullable: false),
                    CreatedUserID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    EditUserID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EditDateTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormDefinitions", x => x.FormDefinitionID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FormDefinitions");
        }
    }
}
