using Microsoft.EntityFrameworkCore.Migrations;

namespace AddressBook.Api.Migrations
{
    public partial class AddInternationalSupport : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "zip",
                table: "contacts",
                newName: "postal_code");

            migrationBuilder.RenameColumn(
                name: "state",
                table: "contacts",
                newName: "region");

            migrationBuilder.AddColumn<string>(
                name: "country",
                table: "contacts",
                type: "character varying(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "country",
                table: "contacts");

            migrationBuilder.RenameColumn(
                name: "region",
                table: "contacts",
                newName: "state");

            migrationBuilder.RenameColumn(
                name: "postal_code",
                table: "contacts",
                newName: "zip");
        }
    }
}
