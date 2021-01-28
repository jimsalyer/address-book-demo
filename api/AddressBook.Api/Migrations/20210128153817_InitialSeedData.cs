using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AddressBook.Api.Migrations
{
    public partial class InitialSeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "user_id", "email_address", "password_hash", "password_salt" },
                values: new object[] { 1, "admin@admin.com", new byte[] { 132, 144, 86, 243, 57, 41, 113, 188, 159, 115, 209, 53, 163, 75, 235, 62, 60, 45, 52, 24, 236, 170, 203, 223, 177, 200, 171, 186, 86, 14, 4, 49, 15, 21, 106, 183, 27, 244, 170, 49, 133, 69, 50, 232, 132, 13, 12, 134, 111, 40, 124, 145, 162, 7, 185, 210, 75, 25, 119, 133, 98, 180, 159, 109 }, new byte[] { 133, 219, 46, 64, 218, 149, 170, 106, 118, 15, 198, 220, 221, 186, 198, 248, 148, 232, 140, 50, 26, 251, 59, 209, 5, 19, 24, 206, 97, 146, 99, 37, 33, 200, 73, 162, 76, 78, 253, 111, 197, 29, 204, 52, 2, 29, 148, 246, 93, 211, 129, 211, 176, 216, 134, 42, 70, 7, 71, 22, 107, 80, 183, 28, 149, 223, 85, 20, 55, 226, 196, 52, 86, 246, 43, 229, 146, 235, 43, 23, 204, 52, 176, 200, 178, 34, 247, 0, 91, 227, 89, 74, 207, 137, 47, 192, 59, 251, 12, 189, 189, 248, 25, 127, 251, 179, 36, 178, 136, 148, 136, 232, 0, 223, 66, 153, 255, 163, 145, 39, 175, 73, 237, 28, 236, 245, 204, 13 } });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "user_id",
                keyValue: 1);
        }
    }
}
