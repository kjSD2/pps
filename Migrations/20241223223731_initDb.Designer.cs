﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using pps.Data;

#nullable disable

namespace pps.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20241223223731_initDb")]
    partial class initDb
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("pps.Models.Bank", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<long>("AtmsTotal")
                        .HasColumnType("bigint");

                    b.Property<long>("BankOfficesTotal")
                        .HasColumnType("bigint");

                    b.Property<long>("CliensTotal")
                        .HasColumnType("bigint");

                    b.Property<long>("EmployeesTotal")
                        .HasColumnType("bigint");

                    b.Property<float>("Interestrate")
                        .HasColumnType("real");

                    b.Property<decimal>("MoneyTotal")
                        .HasColumnType("numeric");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<byte>("Rating")
                        .HasColumnType("smallint");

                    b.HasKey("Id");

                    b.ToTable("Banks");
                });

            modelBuilder.Entity("pps.Models.BankAtm", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("Adress")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<long>("BankId")
                        .HasColumnType("bigint");

                    b.Property<long>("BankOfficeId")
                        .HasColumnType("bigint");

                    b.Property<long>("EmployeeAccompanyingId")
                        .HasColumnType("bigint");

                    b.Property<bool>("IsDepositMoney")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsGiveMoney")
                        .HasColumnType("boolean");

                    b.Property<decimal>("MaintenanceCost")
                        .HasColumnType("numeric");

                    b.Property<decimal>("MoneyTotal")
                        .HasColumnType("numeric");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("BankId");

                    b.HasIndex("BankOfficeId");

                    b.HasIndex("EmployeeAccompanyingId");

                    b.ToTable("BankAtms");
                });

            modelBuilder.Entity("pps.Models.BankOffice", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<long>("AtmsTotal")
                        .HasColumnType("bigint");

                    b.Property<bool>("IsDepositMoney")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsGiveCredit")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsGiveMoney")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsPlaceBankAtm")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsWork")
                        .HasColumnType("boolean");

                    b.Property<decimal>("MoneyTotal")
                        .HasColumnType("numeric");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal>("RentalCost")
                        .HasColumnType("numeric");

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("BankOffices");
                });

            modelBuilder.Entity("pps.Models.CreditAccount", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<long>("BankId")
                        .HasColumnType("bigint");

                    b.Property<long>("EmployeeId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<float>("Interestrate")
                        .HasColumnType("real");

                    b.Property<decimal>("MoneyTotal")
                        .HasColumnType("numeric");

                    b.Property<decimal>("MonthlyPayment")
                        .HasColumnType("numeric");

                    b.Property<long>("MountTotal")
                        .HasColumnType("bigint");

                    b.Property<long>("PaymentAccountId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("BankId");

                    b.HasIndex("EmployeeId");

                    b.HasIndex("PaymentAccountId");

                    b.HasIndex("UserId");

                    b.ToTable("CreditAccounts");
                });

            modelBuilder.Entity("pps.Models.Employee", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<long>("BankOfficeId")
                        .HasColumnType("bigint");

                    b.Property<long>("BanksId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("Birthday")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsGiveCredit")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsRemoteWork")
                        .HasColumnType("boolean");

                    b.Property<string>("JobTitle")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal>("Salary")
                        .HasColumnType("numeric");

                    b.HasKey("Id");

                    b.HasIndex("BankOfficeId");

                    b.HasIndex("BanksId");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("pps.Models.PaymentAccount", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<long>("BankId")
                        .HasColumnType("bigint");

                    b.Property<decimal>("MoneyTotal")
                        .HasColumnType("numeric");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("BankId");

                    b.HasIndex("UserId");

                    b.ToTable("PaymentAccounts");
                });

            modelBuilder.Entity("pps.Models.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<byte>("CreditRating")
                        .HasColumnType("smallint");

                    b.Property<decimal>("MonthlyIncome")
                        .HasColumnType("numeric");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("pps.Models.BankAtm", b =>
                {
                    b.HasOne("pps.Models.Bank", "Bank")
                        .WithMany()
                        .HasForeignKey("BankId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("pps.Models.BankOffice", "BankOffice")
                        .WithMany()
                        .HasForeignKey("BankOfficeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("pps.Models.Employee", "EmployeeAccompanying")
                        .WithMany()
                        .HasForeignKey("EmployeeAccompanyingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Bank");

                    b.Navigation("BankOffice");

                    b.Navigation("EmployeeAccompanying");
                });

            modelBuilder.Entity("pps.Models.CreditAccount", b =>
                {
                    b.HasOne("pps.Models.Bank", "Bank")
                        .WithMany()
                        .HasForeignKey("BankId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("pps.Models.Employee", "Employee")
                        .WithMany()
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("pps.Models.PaymentAccount", "PaymentAccount")
                        .WithMany()
                        .HasForeignKey("PaymentAccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("pps.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Bank");

                    b.Navigation("Employee");

                    b.Navigation("PaymentAccount");

                    b.Navigation("User");
                });

            modelBuilder.Entity("pps.Models.Employee", b =>
                {
                    b.HasOne("pps.Models.BankOffice", "BankOffice")
                        .WithMany()
                        .HasForeignKey("BankOfficeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("pps.Models.Bank", "Banks")
                        .WithMany()
                        .HasForeignKey("BanksId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BankOffice");

                    b.Navigation("Banks");
                });

            modelBuilder.Entity("pps.Models.PaymentAccount", b =>
                {
                    b.HasOne("pps.Models.Bank", "Bank")
                        .WithMany()
                        .HasForeignKey("BankId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("pps.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Bank");

                    b.Navigation("User");
                });
#pragma warning restore 612, 618
        }
    }
}
