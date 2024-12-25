using System.Diagnostics;

namespace pps.Models
{
    public class BankOffice
    {
        public uint Id { get; set; }
        public string Name { get; set; }
        public string Street { get; set; }
        public bool IsWork { get; set; }
        public bool IsPlaceBankAtm { get; set; }

        public Bank Bank { get; set; }
        public ICollection<Employee> Employees { get; set; }

        public bool IsGiveCredit { get; set; }
        public bool IsGiveMoney { get; set; }
        public bool IsDepositMoney { get; set; }

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

        private decimal _rentalCost { get; set; }
        public decimal RentalCost
        {
            get { return _rentalCost; }
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException(nameof(value), "Стоимость аренды не может быть отрицательной");
                _rentalCost = value;
            }
        }

        public BankOffice() { }

        public BankOffice(string name, string street, bool isWork, bool isPlaceBankAtm, bool isGiveCredit,
            bool isGiveMoney, bool isDepositMoney, decimal moneyTotal,
            decimal rentalCost, Bank bank, ICollection<Employee> employees = null)
        {
            Name = name;
            Street = street;
            IsWork = isWork;
            IsPlaceBankAtm = isPlaceBankAtm;
            IsGiveCredit = isGiveCredit;
            IsGiveMoney = isGiveMoney;
            IsDepositMoney = isDepositMoney;
            MoneyTotal = moneyTotal;
            RentalCost = rentalCost;
            Bank = bank;

            Employees = employees ?? new List<Employee>();
        }

    }
}
