using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Notebook.EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class addIndexesToNote : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Indexes",
                table: "Notes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "[]");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Indexes",
                table: "Notes");
        }
    }
}
