using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace pps.Migrations
{
    /// <inheritdoc />
    public partial class setupBD : Migration
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
                name: "FK_BankAtms_Employees_EmployeeAccompanyingId",
                table: "BankAtms");

            migrationBuilder.DropForeignKey(
                name: "FK_CreditAccounts_Banks_BankId",
                table: "CreditAccounts");

            migrationBuilder.DropForeignKey(
                name: "FK_CreditAccounts_Employees_EmployeeId",
                table: "CreditAccounts");

            migrationBuilder.DropForeignKey(
                name: "FK_CreditAccounts_PaymentAccounts_PaymentAccountId",
                table: "CreditAccounts");

            migrationBuilder.DropForeignKey(
                name: "FK_CreditAccounts_Users_UserId",
                table: "CreditAccounts");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_BankOffices_BankOfficeId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Banks_BanksId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentAccounts_Banks_BankId",
                table: "PaymentAccounts");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentAccounts_Users_UserId",
                table: "PaymentAccounts");

            migrationBuilder.DropColumn(
                name: "AtmsTotal",
                table: "Banks");

            migrationBuilder.DropColumn(
                name: "BankOfficesTotal",
                table: "Banks");

            migrationBuilder.DropColumn(
                name: "CliensTotal",
                table: "Banks");

            migrationBuilder.DropColumn(
                name: "EmployeesTotal",
                table: "Banks");

            migrationBuilder.RenameColumn(
                name: "BanksId",
                table: "Employees",
                newName: "BankId");

            migrationBuilder.RenameIndex(
                name: "IX_Employees_BanksId",
                table: "Employees",
                newName: "IX_Employees_BankId");

            migrationBuilder.RenameColumn(
                name: "AtmsTotal",
                table: "BankOffices",
                newName: "BankId");

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
                name: "IX_BankOffices_BankId",
                table: "BankOffices",
                column: "BankId");

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
                name: "FK_BankAtms_Employees_EmployeeAccompanyingId",
                table: "BankAtms",
                column: "EmployeeAccompanyingId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BankOffices_Banks_BankId",
                table: "BankOffices",
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
                name: "FK_CreditAccounts_Employees_EmployeeId",
                table: "CreditAccounts",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CreditAccounts_PaymentAccounts_PaymentAccountId",
                table: "CreditAccounts",
                column: "PaymentAccountId",
                principalTable: "PaymentAccounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CreditAccounts_Users_UserId",
                table: "CreditAccounts",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_BankOffices_BankOfficeId",
                table: "Employees",
                column: "BankOfficeId",
                principalTable: "BankOffices",
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

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentAccounts_Users_UserId",
                table: "PaymentAccounts",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BankAtms_BankOffices_BankOfficeId",
                table: "BankAtms");

            migrationBuilder.DropForeignKey(
                name: "FK_BankAtms_Banks_BankId",
                table: "BankAtms");

            migrationBuilder.DropForeignKey(
                name: "FK_BankAtms_Employees_EmployeeAccompanyingId",
                table: "BankAtms");

            migrationBuilder.DropForeignKey(
                name: "FK_BankOffices_Banks_BankId",
                table: "BankOffices");

            migrationBuilder.DropForeignKey(
                name: "FK_CreditAccounts_Banks_BankId",
                table: "CreditAccounts");

            migrationBuilder.DropForeignKey(
                name: "FK_CreditAccounts_Employees_EmployeeId",
                table: "CreditAccounts");

            migrationBuilder.DropForeignKey(
                name: "FK_CreditAccounts_PaymentAccounts_PaymentAccountId",
                table: "CreditAccounts");

            migrationBuilder.DropForeignKey(
                name: "FK_CreditAccounts_Users_UserId",
                table: "CreditAccounts");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_BankOffices_BankOfficeId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Banks_BankId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentAccounts_Banks_BankId",
                table: "PaymentAccounts");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentAccounts_Users_UserId",
                table: "PaymentAccounts");

            migrationBuilder.DropTable(
                name: "BankUser");

            migrationBuilder.DropIndex(
                name: "IX_BankOffices_BankId",
                table: "BankOffices");

            migrationBuilder.RenameColumn(
                name: "BankId",
                table: "Employees",
                newName: "BanksId");

            migrationBuilder.RenameIndex(
                name: "IX_Employees_BankId",
                table: "Employees",
                newName: "IX_Employees_BanksId");

            migrationBuilder.RenameColumn(
                name: "BankId",
                table: "BankOffices",
                newName: "AtmsTotal");

            migrationBuilder.AddColumn<long>(
                name: "AtmsTotal",
                table: "Banks",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "BankOfficesTotal",
                table: "Banks",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "CliensTotal",
                table: "Banks",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "EmployeesTotal",
                table: "Banks",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddForeignKey(
                name: "FK_BankAtms_BankOffices_BankOfficeId",
                table: "BankAtms",
                column: "BankOfficeId",
                principalTable: "BankOffices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BankAtms_Banks_BankId",
                table: "BankAtms",
                column: "BankId",
                principalTable: "Banks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BankAtms_Employees_EmployeeAccompanyingId",
                table: "BankAtms",
                column: "EmployeeAccompanyingId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CreditAccounts_Banks_BankId",
                table: "CreditAccounts",
                column: "BankId",
                principalTable: "Banks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CreditAccounts_Employees_EmployeeId",
                table: "CreditAccounts",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CreditAccounts_PaymentAccounts_PaymentAccountId",
                table: "CreditAccounts",
                column: "PaymentAccountId",
                principalTable: "PaymentAccounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CreditAccounts_Users_UserId",
                table: "CreditAccounts",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_BankOffices_BankOfficeId",
                table: "Employees",
                column: "BankOfficeId",
                principalTable: "BankOffices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Banks_BanksId",
                table: "Employees",
                column: "BanksId",
                principalTable: "Banks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentAccounts_Banks_BankId",
                table: "PaymentAccounts",
                column: "BankId",
                principalTable: "Banks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentAccounts_Users_UserId",
                table: "PaymentAccounts",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
