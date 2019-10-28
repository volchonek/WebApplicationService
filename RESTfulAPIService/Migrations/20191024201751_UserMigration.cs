using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RESTfulAPIService.Migrations
{
    /// <summary>
    /// </summary>
    public partial class UserMigration : Migration
    {
        /// <summary>
        /// </summary>
        /// <param name="migrationBuilder"></param>
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                "public");

            migrationBuilder.CreateTable(
                "User",
                schema: "public",
                columns: table => new
                {
                    Guid = table.Column<Guid>(),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table => { table.PrimaryKey("PK_User", x => x.Guid); });
        }

        /// <summary>
        /// </summary>
        /// <param name="migrationBuilder"></param>
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "User",
                "public");
        }
    }
}