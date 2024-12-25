using System.ComponentModel.DataAnnotations;

namespace pps.Models
{
    public class User
    {
        public uint Id { get; set; }
        public string FullName { get; set; }
        public DateTime Birthday { get; set; }
        public string PlaceWork { get; set; }

        private decimal _monthlyIncome { get; set; }
        public decimal MonthlyIncome
        {
            get { return _monthlyIncome; }
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException(nameof(value), "Зарплата не может быть отрицательной");
                _monthlyIncome = value;
            }
        }

        private byte _creditRating { get; set; }
        public byte CreditRating
        {
            get { return _creditRating; }
            set
            {
                if (value > 5)
                    throw new ArgumentOutOfRangeException(nameof(value), "Рейтинг может быть от 0 до 5");
                _creditRating = value;
            }
        }

        public ICollection<PaymentAccount> PaymentAccounts;

        public User() { }

        public User(string fullName, DateTime birthday, string placeWork, decimal monthlyIncome, byte creditRating,
            ICollection<PaymentAccount> paymentAccounts = null)
        {
            FullName = fullName;
            Birthday = birthday;
            PlaceWork = placeWork;
            MonthlyIncome = monthlyIncome;
            CreditRating = creditRating;

            PaymentAccounts = paymentAccounts ?? new List<PaymentAccount>();
        }
    }
}
