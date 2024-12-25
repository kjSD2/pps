using Microsoft.EntityFrameworkCore;
using pps.Data;
using pps.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.IO;
using System.Xml.Linq;

namespace pps.Services
{
    public interface IBankOfficeService
    {
        BankOffice GetBankOfficeById(uint id);
        List<BankOffice> GetBankOfficesByCriteria(string? name = null, string? street = null, bool? isWork = null, bool? isPlaceBankAtm = null, 
                                                  bool? isGiveCredit = null, bool? isGiveMoney = null, bool? isDepositMoney = null, uint? bankId = null,
                                                  decimal? minMoneyTotal = null, decimal? maxMoneyTotal = null, decimal? minRentalCost = null, decimal? maxRentalCost = null);
        BankOffice CreateBankOffice(BankOfficeCreate bank);
        BankOfficeResponse MapToBankOfficeResponse(BankOffice bank);
        void UpdateBankOffice(BankOffice bank, BankOfficeUpdate bankUpdate);
        void DeleteBankOffice(BankOffice bank);
    }

    public class BankOfficeService : IBankOfficeService
    {
        private AppDbContext _context;
        private IBankService _bankService;

        public BankOfficeService(AppDbContext context, IBankService bankService)
        {
            _context = context;
            _bankService = bankService;
        }

        public BankOffice GetBankOfficeById(uint id)
        {
            var query = _context.BankOffices.Include(bo => bo.Employees).Include(bo => bo.Bank).AsQueryable();
            return query.FirstOrDefault(bo => bo.Id == id);
        }

        public List<BankOffice> GetBankOfficesByCriteria(string? name = null, string? street = null, bool? isWork = null, bool? isPlaceBankAtm = null, 
                                                         bool? isGiveCredit = null, bool? isGiveMoney = null, bool? isDepositMoney = null, uint? bankId = null,
                                                         decimal? minMoneyTotal = null, decimal? maxMoneyTotal = null, decimal? minRentalCost = null,
                                                         decimal? maxRentalCost = null)
        {
            var query = _context.BankOffices.Include(bo => bo.Employees).Include(bo => bo.Bank).AsQueryable();

            if (!string.IsNullOrEmpty(name))
                query = query.Where(bankOffice => bankOffice.Name.Contains(name));
            if (!string.IsNullOrEmpty(street))
                query = query.Where(bankOffice => bankOffice.Street.Contains(street));
            if (isWork.HasValue)
                query = query.Where(bankOffice => bankOffice.IsWork == isWork);
            if (isPlaceBankAtm.HasValue)
                query = query.Where(bankOffice => bankOffice.IsPlaceBankAtm == isPlaceBankAtm);
            if (isGiveCredit.HasValue)
                query = query.Where(bankOffice => bankOffice.IsGiveCredit == isGiveCredit);
            if (isGiveMoney.HasValue)
                query = query.Where(bankOffice => bankOffice.IsGiveMoney == isGiveMoney);
            if (isDepositMoney.HasValue)
                query = query.Where(bankOffice => bankOffice.IsDepositMoney == isDepositMoney);
            if (bankId.HasValue)
                query = query.Where(bankOffice => bankOffice.Bank.Id == bankId);
            if (minMoneyTotal.HasValue)
                query = query.Where(bankOffice => bankOffice.MoneyTotal >= minMoneyTotal);
            if (maxMoneyTotal.HasValue)
                query = query.Where(bankOffice => bankOffice.MoneyTotal <= maxMoneyTotal);
            if (minRentalCost.HasValue)
                query = query.Where(bankOffice => bankOffice.RentalCost >= minRentalCost);
            if (maxRentalCost.HasValue)
                query = query.Where(bankOffice => bankOffice.RentalCost <= maxRentalCost);

            return query.ToList();
        }

        public BankOffice CreateBankOffice(BankOfficeCreate bankOffice)
        {
            BankOffice newBank = new BankOffice(bankOffice.Name, bankOffice.Street, bankOffice.IsWork, bankOffice.IsPlaceBankAtm,
                bankOffice.IsGiveCredit, bankOffice.IsGiveMoney, bankOffice.IsDepositMoney, bankOffice.MoneyTotal, bankOffice.RentalCost,
                _bankService.GetBankById(bankOffice.BankId));
            _context.BankOffices.Add(newBank);
            _context.SaveChanges();
            return newBank;
        }

        public BankOfficeResponse MapToBankOfficeResponse(BankOffice bankOffice)
        {
            return new BankOfficeResponse
            {
                Id = bankOffice.Id,
                Name = bankOffice.Name,
                Street = bankOffice.Street,
                IsWork = bankOffice.IsWork,
                IsPlaceBankAtm = bankOffice.IsPlaceBankAtm,
                IsGiveCredit = bankOffice.IsGiveCredit,
                IsGiveMoney = bankOffice.IsGiveMoney,
                IsDepositMoney = bankOffice.IsDepositMoney,
                Bank = new BankResponseShort
                {
                    Id = bankOffice.Bank.Id,
                    Name = bankOffice.Bank.Name
                },
                //BankId = bankOffice.Bank.Id,
                MoneyTotal = bankOffice.MoneyTotal,
                RentalCost = bankOffice.RentalCost,
                EmployeeTotal = bankOffice.Employees.Count,
                Employees = bankOffice.Employees.Select(e => new EmployeeResponseShort
                {
                    Id = e.Id,
                    FullName = e.FullName
                }).ToList()
            };
        }

        public void UpdateBankOffice(BankOffice bankOffice, BankOfficeUpdate bankOfficeUpdate)
        {
            if (!string.IsNullOrEmpty(bankOfficeUpdate.Name))
                bankOffice.Name = bankOfficeUpdate.Name;
            if (!string.IsNullOrEmpty(bankOfficeUpdate.Street))
                bankOffice.Street = bankOfficeUpdate.Street;
            if (bankOfficeUpdate.IsWork.HasValue)
                bankOffice.IsWork = bankOfficeUpdate.IsWork.Value;
            if (bankOfficeUpdate.IsPlaceBankAtm.HasValue)
                bankOffice.IsPlaceBankAtm = bankOfficeUpdate.IsPlaceBankAtm.Value;
            if (bankOfficeUpdate.IsGiveCredit.HasValue)
                bankOffice.IsGiveCredit = bankOfficeUpdate.IsGiveCredit.Value;
            if (bankOfficeUpdate.IsGiveMoney.HasValue)
                bankOffice.IsGiveMoney = bankOfficeUpdate.IsGiveMoney.Value;
            if (bankOfficeUpdate.IsDepositMoney.HasValue)
                bankOffice.IsDepositMoney = bankOfficeUpdate.IsDepositMoney.Value;
            if (bankOfficeUpdate.BankId.HasValue)
                bankOffice.Bank = _bankService.GetBankById(bankOfficeUpdate.BankId.Value);
            if (bankOfficeUpdate.MoneyTotal.HasValue)
                bankOffice.MoneyTotal = bankOfficeUpdate.MoneyTotal.Value;
            if (bankOfficeUpdate.RentalCost.HasValue)
                bankOffice.RentalCost = bankOfficeUpdate.RentalCost.Value;

            _context.SaveChanges();
        }

        public void DeleteBankOffice(BankOffice bankOffice)
        {
            _context.BankOffices.Remove(bankOffice);
            _context.SaveChanges();
        }
    }
}
