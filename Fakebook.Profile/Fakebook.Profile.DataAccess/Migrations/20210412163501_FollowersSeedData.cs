using Microsoft.EntityFrameworkCore.Migrations;

namespace Fakebook.Profile.DataAccess.Migrations
{
    public partial class FollowersSeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Follows",
                columns: new[] { "FollowingId", "UserId" },
                values: new object[,]
                {
                    { 2, 1 },
                    { 1, 2 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Follows",
                keyColumns: new[] { "FollowingId", "UserId" },
                keyValues: new object[] { 2, 1 });

            migrationBuilder.DeleteData(
                table: "Follows",
                keyColumns: new[] { "FollowingId", "UserId" },
                keyValues: new object[] { 1, 2 });
        }
    }
}
