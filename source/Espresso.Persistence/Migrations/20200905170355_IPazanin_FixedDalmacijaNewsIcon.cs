using Microsoft.EntityFrameworkCore.Migrations;

namespace Espresso.Persistence.Migrations
{
    public partial class IPazanin_FixedDalmacijaNewsIcon : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "NewsPortals",
                keyColumn: "Id",
                keyValue: 40,
                column: "IconUrl",
                value: "Icons/DalmacijaNews.png");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "NewsPortals",
                keyColumn: "Id",
                keyValue: 40,
                column: "IconUrl",
                value: "Icons/DalmacijaDanas.png");

        }
    }
}
