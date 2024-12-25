namespace pps.Models
{
    public class PaymentAccount
    {
        public uint Id { get; set; }

        private decimal _moneyTotal { get; set; }
        public decimal MoneyTotal
        {
            get { return _moneyTotal; }
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException(nameof(value), "Количество денег не может быть отрицательным");
                _moneyTotal = value;
            }
        }

        public User User { get; set; }
        public ICollection<CreditAccount> CreditAccounts { get; set; }

        public PaymentAccount() { }

        public PaymentAccount(decimal moneyTotal, User user, ICollection<CreditAccount> creditAccounts = null)
        {
            MoneyTotal = moneyTotal;
            User = user;

            CreditAccounts = creditAccounts ?? new List<CreditAccount>();
        }
    }
}
