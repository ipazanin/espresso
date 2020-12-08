using Microsoft.EntityFrameworkCore.Migrations;

namespace Espresso.Persistence.Migrations
{
    public partial class IPazanin_AddedArticleEditorConfiguration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsHidden",
                table: "Articles",
                newName: "EditorConfiguration_IsHidden");

            migrationBuilder.RenameColumn(
                name: "IsFeatured",
                table: "Articles",
                newName: "EditorConfiguration_IsFeatured");

            migrationBuilder.AlterColumn<bool>(
                name: "EditorConfiguration_IsFeatured",
                table: "Articles",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "EditorConfiguration_FeaturedPosition",
                table: "Articles",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EditorConfiguration_FeaturedPosition",
                table: "Articles");

            migrationBuilder.RenameColumn(
                name: "EditorConfiguration_IsHidden",
                table: "Articles",
                newName: "IsHidden");

            migrationBuilder.RenameColumn(
                name: "EditorConfiguration_IsFeatured",
                table: "Articles",
                newName: "IsFeatured");

            migrationBuilder.AlterColumn<bool>(
                name: "IsFeatured",
                table: "Articles",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);
        }
    }
}
