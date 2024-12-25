namespace pps.Models
{
    public class BankOfficeResponse
    {
        public uint Id { get; set; }
        public string Name { get; set; }
        public string Street { get; set; }
        public bool IsWork { get; set; }
        public bool IsPlaceBankAtm { get; set; }
        public bool IsGiveCredit { get; set; }
        public bool IsGiveMoney { get; set; }
        public bool IsDepositMoney { get; set; }
        public BankResponseShort Bank { get; set; }
        //public uint BankId { get; set; }
        public decimal MoneyTotal { get; set; }
        public decimal RentalCost { get; set; }
        public int EmployeeTotal { get; set; }
        public ICollection<EmployeeResponseShort> Employees { get; set; }
    }
}
