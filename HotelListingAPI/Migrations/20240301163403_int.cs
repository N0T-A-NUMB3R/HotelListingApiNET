using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelListingAPI.Migrations
{
    public partial class @int : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
            name: "Countries",
            table: "Id");

        migrationBuilder.AddColumn<int>(
            name: "Countries",
            table: "Id",
            nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
            name: "Countries",
            table: "Id");

        migrationBuilder.AddColumn<Guid>(
            name: "Countries",  
            table: "Id",
            nullable: true);
        }
    }
}
