using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace pps.Migrations
{
    /// <inheritdoc />
    public partial class NewConnect : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BankAtms_BankOffices_BankOfficeId",
                table: "BankAtms");

            migrationBuilder.DropForeignKey(
                name: "FK_BankAtms_Banks_BankId",
                table: "BankAtms");

            migrationBuilder.DropForeignKey(
                name: "FK_CreditAccounts_Banks_BankId",
                table: "CreditAccounts");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Banks_BankId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentAccounts_Banks_BankId",
                table: "PaymentAccounts");

            migrationBuilder.DropTable(
                name: "BankUser");

            migrationBuilder.DropIndex(
                name: "IX_PaymentAccounts_BankId",
                table: "PaymentAccounts");

            migrationBuilder.DropIndex(
                name: "IX_Employees_BankId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_CreditAccounts_BankId",
                table: "CreditAccounts");

            migrationBuilder.DropIndex(
                name: "IX_BankAtms_BankId",
                table: "BankAtms");

            migrationBuilder.DropIndex(
                name: "IX_BankAtms_BankOfficeId",
                table: "BankAtms");

            migrationBuilder.DropColumn(
                name: "BankId",
                table: "PaymentAccounts");

            migrationBuilder.DropColumn(
                name: "BankId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "BankId",
                table: "CreditAccounts");

            migrationBuilder.DropColumn(
                name: "BankId",
                table: "BankAtms");

            migrationBuilder.DropColumn(
                name: "BankOfficeId",
                table: "BankAtms");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "BankId",
                table: "PaymentAccounts",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "BankId",
                table: "Employees",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "BankId",
                table: "CreditAccounts",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "BankId",
                table: "BankAtms",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "BankOfficeId",
                table: "BankAtms",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "BankUser",
                columns: table => new
                {
                    BanksId = table.Column<long>(type: "bigint", nullable: false),
                    UsersId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankUser", x => new { x.BanksId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_BankUser_Banks_BanksId",
                        column: x => x.BanksId,
                        principalTable: "Banks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BankUser_Users_UsersId",
                        column: x => x.UsersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PaymentAccounts_BankId",
                table: "PaymentAccounts",
                column: "BankId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_BankId",
                table: "Employees",
                column: "BankId");

            migrationBuilder.CreateIndex(
                name: "IX_CreditAccounts_BankId",
                table: "CreditAccounts",
                column: "BankId");

            migrationBuilder.CreateIndex(
                name: "IX_BankAtms_BankId",
                table: "BankAtms",
                column: "BankId");

            migrationBuilder.CreateIndex(
                name: "IX_BankAtms_BankOfficeId",
                table: "BankAtms",
                column: "BankOfficeId");

            migrationBuilder.CreateIndex(
                name: "IX_BankUser_UsersId",
                table: "BankUser",
                column: "UsersId");

            migrationBuilder.AddForeignKey(
                name: "FK_BankAtms_BankOffices_BankOfficeId",
                table: "BankAtms",
                column: "BankOfficeId",
                principalTable: "BankOffices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BankAtms_Banks_BankId",
                table: "BankAtms",
                column: "BankId",
                principalTable: "Banks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CreditAccounts_Banks_BankId",
                table: "CreditAccounts",
                column: "BankId",
                principalTable: "Banks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Banks_BankId",
                table: "Employees",
                column: "BankId",
                principalTable: "Banks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentAccounts_Banks_BankId",
                table: "PaymentAccounts",
                column: "BankId",
                principalTable: "Banks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
