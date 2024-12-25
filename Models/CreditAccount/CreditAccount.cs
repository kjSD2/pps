namespace pps.Models
{
    public class CreditAccount
    {
        public uint Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public uint MountTotal { get; set; }

        private decimal _moneyTotal { get; set; }
        public decimal MoneyTotal
        {
            get { return _moneyTotal; }
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException(nameof(value), "Всего денег не может быть отрицательным");
                _moneyTotal = value;
            }
        }

        private decimal _monthlyPayment { get; set; }
        public decimal MonthlyPayment
        {
            get { return _monthlyPayment; }
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException(nameof(value), "Ежемесячных платеж не может быть отрицательным");
                _monthlyPayment = value;
            }
        }

        private float _interestrate { get; set; }
        public float Interestrate
        {
            get { return _interestrate; }
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException(nameof(value), "Процентная ставка не может быть отрицательной");
                _interestrate = value;
            }
        }

        public PaymentAccount PaymentAccount { get; set; }
        public Employee Employee { get; set; }

        public CreditAccount() { }

        public CreditAccount(DateTime startDate, DateTime endDate, uint mountTotal, decimal moneyTotal,
            decimal monthlyPayment, float interestrate, PaymentAccount paymentAccount, Employee employee)
        {
            StartDate = startDate;
            EndDate = endDate;
            MountTotal = mountTotal;
            MoneyTotal = moneyTotal;
            MonthlyPayment = monthlyPayment;
            Interestrate = interestrate;
            PaymentAccount = paymentAccount;
            Employee = employee;
        }
    }
}
