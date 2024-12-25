using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace pps.Migrations
{
    /// <inheritdoc />
    public partial class NewConnectForCA : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CreditAccounts_Users_UserId",
                table: "CreditAccounts");

            migrationBuilder.DropIndex(
                name: "IX_CreditAccounts_UserId",
                table: "CreditAccounts");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "CreditAccounts");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "UserId",
                table: "CreditAccounts",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_CreditAccounts_UserId",
                table: "CreditAccounts",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_CreditAccounts_Users_UserId",
                table: "CreditAccounts",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
