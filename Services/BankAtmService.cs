using Microsoft.EntityFrameworkCore;
using pps.Data;
using pps.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.IO;
using System.Xml.Linq;

namespace pps.Services
{
    public interface IBankAtmService
    {
        BankAtm GetBankAtmById(uint id);
        List<BankAtm> GetBankAtmByCriteria(string? name, string? status, bool? isGiveMoney, bool? isDepositMoney, decimal? minMoneyTotal,
                                                   decimal? maxMoneyTotal, decimal? minMaintenanceCost, decimal? maxMaintenanceCost, uint? EmployeeAccompanyingId);
        BankAtm CreateBankAtm(BankAtmCreate bankAtmCreate);
        BankAtmResponse MapToBankAtmResponse(BankAtm bankAtm);
        void UpdateBankAtm(BankAtm bankAtm, BankAtmUpdate bankAtmUpdate);
        void DeleteBankAtm(BankAtm bankAtm);
    }

    public class BankAtmService : IBankAtmService
    {
        private AppDbContext _context;
        private IEmployeeService _employeeService;

        public BankAtmService(AppDbContext context, IEmployeeService employeeService)
        {
            _context = context;
            _employeeService = employeeService;
        }

        public BankAtm GetBankAtmById(uint id)
        {
            var query = _context.BankAtms.Include(b => b.EmployeeAccompanying).AsQueryable();
            return query.FirstOrDefault(bo => bo.Id == id);
        }

        public List<BankAtm> GetBankAtmByCriteria(string? name, string? status, bool? isGiveMoney, bool? isDepositMoney, decimal? minMoneyTotal,
                                                   decimal? maxMoneyTotal, decimal? minMaintenanceCost, decimal? maxMaintenanceCost, uint? EmployeeAccompanyingId)
        {
            var query = _context.BankAtms.Include(b => b.EmployeeAccompanying).AsQueryable();

            if (!string.IsNullOrEmpty(name))
                query = query.Where(e => e.Name.Contains(name));
            if (!string.IsNullOrEmpty(status))
                query = query.Where(e => e.Status.Contains(status));
            if (isGiveMoney.HasValue)
                query = query.Where(e => e.IsGiveMoney == isGiveMoney);
            if (isDepositMoney.HasValue)
                query = query.Where(e => e.IsDepositMoney == isDepositMoney);
            if (minMoneyTotal.HasValue)
                query = query.Where(e => e.MoneyTotal >= minMoneyTotal);
            if (maxMoneyTotal.HasValue)
                query = query.Where(e => e.MoneyTotal <= maxMoneyTotal);
            if (minMaintenanceCost.HasValue)
                query = query.Where(e => e.MaintenanceCost >= minMaintenanceCost);
            if (maxMaintenanceCost.HasValue)
                query = query.Where(e => e.MaintenanceCost <= maxMaintenanceCost);
            if (EmployeeAccompanyingId.HasValue)
                query = query.Where(e => e.EmployeeAccompanying.Id == EmployeeAccompanyingId);

            return query.ToList();
        }

        public BankAtm CreateBankAtm(BankAtmCreate bankAtmCreate)
        {
            BankAtm bankAtm = new BankAtm(bankAtmCreate.Name, bankAtmCreate.Status, bankAtmCreate.IsGiveMoney, bankAtmCreate.IsDepositMoney, bankAtmCreate.MoneyTotal,
                                          bankAtmCreate.MaintenanceCost, _employeeService.GetEmployeeById(bankAtmCreate.EmployeeAccompanyingId));
            _context.BankAtms.Add(bankAtm);
            _context.SaveChanges();
            return bankAtm;
        }

        public BankAtmResponse MapToBankAtmResponse(BankAtm bankAtm)
        {
            return new BankAtmResponse
            {
                Id = bankAtm.Id,
                Name = bankAtm.Name,
                Status = bankAtm.Status,
                IsGiveMoney = bankAtm.IsGiveMoney,
                IsDepositMoney = bankAtm.IsDepositMoney,
                MoneyTotal = bankAtm.MoneyTotal,
                MaintenanceCost = bankAtm.MaintenanceCost,
                EmployeeAccompanying = new EmployeeResponseShort
                {
                    Id = bankAtm.EmployeeAccompanying.Id,
                    FullName = bankAtm.EmployeeAccompanying.FullName
                }
            };
        }

        public void UpdateBankAtm(BankAtm bankAtm, BankAtmUpdate bankAtmUpdate)
        {
            if (!string.IsNullOrEmpty(bankAtmUpdate.Name))
                bankAtm.Name = bankAtmUpdate.Name;
            if (!string.IsNullOrEmpty(bankAtmUpdate.Status))
                bankAtm.Status = bankAtmUpdate.Status;
            if (bankAtmUpdate.IsGiveMoney.HasValue)
                bankAtm.IsGiveMoney = bankAtmUpdate.IsGiveMoney.Value;
            if (bankAtmUpdate.IsDepositMoney.HasValue)
                bankAtm.IsDepositMoney = bankAtmUpdate.IsDepositMoney.Value;
            if (bankAtmUpdate.IsDepositMoney.HasValue)
                bankAtm.IsDepositMoney = bankAtmUpdate.IsDepositMoney.Value;
            if (bankAtmUpdate.MoneyTotal.HasValue)
                bankAtm.MoneyTotal = bankAtmUpdate.MoneyTotal.Value;
            if (bankAtmUpdate.MaintenanceCost.HasValue)
                bankAtm.MaintenanceCost = bankAtmUpdate.MaintenanceCost.Value;
            if (bankAtmUpdate.EmployeeAccompanyingId.HasValue)
                bankAtm.EmployeeAccompanying = _employeeService.GetEmployeeById(bankAtmUpdate.EmployeeAccompanyingId.Value);

            _context.SaveChanges();
        }

        public void DeleteBankAtm(BankAtm bankAtm)
        {
            _context.BankAtms.Remove(bankAtm);
            _context.SaveChanges();
        }
    }
}
