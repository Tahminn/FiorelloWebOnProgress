using Microsoft.EntityFrameworkCore.Migrations;

namespace FiorelloBackDb.Migrations
{
    public partial class AddStettingNewObject : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Settings",
                columns: new[] { "Id", "LoadTake", "ProductTake" },
                values: new object[] { 1, 4, 8 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Settings",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
