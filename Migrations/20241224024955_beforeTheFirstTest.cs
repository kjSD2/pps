using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace pps.Migrations
{
    /// <inheritdoc />
    public partial class beforeTheFirstTest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Adress",
                table: "BankAtms");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Adress",
                table: "BankAtms",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
