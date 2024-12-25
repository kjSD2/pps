using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using pps.Models;

namespace pps.Data
{
    public static class DbInitializer
    {
        public static void Initialize(AppDbContext context)
        {

            // Очистка базы данных (удаление всех данных)
            ClearDatabase(context);

            Random random = new Random();

            // Генерация банков
            for (uint bankId = 0; bankId < 5; bankId++)
            {
                Bank bank = new Bank(
                    $"Банк {bankId}",
                    (byte)random.Next(0, 6),
                    random.Next(100000, 10000000),
                    (float)(random.NextDouble() * (24 - 15) + 15)
                );

                ICollection<BankOffice> bankOffices = new List<BankOffice>();

                // Генерация банковских офисов
                for (uint bankOfficeId = 0; bankOfficeId < 3; bankOfficeId++)
                {
                    BankOffice bankOffice = new BankOffice(
                        $"Офис {bankId}_{bankOfficeId}",
                        $"Улица {bankId * 3 + bankOfficeId}",
                        true, true, true, true, true,
                        random.Next(10000, 1000000),
                        (decimal)random.Next(1000, 10000),
                        bank
                    ); 

                    ICollection<Employee> employees = new List<Employee>();

                    // Генерация сотрудников
                    for (uint employeeId = 0; employeeId < 3; employeeId++)
                    {
                        string position = employeeId switch
                        {
                            0 => "Менеджер",
                            1 => "Кассир",
                            2 => "Сотрудник безопасности",
                            _ => "Менеджер"
                        };

                        Employee employee = new Employee(
                            $"Сотрудник {bankId}_{bankOfficeId}_{employeeId}",
                            new DateTime(random.Next(1990, 2003), random.Next(1, 13), random.Next(1, 25)).ToUniversalTime(),
                            position,
                            position != "Сотрудник безопасности",
                            position == "Кассир",
                            30000m, bankOffice
                        );

                        ICollection<BankAtm> bankAtms = new List<BankAtm>();
                            for (uint atmId = 0; atmId < 2; atmId++)
                            {
                                BankAtm bankAtm = new BankAtm(
                                    $"ATM {bankId}_{bankOfficeId}_{employeeId}_{atmId}",
                                    "Работает",
                                    true,
                                    true,
                                    random.Next(1000, 100000),
                                    random.Next(100, 1000),
                                    employee
                                );
                                bankAtms.Add(bankAtm);
                            }
                        employee.BankAtms = bankAtms;

                        for (int userId = 0; userId < 10; userId++)
                        {
                            User user = new User($"user{bankId}_{userId}", new DateTime(random.Next(1990, 2003), random.Next(1, 13), random.Next(1, 25)).ToUniversalTime(),
                                                 $"userPlaceWork{bankId}_{userId}", random.Next(1000, 10000), (byte)random.Next(1, 5));

                            PaymentAccount paymentAccount1 = new PaymentAccount(random.Next(1000, 10000), user);
                            PaymentAccount paymentAccount2 = new PaymentAccount(random.Next(1000, 10000), user);

                            //var employee = bank.BankOffices.Employees.FirstOrDefault(e => e.Id == (bankId * 3 + 1));
                            if (employee != null)
                            {
                                CreditAccount creditAccount1 = new CreditAccount(
                                    new DateTime(random.Next(2002, 2010), random.Next(1, 13), random.Next(1, 25)).ToUniversalTime(),
                                    new DateTime(random.Next(2010, 2020), random.Next(1, 13), random.Next(1, 25)).ToUniversalTime(),
                                    100, 10000m, 100m, 20,
                                    paymentAccount1, employee
                                );

                                CreditAccount creditAccount2 = new CreditAccount(
                                    new DateTime(random.Next(2002, 2010), random.Next(1, 13), random.Next(1, 25)).ToUniversalTime(),
                                    new DateTime(random.Next(2010, 2020), random.Next(1, 13), random.Next(1, 25)).ToUniversalTime(),
                                    100, 10000m, 100m, 20,
                                    paymentAccount1, employee
                                );

                                paymentAccount1.CreditAccounts.Add(creditAccount1);
                                paymentAccount1.CreditAccounts.Add(creditAccount2);
                                user.PaymentAccounts.Add(paymentAccount1);
                                user.PaymentAccounts.Add(paymentAccount2);
                            }

                            context.Add(user);
                        }

                        employees.Add(employee);
                    }

                    bankOffice.Employees = employees;
                    bankOffices.Add(bankOffice);
                }

                bank.BankOffices = bankOffices;
                context.Banks.Add(bank);
            }
            
            context.SaveChanges();
        }

        // Функция для очистки базы данных
        public static void ClearDatabase(AppDbContext context)
        {
            var creditAccounts = context.CreditAccounts.ToList();
            context.CreditAccounts.RemoveRange(creditAccounts);

            var PaymentAccounts = context.PaymentAccounts.ToList();
            context.PaymentAccounts.RemoveRange(PaymentAccounts);

            var Users = context.Users.ToList();
            context.Users.RemoveRange(Users);

            var BankAtms = context.BankAtms.ToList();
            context.BankAtms.RemoveRange(BankAtms);

            var Employees = context.Employees.ToList();
            context.Employees.RemoveRange(Employees);

            var BankOffices = context.BankOffices.ToList();
            context.BankOffices.RemoveRange(BankOffices);

            var Banks = context.Banks.ToList();
            context.Banks.RemoveRange(Banks);

            context.SaveChanges();
        }

    }
}
