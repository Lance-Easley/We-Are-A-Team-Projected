using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Projected.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Profiles",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectName = table.Column<string>(maxLength: 50, nullable: false),
                    PermissionGroups = table.Column<string>(nullable: false),
                    Author = table.Column<string>(maxLength: 50, nullable: true),
                    DateSubmitted = table.Column<DateTime>(nullable: false),
                    ModifiedBy = table.Column<string>(maxLength: 50, nullable: true),
                    DateModified = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profiles", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Profiles");
        }
    }
}
