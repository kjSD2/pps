using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace pps.Migrations
{
    /// <inheritdoc />
    public partial class initDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BankOffices",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Street = table.Column<string>(type: "text", nullable: false),
                    IsWork = table.Column<bool>(type: "boolean", nullable: false),
                    IsPlaceBankAtm = table.Column<bool>(type: "boolean", nullable: false),
                    AtmsTotal = table.Column<long>(type: "bigint", nullable: false),
                    IsGiveCredit = table.Column<bool>(type: "boolean", nullable: false),
                    IsGiveMoney = table.Column<bool>(type: "boolean", nullable: false),
                    IsDepositMoney = table.Column<bool>(type: "boolean", nullable: false),
                    MoneyTotal = table.Column<decimal>(type: "numeric", nullable: false),
                    RentalCost = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankOffices", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Banks",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    BankOfficesTotal = table.Column<long>(type: "bigint", nullable: false),
                    AtmsTotal = table.Column<long>(type: "bigint", nullable: false),
                    EmployeesTotal = table.Column<long>(type: "bigint", nullable: false),
                    CliensTotal = table.Column<long>(type: "bigint", nullable: false),
                    Rating = table.Column<byte>(type: "smallint", nullable: false),
                    MoneyTotal = table.Column<decimal>(type: "numeric", nullable: false),
                    Interestrate = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Banks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    MonthlyIncome = table.Column<decimal>(type: "numeric", nullable: false),
                    CreditRating = table.Column<byte>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FullName = table.Column<string>(type: "text", nullable: false),
                    Birthday = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    JobTitle = table.Column<string>(type: "text", nullable: false),
                    IsRemoteWork = table.Column<bool>(type: "boolean", nullable: false),
                    IsGiveCredit = table.Column<bool>(type: "boolean", nullable: false),
                    Salary = table.Column<decimal>(type: "numeric", nullable: false),
                    BanksId = table.Column<long>(type: "bigint", nullable: false),
                    BankOfficeId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employees_BankOffices_BankOfficeId",
                        column: x => x.BankOfficeId,
                        principalTable: "BankOffices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Employees_Banks_BanksId",
                        column: x => x.BanksId,
                        principalTable: "Banks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PaymentAccounts",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    MoneyTotal = table.Column<decimal>(type: "numeric", nullable: false),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    BankId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentAccounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PaymentAccounts_Banks_BankId",
                        column: x => x.BankId,
                        principalTable: "Banks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PaymentAccounts_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BankAtms",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Adress = table.Column<string>(type: "text", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false),
                    IsGiveMoney = table.Column<bool>(type: "boolean", nullable: false),
                    IsDepositMoney = table.Column<bool>(type: "boolean", nullable: false),
                    MoneyTotal = table.Column<decimal>(type: "numeric", nullable: false),
                    MaintenanceCost = table.Column<decimal>(type: "numeric", nullable: false),
                    BankId = table.Column<long>(type: "bigint", nullable: false),
                    BankOfficeId = table.Column<long>(type: "bigint", nullable: false),
                    EmployeeAccompanyingId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankAtms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BankAtms_BankOffices_BankOfficeId",
                        column: x => x.BankOfficeId,
                        principalTable: "BankOffices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BankAtms_Banks_BankId",
                        column: x => x.BankId,
                        principalTable: "Banks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BankAtms_Employees_EmployeeAccompanyingId",
                        column: x => x.EmployeeAccompanyingId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CreditAccounts",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    StartDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EndDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    MountTotal = table.Column<long>(type: "bigint", nullable: false),
                    MoneyTotal = table.Column<decimal>(type: "numeric", nullable: false),
                    MonthlyPayment = table.Column<decimal>(type: "numeric", nullable: false),
                    Interestrate = table.Column<float>(type: "real", nullable: false),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    BankId = table.Column<long>(type: "bigint", nullable: false),
                    PaymentAccountId = table.Column<long>(type: "bigint", nullable: false),
                    EmployeeId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CreditAccounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CreditAccounts_Banks_BankId",
                        column: x => x.BankId,
                        principalTable: "Banks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CreditAccounts_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CreditAccounts_PaymentAccounts_PaymentAccountId",
                        column: x => x.PaymentAccountId,
                        principalTable: "PaymentAccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CreditAccounts_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BankAtms_BankId",
                table: "BankAtms",
                column: "BankId");

            migrationBuilder.CreateIndex(
                name: "IX_BankAtms_BankOfficeId",
                table: "BankAtms",
                column: "BankOfficeId");

            migrationBuilder.CreateIndex(
                name: "IX_BankAtms_EmployeeAccompanyingId",
                table: "BankAtms",
                column: "EmployeeAccompanyingId");

            migrationBuilder.CreateIndex(
                name: "IX_CreditAccounts_BankId",
                table: "CreditAccounts",
                column: "BankId");

            migrationBuilder.CreateIndex(
                name: "IX_CreditAccounts_EmployeeId",
                table: "CreditAccounts",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_CreditAccounts_PaymentAccountId",
                table: "CreditAccounts",
                column: "PaymentAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_CreditAccounts_UserId",
                table: "CreditAccounts",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_BankOfficeId",
                table: "Employees",
                column: "BankOfficeId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_BanksId",
                table: "Employees",
                column: "BanksId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentAccounts_BankId",
                table: "PaymentAccounts",
                column: "BankId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentAccounts_UserId",
                table: "PaymentAccounts",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BankAtms");

            migrationBuilder.DropTable(
                name: "CreditAccounts");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "PaymentAccounts");

            migrationBuilder.DropTable(
                name: "BankOffices");

            migrationBuilder.DropTable(
                name: "Banks");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
