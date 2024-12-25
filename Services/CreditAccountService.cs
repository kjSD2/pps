using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using pps.Data;
using pps.Models;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Xml.Linq;

namespace pps.Services
{
    public interface ICreditAccountService
    {
        CreditAccount GetCreditAccountById(uint id);
        List<CreditAccount> GetCreditAccountByCriteria(DateTime? minStartDate, DateTime? maxStartDate, DateTime? minEndDate, DateTime? maxEndDate,
                                                              uint? minMountTotal, uint? maxMountTotal, decimal? minMoneyTotal, decimal? maxMoneyTotal,
                                                              decimal? minMonthlyPayment, decimal? maxMonthlyPayment, float? minInterestrate, float? maxInterestrate);
        CreditAccount CreateCreditAccount(CreditAccountCreate bank);
        CreditAccount CreateCreditAccount(DateTime startDate, DateTime endDate, uint mountTotal, decimal moneyTotal, decimal monthlyPayment,
                                                 float interestrate, uint paymentAccountId, uint employeeId);
        CreditAccountResponse MapToCreditAccountResponse(CreditAccount bank);
        void UpdateCreditAccount(CreditAccount bank, CreditAccountUpdate bankUpdate);
        void DeleteCreditAccount(CreditAccount bank);
    }

    public class CreditAccountService : ICreditAccountService
    {
        private AppDbContext _context;
        private IEmployeeService _employeeService;
        private IPaymentAccountService _paymentAccountService;

        public CreditAccountService(AppDbContext context, IEmployeeService employeeService, IPaymentAccountService paymentAccountService)
        {
            _context = context;
            _employeeService = employeeService;
            _paymentAccountService = paymentAccountService;
        }

        public CreditAccount GetCreditAccountById(uint id)
        {
            var query = _context.CreditAccounts.Include(ca => ca.PaymentAccount).Include(ca => ca.Employee).AsQueryable();
            return query.FirstOrDefault(b => b.Id == id);
        }

        public List<CreditAccount> GetCreditAccountByCriteria(DateTime? minStartDate, DateTime? maxStartDate, DateTime? minEndDate, DateTime? maxEndDate,
                                                              uint? minMountTotal, uint? maxMountTotal, decimal? minMoneyTotal, decimal? maxMoneyTotal,
                                                              decimal? minMonthlyPayment, decimal? maxMonthlyPayment, float? minInterestrate, float? maxInterestrate)
        {
            var query = _context.CreditAccounts.Include(ca => ca.PaymentAccount).Include(ca => ca.Employee).AsQueryable();

            if (minStartDate.HasValue)
                query = query.Where(e => e.StartDate >= minStartDate);
            if (maxStartDate.HasValue)
                query = query.Where(e => e.StartDate <= maxStartDate);
            
            if (minEndDate.HasValue)
                query = query.Where(e => e.EndDate >= minEndDate);
            if (maxEndDate.HasValue)
                query = query.Where(e => e.EndDate <= maxEndDate);
            
            if (minMountTotal.HasValue)
                query = query.Where(e => e.MountTotal >= minMountTotal);
            if (maxMountTotal.HasValue)
                query = query.Where(e => e.MountTotal <= maxMountTotal);
            
            if (minMoneyTotal.HasValue)
                query = query.Where(e => e.MoneyTotal >= minMoneyTotal);
            if (maxMoneyTotal.HasValue)
                query = query.Where(e => e.MoneyTotal <= maxMoneyTotal);
            
            if (minMonthlyPayment.HasValue)
                query = query.Where(e => e.MonthlyPayment >= minMonthlyPayment);
            if (maxMonthlyPayment.HasValue)
                query = query.Where(e => e.MonthlyPayment <= maxMonthlyPayment);
            
            if (minInterestrate.HasValue)
                query = query.Where(e => e.Interestrate >= minInterestrate);
            if (maxInterestrate.HasValue)
                query = query.Where(e => e.Interestrate <= maxInterestrate);

            return query.ToList();
        }

