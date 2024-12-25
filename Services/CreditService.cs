using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using pps.Models;
using System.Collections.ObjectModel;
using System.Linq;

namespace pps.Services
{
    public interface ICreditService
    {
        IActionResult getCredit(decimal requestedAmount, uint userId, uint? paymentAccountId = null);
    }

    public class CreditService : ICreditService
    {
        IBankService _bankService;
        IBankOfficeService _bankOfficeService;
        IEmployeeService _employeeService;
        IUserService _userSetvice;
        IPaymentAccountService _paymentAccountService;
        IBankAtmService _bankAtmService;
        ICreditAccountService _creditAccountService;

        public CreditService(IBankService bankService, IBankOfficeService officeService, IEmployeeService employeeService, IUserService userSetvice,
                             IPaymentAccountService paymentAccountService, IBankAtmService bankAtmService, ICreditAccountService creditAccountService)
        {
            _bankService = bankService;
            _bankOfficeService = officeService;
            _employeeService = employeeService;
            _userSetvice = userSetvice;
            _paymentAccountService = paymentAccountService;
            _bankAtmService = bankAtmService;
            _creditAccountService = creditAccountService;
        }

        public IActionResult getCredit(decimal requestedAmount, uint userId, uint? paymentAccountId = null)
        {
            var bestBank = _bankService.GetBanksByCriteria().Select(bank => new
            {
                Bank = bank,
                OfficeCount = bank.BankOffices.Count,
                EmployeeCount = bank.BankOffices.Sum(bo => bo.Employees.Count),
                AtmsCount = bank.BankOffices.Sum(bo => bo.Employees.Sum(e => e.BankAtms.Count)),
                InterestrateCoef = 100 - bank.Interestrate
            }).OrderByDescending(b => (b.OfficeCount + b.EmployeeCount + b.EmployeeCount) * b.InterestrateCoef).FirstOrDefault();

            if (bestBank == null)
                return new ObjectResult(new { message = "Не найдено подходящего банка." })
                {
                    StatusCode = 404
                };

            Bank bank = _bankService.GetBankById(bestBank.Bank.Id);

            var bestBankOffices = _bankOfficeService.GetBankOfficesByCriteria().Where(bo => bo.Bank.Id == bestBank.Bank.Id &&
                bo.IsGiveMoney && bo.IsGiveCredit && bo.MoneyTotal >= requestedAmount &&
                bo.Employees.Any(e => e.BankAtms.Any(atm => atm.MoneyTotal >= requestedAmount) && e.IsGiveCredit)).FirstOrDefault();

            if (bestBankOffices == null)
                return new ObjectResult(new { message = "Не найдено подходящего банковского офиса." })
                {
                    StatusCode = 404
                };

            var bestAtm = bestBankOffices.Employees.SelectMany(e => e.BankAtms).Where(atm => atm.MoneyTotal >= requestedAmount && atm.EmployeeAccompanying.IsGiveCredit)
                .FirstOrDefault();

            if (bestAtm == null)
                return new ObjectResult(new { message = "Не найдено подходящего банкомата." })
                {
                    StatusCode = 404
                };

            var bestEmployee = bestAtm.EmployeeAccompanying;

            var user = _userSetvice.GetUserById(userId);
            if (user == null)
                return new ObjectResult(new { message = "Пользователь не найден." })
                {
                    StatusCode = 404
                };

            PaymentAccount paymentAccount;
            if (paymentAccountId.HasValue)
                paymentAccount = _paymentAccountService.GetPaymentAccountById(paymentAccountId.Value);
            else
                paymentAccount = _paymentAccountService.CreatePaymentAccount(100000, user.Id);

            _creditAccountService.CreateCreditAccount(new DateTime(10, 10, 10), new DateTime(11, 11, 11), 22,
                requestedAmount, requestedAmount / 22, bank.Interestrate, paymentAccount.Id, bestEmployee.Id);

            return new ObjectResult(new
            {
                bankId = bestBank.Bank.Id,
                bankOfficeId = bestBankOffices.Id,
                employeeId = bestEmployee.Id,
                bankAtmId = bestAtm.Id
            })
            {
                StatusCode = 200 // Устанавливаем статус код для успешного запроса
            };
        }
    }
}
