using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MarkdownNoteTakingApp.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class addingFileFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FileName",
                table: "Notes",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FilePath",
                table: "Notes",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<long>(
                name: "FileSize",
                table: "Notes",
                type: "bigint",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileName",
                table: "Notes");

            migrationBuilder.DropColumn(
                name: "FilePath",
                table: "Notes");

            migrationBuilder.DropColumn(
                name: "FileSize",
                table: "Notes");
        }
    }
}
