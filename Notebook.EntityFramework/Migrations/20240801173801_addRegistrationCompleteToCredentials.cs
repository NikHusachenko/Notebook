using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Notebook.EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class addRegistrationCompleteToCredentials : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsRegistrationCompleted",
                table: "Credentials",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsRegistrationCompleted",
                table: "Credentials");
        }
    }
}
