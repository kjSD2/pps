using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using pps.Data;
using pps.Models;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Xml.Linq;

namespace pps.Services
{
    public interface IBankService 
    {
        Bank GetBankById(uint id);
        List<Bank> GetBanksByCriteria(uint? id = null, string? name = null, byte? minRating = null, byte? maxRating = null, decimal? minMoneyTotal = null,
                                   decimal? maxMoneyTotal = null, float? minInterestrate = null, float? maxInterestrate = null);
        Bank CreateBank(BankCreate bank);
        BankResponse MapToBankResponse(Bank bank);
        void UpdateBank(Bank bank, BankUpdate bankUpdate);
        void DeleteBank(Bank bank);
    }

    public class BankService : IBankService
    {
        private AppDbContext _context;

        public BankService(AppDbContext context)
        {
            _context = context;
        }

        public Bank GetBankById(uint id)
        {
            var query = _context.Banks.Include(b => b.BankOffices).AsQueryable();
            return query.FirstOrDefault(b => b.Id == id);
        }
        
        public List<Bank> GetBanksByCriteria(uint? id = null, string? name = null, byte? minRating = null, byte? maxRating = null, decimal? minMoneyTotal = null,
                                   decimal? maxMoneyTotal = null, float? minInterestrate = null, float? maxInterestrate = null)
        {
            var query = _context.Banks.Include(b => b.BankOffices).AsQueryable();

            if (id.HasValue)
                query = query.Where(bank => bank.Id == id);
            if (!string.IsNullOrEmpty(name))
                query = query.Where(bank => bank.Name.Contains(name));
            if (minRating.HasValue)
                query = query.Where(bank => bank.Rating >= minRating);
            if (maxRating.HasValue)
                query = query.Where(bank => bank.Rating <= maxRating);
            if (minMoneyTotal.HasValue)
                query = query.Where(bank => bank.MoneyTotal >= minMoneyTotal);
            if (maxMoneyTotal.HasValue)
                query = query.Where(bank => bank.MoneyTotal <= maxMoneyTotal);
            if (minInterestrate.HasValue)
                query = query.Where(bank => bank.Interestrate >= minInterestrate);
            if (maxInterestrate.HasValue)
                query = query.Where(bank => bank.Interestrate <= maxInterestrate);

            return query.ToList();
        }

        public Bank CreateBank(BankCreate bank)
        {
            Bank newBank = new Bank(bank.Name, bank.Rating, bank.MoneyTotal, bank.Interestrate);
            _context.Banks.Add(newBank);
            _context.SaveChanges();
            return newBank;
        }

        public BankResponse MapToBankResponse(Bank bank)
        {
            return new BankResponse
            {
                Id = bank.Id,
                Name = bank.Name,
                Rating = bank.Rating,
                MoneyTotal = bank.MoneyTotal,
                InterestRate = bank.Interestrate,
                BankOfficeTotal = bank.BankOffices.Count,
                BankOffices = bank.BankOffices.Select(bo => new BankOfficeResponseShort
                {
                    Id = bo.Id,
                    Name = bo.Name
                }).ToList()
            };
        }

        public void UpdateBank(Bank bank, BankUpdate bankUpdate)
        {
            bank.Name = string.IsNullOrEmpty(bankUpdate.Name) ? bank.Name : bankUpdate.Name;
            bank.Rating = bankUpdate.Rating.HasValue ? bankUpdate.Rating.Value : bank.Rating;
            bank.MoneyTotal = bankUpdate.MoneyTotal.HasValue ? bankUpdate.MoneyTotal.Value : bank.MoneyTotal;
            bank.Interestrate = bankUpdate.Interestrate.HasValue ? bankUpdate.Interestrate.Value : bank.Interestrate;
            _context.SaveChanges();
        }

        public void DeleteBank(Bank bank)
        {
            _context.Banks.Remove(bank);
            _context.SaveChanges();
        }
    }

}
