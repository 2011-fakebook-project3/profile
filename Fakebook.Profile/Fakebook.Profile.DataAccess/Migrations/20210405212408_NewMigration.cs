using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Fakebook.Profile.DataAccess.Migrations
{
    public partial class NewMigration : Migration
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
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
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
                    table.PrimaryKey("PK_Profile", x => x.Id);
                });

            migrationBuilder.InsertData(
                schema: "Fakebook",
                table: "Profile",
                columns: new[] { "Id", "BirthDate", "Email", "FirstName", "LastName", "PhoneNumber", "ProfilePictureUrl", "Status" },
                values: new object[,]
                {
                    { 1, new DateTime(1994, 6, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "john.werner@revature.net", "John", "Werner", null, "https://images.unsplash.com/photo-1489533119213-66a5cd877091", "deployed my app feeling good about today's presentation" },
                    { 2, new DateTime(1994, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "testaccount@gmail.com", "Jay", "Shin", null, "https://cdn.download.ams.birds.cornell.edu/api/v1/asset/252252921/1800", null }
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
