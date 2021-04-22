using Microsoft.EntityFrameworkCore.Migrations;

namespace Espresso.Persistence.EspressoDatabaseMigrations
{
    public partial class AddedOrderIndexToRssFeedContentModifier : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OrderIndex",
                table: "RssFeedContentModifier",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "RssFeedContentModifier",
                keyColumn: "Id",
                keyValue: 1,
                column: "OrderIndex",
                value: 1);

            migrationBuilder.UpdateData(
                table: "RssFeedContentModifier",
                keyColumn: "Id",
                keyValue: 2,
                column: "OrderIndex",
                value: 2);

            migrationBuilder.UpdateData(
                table: "RssFeedContentModifier",
                keyColumn: "Id",
                keyValue: 3,
                column: "OrderIndex",
                value: 1);

            migrationBuilder.UpdateData(
                table: "RssFeedContentModifier",
                keyColumn: "Id",
                keyValue: 4,
                column: "OrderIndex",
                value: 2);

            migrationBuilder.UpdateData(
                table: "RssFeedContentModifier",
                keyColumn: "Id",
                keyValue: 5,
                column: "OrderIndex",
                value: 1);

            migrationBuilder.UpdateData(
                table: "RssFeedContentModifier",
                keyColumn: "Id",
                keyValue: 6,
                column: "OrderIndex",
                value: 2);

            migrationBuilder.UpdateData(
                table: "RssFeedContentModifier",
                keyColumn: "Id",
                keyValue: 7,
                column: "OrderIndex",
                value: 1);

            migrationBuilder.UpdateData(
                table: "RssFeedContentModifier",
                keyColumn: "Id",
                keyValue: 8,
                column: "OrderIndex",
                value: 2);

            migrationBuilder.UpdateData(
                table: "RssFeedContentModifier",
                keyColumn: "Id",
                keyValue: 9,
                column: "OrderIndex",
                value: 1);

            migrationBuilder.UpdateData(
                table: "RssFeedContentModifier",
                keyColumn: "Id",
                keyValue: 10,
                column: "OrderIndex",
                value: 2);

            migrationBuilder.UpdateData(
                table: "RssFeedContentModifier",
                keyColumn: "Id",
                keyValue: 11,
                column: "OrderIndex",
                value: 1);

            migrationBuilder.UpdateData(
                table: "RssFeedContentModifier",
                keyColumn: "Id",
                keyValue: 12,
                column: "OrderIndex",
                value: 2);

            migrationBuilder.UpdateData(
                table: "RssFeedContentModifier",
                keyColumn: "Id",
                keyValue: 13,
                column: "OrderIndex",
                value: 1);

            migrationBuilder.UpdateData(
                table: "RssFeedContentModifier",
                keyColumn: "Id",
                keyValue: 14,
                column: "OrderIndex",
                value: 2);

            migrationBuilder.UpdateData(
                table: "RssFeedContentModifier",
                keyColumn: "Id",
                keyValue: 15,
                column: "OrderIndex",
                value: 1);

            migrationBuilder.UpdateData(
                table: "RssFeedContentModifier",
                keyColumn: "Id",
                keyValue: 16,
                column: "OrderIndex",
                value: 2);

            migrationBuilder.UpdateData(
                table: "RssFeedContentModifier",
                keyColumn: "Id",
                keyValue: 17,
                column: "OrderIndex",
                value: 1);

            migrationBuilder.UpdateData(
                table: "RssFeedContentModifier",
                keyColumn: "Id",
                keyValue: 18,
                column: "OrderIndex",
                value: 2);

            migrationBuilder.UpdateData(
                table: "RssFeedContentModifier",
                keyColumn: "Id",
                keyValue: 19,
                column: "OrderIndex",
                value: 1);

            migrationBuilder.UpdateData(
                table: "RssFeedContentModifier",
                keyColumn: "Id",
                keyValue: 20,
                column: "OrderIndex",
                value: 2);

            migrationBuilder.UpdateData(
                table: "RssFeedContentModifier",
                keyColumn: "Id",
                keyValue: 21,
                column: "OrderIndex",
                value: 1);

            migrationBuilder.UpdateData(
                table: "RssFeedContentModifier",
                keyColumn: "Id",
                keyValue: 22,
                column: "OrderIndex",
                value: 2);

            migrationBuilder.UpdateData(
                table: "RssFeedContentModifier",
                keyColumn: "Id",
                keyValue: 23,
                column: "OrderIndex",
                value: 3);

            migrationBuilder.UpdateData(
                table: "RssFeedContentModifier",
                keyColumn: "Id",
                keyValue: 24,
                column: "OrderIndex",
                value: 4);

            migrationBuilder.UpdateData(
                table: "RssFeedContentModifier",
                keyColumn: "Id",
                keyValue: 25,
                column: "OrderIndex",
                value: 1);

            migrationBuilder.UpdateData(
                table: "RssFeedContentModifier",
                keyColumn: "Id",
                keyValue: 26,
                column: "OrderIndex",
                value: 2);

            migrationBuilder.UpdateData(
                table: "RssFeedContentModifier",
                keyColumn: "Id",
                keyValue: 27,
                column: "OrderIndex",
                value: 3);

            migrationBuilder.UpdateData(
                table: "RssFeedContentModifier",
                keyColumn: "Id",
                keyValue: 28,
                column: "OrderIndex",
                value: 4);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrderIndex",
                table: "RssFeedContentModifier");
        }
    }
}
