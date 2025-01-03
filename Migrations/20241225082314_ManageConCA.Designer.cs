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
    [Migration("20241225082314_ManageConCA")]
    partial class ManageConCA
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

                    b.HasIndex("EmployeeAccompanyingId");

                    b.ToTable("BankAtms");
                });

            modelBuilder.Entity("pps.Models.BankOffice", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<long>("BankId")
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

                    b.HasIndex("BankId");

                    b.ToTable("BankOffices");
                });

            modelBuilder.Entity("pps.Models.CreditAccount", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

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

                    b.HasKey("Id");

                    b.HasIndex("EmployeeId");

                    b.HasIndex("PaymentAccountId");

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

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("pps.Models.PaymentAccount", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<decimal>("MoneyTotal")
                        .HasColumnType("numeric");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("PaymentAccounts");
                });

            modelBuilder.Entity("pps.Models.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<DateTime>("Birthday")
                        .HasColumnType("timestamp with time zone");

                    b.Property<byte>("CreditRating")
                        .HasColumnType("smallint");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal>("MonthlyIncome")
                        .HasColumnType("numeric");

                    b.Property<string>("PlaceWork")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("pps.Models.BankAtm", b =>
                {
                    b.HasOne("pps.Models.Employee", "EmployeeAccompanying")
                        .WithMany("BankAtms")
                        .HasForeignKey("EmployeeAccompanyingId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("EmployeeAccompanying");
                });

            modelBuilder.Entity("pps.Models.BankOffice", b =>
                {
                    b.HasOne("pps.Models.Bank", "Bank")
                        .WithMany("BankOffices")
                        .HasForeignKey("BankId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Bank");
                });

            modelBuilder.Entity("pps.Models.CreditAccount", b =>
                {
                    b.HasOne("pps.Models.Employee", "Employee")
                        .WithMany("CreditAccounts")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("pps.Models.PaymentAccount", "PaymentAccount")
                        .WithMany("CreditAccounts")
                        .HasForeignKey("PaymentAccountId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Employee");

                    b.Navigation("PaymentAccount");
                });

            modelBuilder.Entity("pps.Models.Employee", b =>
                {
                    b.HasOne("pps.Models.BankOffice", "BankOffice")
                        .WithMany("Employees")
                        .HasForeignKey("BankOfficeId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("BankOffice");
                });

            modelBuilder.Entity("pps.Models.PaymentAccount", b =>
                {
                    b.HasOne("pps.Models.User", "User")
                        .WithMany("PaymentAccounts")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("pps.Models.Bank", b =>
                {
                    b.Navigation("BankOffices");
                });

            modelBuilder.Entity("pps.Models.BankOffice", b =>
                {
                    b.Navigation("Employees");
                });

            modelBuilder.Entity("pps.Models.Employee", b =>
                {
                    b.Navigation("BankAtms");

                    b.Navigation("CreditAccounts");
                });

            modelBuilder.Entity("pps.Models.PaymentAccount", b =>
                {
                    b.Navigation("CreditAccounts");
                });

            modelBuilder.Entity("pps.Models.User", b =>
                {
                    b.Navigation("PaymentAccounts");
                });
#pragma warning restore 612, 618
        }
    }
}