        public CreditAccount CreateCreditAccount(CreditAccountCreate creditAccountCreat)
        {
            CreditAccount newCreditAccount = new CreditAccount(creditAccountCreat.StartDate, creditAccountCreat.EndDate, creditAccountCreat.MountTotal, 
                                                      creditAccountCreat.MoneyTotal, creditAccountCreat.MonthlyPayment, creditAccountCreat.Interestrate,
                                                      _paymentAccountService.GetPaymentAccountById(creditAccountCreat.PaymentAccountId), 
                                                      _employeeService.GetEmployeeById(creditAccountCreat.EmployeeId));
            _context.CreditAccounts.Add(newCreditAccount);
            _context.SaveChanges();
            return newCreditAccount;
        }

        public CreditAccount CreateCreditAccount(DateTime startDate, DateTime endDate, uint mountTotal, decimal moneyTotal, decimal monthlyPayment,
                                                 float interestrate, uint paymentAccountId, uint employeeId)
        {
            CreditAccount newCreditAccount = new CreditAccount(startDate, endDate, mountTotal, moneyTotal, monthlyPayment, interestrate,
                                                      _paymentAccountService.GetPaymentAccountById(paymentAccountId),
                                                      _employeeService.GetEmployeeById(employeeId));
            _context.CreditAccounts.Add(newCreditAccount);
            _context.SaveChanges();
            return newCreditAccount;
        }

        public CreditAccountResponse MapToCreditAccountResponse(CreditAccount creditAccount)
        {
            return new CreditAccountResponse
            {
                StartDate = creditAccount.StartDate,
                EndDate = creditAccount.EndDate,
                MountTotal = creditAccount.MountTotal,
                MoneyTotal = creditAccount.MoneyTotal,
                MonthlyPayment = creditAccount.MonthlyPayment,
                Interestrate = creditAccount.Interestrate,
                PaymentAccount = new PaymentAccountResponseShort
                {
                    Id = creditAccount.PaymentAccount.Id,
                    MoneyTotal = creditAccount.PaymentAccount.MoneyTotal
                },
                Employee = new EmployeeResponseShort
                {
                    Id = creditAccount.Employee.Id,
                    FullName = creditAccount.Employee.FullName
                }
            };
        }

        public void UpdateCreditAccount(CreditAccount creditAccount, CreditAccountUpdate creditAccountUpdate)
        {
            if (creditAccountUpdate.StartDate.HasValue)
                creditAccount.StartDate = creditAccountUpdate.StartDate.Value;
            if (creditAccountUpdate.EndDate.HasValue)
                creditAccount.EndDate = creditAccountUpdate.EndDate.Value;
            if (creditAccountUpdate.MountTotal.HasValue)
                creditAccount.MountTotal = creditAccountUpdate.MountTotal.Value;
            if (creditAccountUpdate.MoneyTotal.HasValue)
                creditAccount.MoneyTotal = creditAccountUpdate.MoneyTotal.Value;
            if (creditAccountUpdate.StartDate.HasValue)
                creditAccount.MonthlyPayment = creditAccountUpdate.MonthlyPayment.Value;
            if (creditAccountUpdate.MonthlyPayment.HasValue)
                creditAccount.Interestrate = creditAccountUpdate.Interestrate.Value;
            if (creditAccountUpdate.PaymentAccountId.HasValue)
                creditAccount.PaymentAccount = _paymentAccountService.GetPaymentAccountById(creditAccountUpdate.PaymentAccountId.Value);
            if (creditAccountUpdate.EmployeeId.HasValue)
                creditAccount.Employee = _employeeService.GetEmployeeById(creditAccountUpdate.EmployeeId.Value);


            _context.SaveChanges();
        }

        public void DeleteCreditAccount(CreditAccount creditAccount)
        {
            _context.CreditAccounts.Remove(creditAccount);
            _context.SaveChanges();
        }
    }

}
