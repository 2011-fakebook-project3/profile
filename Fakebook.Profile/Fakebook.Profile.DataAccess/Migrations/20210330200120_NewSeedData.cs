﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Fakebook.Profile.DataAccess.Migrations
{
    public partial class NewSeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "Fakebook",
                table: "Profile",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.InsertData(
                schema: "Fakebook",
                table: "Profile",
                columns: new[] {"Id", "Email", "BirthDate", "FirstName", "LastName", "PhoneNumber", "ProfilePictureUrl", "Status" },
                values: new object[] {1, "john.werner@revature.net", new DateTime(1994, 6, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "John", "Werner", null, "https://images.unsplash.com/photo-1489533119213-66a5cd877091", "deployed my app feeling good about today's presentation" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "Fakebook",
                table: "Profile",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.InsertData(
                schema: "Fakebook",
                table: "Profile",
                columns: new[] {"Id", "Email", "BirthDate", "FirstName", "LastName", "PhoneNumber", "ProfilePictureUrl", "Status" },
                values: new object[] {1, "david.barnes@revature.net", new DateTime(1994, 6, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "David", "Barnes", null, "https://images.unsplash.com/photo-1489533119213-66a5cd877091", "deployed my app feeling good about today's presentation" });
        }
    }
}
