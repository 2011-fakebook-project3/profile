using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Fakebook.Profile.DataAccess.Migrations
{
    public partial class AddNewAccountSeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "Fakebook",
                table: "Profile",
                columns: new[] { "Id", "BirthDate", "Email", "FirstName", "LastName", "PhoneNumber", "ProfilePictureUrl", "Status" },
                values: new object[] { 3, new DateTime(1996, 8, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "trevor.dunbar@revature.net", "Trevor", "Dunbar", null, "https://cdn.download.ams.birds.cornell.edu/api/v1/asset/252252921/1800", "four more days" });

            migrationBuilder.InsertData(
                table: "Follows",
                columns: new[] { "FollowingId", "UserId" },
                values: new object[,]
                {
                    { 1, 3 },
                    { 3, 1 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Follows",
                keyColumns: new[] { "FollowingId", "UserId" },
                keyValues: new object[] { 3, 1 });

            migrationBuilder.DeleteData(
                table: "Follows",
                keyColumns: new[] { "FollowingId", "UserId" },
                keyValues: new object[] { 1, 3 });

            migrationBuilder.DeleteData(
                schema: "Fakebook",
                table: "Profile",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
