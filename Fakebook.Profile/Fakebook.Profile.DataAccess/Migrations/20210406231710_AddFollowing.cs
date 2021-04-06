using Microsoft.EntityFrameworkCore.Migrations;

namespace Fakebook.Profile.DataAccess.Migrations
{
    public partial class AddFollowing : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Relationships",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    FollowingId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Relationships", x => new { x.UserId, x.FollowingId });
                    table.ForeignKey(
                        name: "FK_Relationships_Profile_FollowingId",
                        column: x => x.FollowingId,
                        principalSchema: "Fakebook",
                        principalTable: "Profile",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Relationships_Profile_UserId",
                        column: x => x.UserId,
                        principalSchema: "Fakebook",
                        principalTable: "Profile",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Relationships_FollowingId",
                table: "Relationships",
                column: "FollowingId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Relationships");
        }
    }
}
