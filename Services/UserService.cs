using Microsoft.EntityFrameworkCore;
using pps.Data;
using pps.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.IO;
using System.Xml.Linq;

namespace pps.Services
{
    public interface IUserService
    {
        User GetUserById(uint id);
        List<User> GetUserByCriteria(string? fullName, DateTime? minBirthday, DateTime? maxBirthday, string? placeWork, decimal? minMonthlyIncome,
                                                    decimal? maxMonthlyIncome, byte? minCreditRating, byte? maxCreditRating);
        User CreateUser(UserCreate userCreate);
        UserResponse MapToUserResponse(User user);
        void UpdateUser(User user, UserUpdate userUpdate);
        void DeleteUser(User user);
    }

    public class UserService : IUserService
    {
        private AppDbContext _context;

        public UserService(AppDbContext context)
        {
            _context = context;
        }

        public User GetUserById(uint id)
        {
            var query = _context.Users.Include(u => u.PaymentAccounts).AsQueryable();
            return query.FirstOrDefault(bo => bo.Id == id);
        }

        /*
         * public string FullName { get; set; }
        public DateTime Birthday { get; set; }
        public string PlaceWork { get; set; }
        public decimal MonthlyIncome { get; set; }
        public byte CreditRating { get; set; }
         */
        // fullName, minBirthday, maxBirthday, PlaceWork, minMonthlyIncome, maxMonthlyIncome, minCreditRating, maxCreditRating
        public List<User> GetUserByCriteria(string? fullName, DateTime? minBirthday, DateTime? maxBirthday, string? placeWork, decimal? minMonthlyIncome,
                                                    decimal? maxMonthlyIncome, byte? minCreditRating, byte? maxCreditRating)
        {
            var query = _context.Users.Include(u => u.PaymentAccounts).AsQueryable();

            if (!string.IsNullOrEmpty(fullName))
                query = query.Where(e => e.FullName.Contains(fullName));
            if (minBirthday.HasValue)
                query = query.Where(e => e.Birthday >= minBirthday);
            if (maxBirthday.HasValue)
                query = query.Where(e => e.Birthday <= maxBirthday);
            if (!string.IsNullOrEmpty(placeWork))
                query = query.Where(e => e.PlaceWork.Contains(placeWork));
            if (minMonthlyIncome.HasValue)
                query = query.Where(e => e.MonthlyIncome >= minMonthlyIncome);
            if (maxMonthlyIncome.HasValue)
                query = query.Where(e => e.MonthlyIncome <= maxMonthlyIncome);
            if (minCreditRating.HasValue)
                query = query.Where(e => e.CreditRating >= minCreditRating);
            if (maxCreditRating.HasValue)
                query = query.Where(e => e.CreditRating <= maxCreditRating);

            return query.ToList();
        }

        public User CreateUser(UserCreate userCreate)
        {
            User user = new User(userCreate.FullName, userCreate.Birthday, userCreate.PlaceWork, userCreate.MonthlyIncome, userCreate.CreditRating);
            _context.Users.Add(user);
            _context.SaveChanges();
            return user;
        }

        public UserResponse MapToUserResponse(User user)
        {
            return new UserResponse
            {
                Id = user.Id,
                FullName = user.FullName,
                Birthday = user.Birthday,
                PlaceWork = user.PlaceWork,
                MonthlyIncome = user.MonthlyIncome,
                CreditRating = user.CreditRating,
                PaymentAccounts = user.PaymentAccounts.Select(pa => new PaymentAccountResponseShort
                {
                    Id = pa.Id,
                    MoneyTotal = pa.MoneyTotal
                }).ToList()
            };
        }

        public void UpdateUser(User user, UserUpdate userUpdate)
        {
            if (!string.IsNullOrEmpty(userUpdate.FullName))
                user.FullName = userUpdate.FullName;
            if (userUpdate.Birthday.HasValue)
                user.Birthday = userUpdate.Birthday.Value;
            if (!string.IsNullOrEmpty(userUpdate.PlaceWork))
                user.PlaceWork = userUpdate.PlaceWork;
            if (userUpdate.MonthlyIncome.HasValue)
                user.MonthlyIncome = userUpdate.MonthlyIncome.Value;
            if (userUpdate.CreditRating.HasValue)
                user.MonthlyIncome = userUpdate.CreditRating.Value;

            _context.SaveChanges();
        }

        public void DeleteUser(User user)
        {
            _context.Users.Remove(user);
            _context.SaveChanges();
        }
    }
}
