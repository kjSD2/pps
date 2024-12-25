using System.Formats.Asn1;

namespace pps.Models
{
    public class BankAtm
    {
        public uint Id { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
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

        private decimal _maintenanceCost { get; set; }
        public decimal MaintenanceCost
        {
            get { return _maintenanceCost; }
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException(nameof(value), "Стоимость обслуживания не может быть отрицательной");
                _maintenanceCost = value;
            }
        }

        public Employee EmployeeAccompanying { get; set; }

        public BankAtm() { }

        // Конструктор с параметрами для инициализации всех свойств
        public BankAtm(string name, string status, bool isGiveMoney, bool isDepositMoney,
            decimal moneyTotal, decimal maintenanceCost, Employee employeeAccompanying)
        {
            Name = name;
            Status = status;
            IsGiveMoney = isGiveMoney;
            IsDepositMoney = isDepositMoney;
            MoneyTotal = moneyTotal;
            MaintenanceCost = maintenanceCost;
            EmployeeAccompanying = employeeAccompanying;
        }
    }
}
