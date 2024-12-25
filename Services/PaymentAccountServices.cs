using Microsoft.EntityFrameworkCore;
using pps.Data;
using pps.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.IO;
using System.Xml.Linq;

namespace pps.Services
{
    public interface IPaymentAccountService
    {
        PaymentAccount GetPaymentAccountById(uint id);
        List<PaymentAccount> GetPaymentAccountByCriteria(decimal? minMoneyTotal, decimal? maxMoneyTotal, uint? userId);
        PaymentAccount CreatePaymentAccount(PaymentAccountCreate paymentAccount);
        PaymentAccount CreatePaymentAccount(decimal moneyTotal, uint userId);
        PaymentAccountResponse MapToPaymentAccountResponse(PaymentAccount paymentAccount);
        void UpdatePaymentAccount(PaymentAccount paymentAccount, PaymentAccountUpdate paymentAccountUpdate);
        void DeletePaymentAccount(PaymentAccount paymentAccount);
    }

    public class PaymentAccountService : IPaymentAccountService
    {
        private AppDbContext _context;
        private IUserService _userService;

        public PaymentAccountService(AppDbContext context, IUserService userService)
        {
            _context = context;
            _userService = userService;
        }

        public PaymentAccount GetPaymentAccountById(uint id)
        {
            var query = _context.PaymentAccounts.Include(pa => pa.User).AsQueryable();
            return query.FirstOrDefault(bo => bo.Id == id);
        }

        public List<PaymentAccount> GetPaymentAccountByCriteria(decimal? minMoneyTotal, decimal? maxMoneyTotal, uint? userId)
        {
            var query = _context.PaymentAccounts.Include(pa => pa.User).AsQueryable();

            if (minMoneyTotal.HasValue)
                query = query.Where(e => e.MoneyTotal >= minMoneyTotal);
            if (maxMoneyTotal.HasValue)
                query = query.Where(e => e.MoneyTotal <= maxMoneyTotal);
            if (userId.HasValue)
                query = query.Where(e => e.User.Id == userId);

            return query.ToList();
        }

        public PaymentAccount CreatePaymentAccount(PaymentAccountCreate paymentAccount)
        {
            PaymentAccount newPaymentAccount = new PaymentAccount(paymentAccount.MoneyTotal, _userService.GetUserById(paymentAccount.UserId));
            _context.PaymentAccounts.Add(newPaymentAccount);
            _context.SaveChanges();
            return newPaymentAccount;
        }

        public PaymentAccount CreatePaymentAccount(decimal moneyTotal, uint userId)
        {
            PaymentAccount newPaymentAccount = new PaymentAccount(moneyTotal, _userService.GetUserById(userId));
            _context.PaymentAccounts.Add(newPaymentAccount);
            _context.SaveChanges();
            return newPaymentAccount;
        }

        public PaymentAccountResponse MapToPaymentAccountResponse(PaymentAccount paymentAccount)
        {
            return new PaymentAccountResponse
            {
                Id = paymentAccount.Id,
                MoneyTotal = paymentAccount.MoneyTotal,
                User = new UserResponseShort
                {
                    Id = paymentAccount.User.Id,
                    FullName = paymentAccount.User.FullName
                }
            };
        }

        public void UpdatePaymentAccount(PaymentAccount paymentAccount, PaymentAccountUpdate paymentAccountUpdate)
        {
            if (paymentAccountUpdate.MoneyTotal.HasValue)
                paymentAccount.MoneyTotal = paymentAccountUpdate.MoneyTotal.Value;
            if (paymentAccountUpdate.UserId.HasValue)
                paymentAccount.User = _userService.GetUserById(paymentAccountUpdate.UserId.Value);

            _context.SaveChanges();
        }

        public void DeletePaymentAccount(PaymentAccount paymentAccount)
        {
            _context.PaymentAccounts.Remove(paymentAccount);
            _context.SaveChanges();
        }
    }
}
