using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Fakebook.Profile.DataAccess.Migrations
{
    public partial class SeedingData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Fakebook");

            migrationBuilder.CreateTable(
                name: "Profile",
                schema: "Fakebook",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    ProfilePictureUrl = table.Column<string>(type: "text", nullable: true),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    PhoneNumber = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: true),
                    BirthDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profile", x => x.UserId);
                });

            migrationBuilder.InsertData(
                schema: "Fakebook",
                table: "Profile",
                columns: new[] { "UserId", "Email", "BirthDate", "FirstName", "LastName", "PhoneNumber", "ProfilePictureUrl", "Status" },
                values: new object[,]
                {
                    { 1, "david.barnes@revature.net", new DateTime(1994, 6, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "David", "Barnes", null, "https://images.unsplash.com/photo-1489533119213-66a5cd877091", "deployed my app feeling good about today's presentation" },
                    { 2, "testaccount@gmail.com", new DateTime(1994, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "Jay", "Shin", null, "https://cdn.download.ams.birds.cornell.edu/api/v1/asset/252252921/1800", null }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Profile",
                schema: "Fakebook");
        }
    }
}
