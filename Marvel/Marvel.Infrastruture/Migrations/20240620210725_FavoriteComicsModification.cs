using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Marvel.Infrastruture.Migrations
{
    /// <inheritdoc />
    public partial class FavoriteComicsModification : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "FavoriteComics",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ImgUrl",
                table: "FavoriteComics",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "FavoriteComics",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "FavoriteComics");

            migrationBuilder.DropColumn(
                name: "ImgUrl",
                table: "FavoriteComics");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "FavoriteComics");
        }
    }
}
