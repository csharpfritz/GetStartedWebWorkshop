using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyCollection.Migrations
{
    /// <inheritdoc />
    public partial class AddAiDescriptionAndTags : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AiDescription",
                table: "CollectionItems",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AiTags",
                table: "CollectionItems",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AiDescription",
                table: "CollectionItems");

            migrationBuilder.DropColumn(
                name: "AiTags",
                table: "CollectionItems");
        }
    }
}
