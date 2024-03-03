using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelListingAPI.Migrations
{
    public partial class AddedRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "40816856-da90-4fa8-a940-b1e5840c99ff", "d5347b1f-c870-4797-954b-b343532982e2", "Administrator", "ADMINISTRATOR" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "d31e585e-b515-4f0c-980a-ca1be51f9fe0", "2787c399-ebec-4b5b-aeae-9431edc5a265", "User", "USER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "40816856-da90-4fa8-a940-b1e5840c99ff");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d31e585e-b515-4f0c-980a-ca1be51f9fe0");
        }
    }
}
