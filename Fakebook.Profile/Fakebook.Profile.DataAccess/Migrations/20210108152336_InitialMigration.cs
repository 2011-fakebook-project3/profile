using System;

using Microsoft.EntityFrameworkCore.Migrations;

namespace Fakebook.Profile.DataAccess.Migrations
{
    public partial class InitialMigration : Migration
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
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProfilePictureUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profile", x => x.Email);
                });

            migrationBuilder.CreateTable(
                name: "Follow",
                schema: "Fakebook",
                columns: table => new
                {
                    FolloweeEmail = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FollowerEmail = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Pk_FollowEntity", x => new { x.FollowerEmail, x.FolloweeEmail });
                    table.ForeignKey(
                        name: "Fk_Follow_Followee",
                        column: x => x.FolloweeEmail,
                        principalSchema: "Fakebook",
                        principalTable: "Profile",
                        principalColumn: "Email");
                    table.ForeignKey(
                        name: "Fk_Follow_Follower",
                        column: x => x.FollowerEmail,
                        principalSchema: "Fakebook",
                        principalTable: "Profile",
                        principalColumn: "Email");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Follow_FolloweeEmail",
                schema: "Fakebook",
                table: "Follow",
                column: "FolloweeEmail");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Follow",
                schema: "Fakebook");

            migrationBuilder.DropTable(
                name: "Profile",
                schema: "Fakebook");
        }
    }
}
