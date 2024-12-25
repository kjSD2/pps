namespace pps.Models
{
    public class Employee
    {
        public uint Id { get; set; }
        public string FullName { get; set; }
        public DateTime Birthday { get; set; }
        public string JobTitle { get; set; }
        public bool IsRemoteWork { get; set; }
        public bool IsGiveCredit { get; set; }

        private decimal _salary { get; set; }
        public decimal Salary
        {
            get { return _salary; }
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException(nameof(value), "Зарплата не может быть отрицательной");
                _salary = value;
            }
        }

        public BankOffice BankOffice { get; set; }
        public ICollection<BankAtm> BankAtms { get; set; }
        public ICollection<CreditAccount> CreditAccounts { get; set; }

        public Employee() { }

        public Employee(string fullName, DateTime birthday, string jobTitle, bool isRemoteWork, bool isGiveCredit,
            decimal salary, BankOffice bankOffice, ICollection<BankAtm> bankAtms = null, ICollection<CreditAccount> creditAccounts = null)
        {
            FullName = fullName;
            Birthday = birthday;
            JobTitle = jobTitle;
            IsRemoteWork = isRemoteWork;
            IsGiveCredit = isGiveCredit;
            Salary = salary;

            BankOffice = bankOffice;
            BankAtms = bankAtms ?? new List<BankAtm>();
            CreditAccounts = creditAccounts ?? new List<CreditAccount>();
        }
    }
}
