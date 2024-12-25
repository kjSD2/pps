namespace pps.Models
{
    public class BankOfficeCreate
    {
        public string Name { get; set; }
        public string Street { get; set; }
        public bool IsWork { get; set; }
        public bool IsPlaceBankAtm { get; set; }
        public bool IsGiveCredit { get; set; }
        public bool IsGiveMoney { get; set; }
        public bool IsDepositMoney { get; set; }
        public uint BankId { get; set; }
        public decimal MoneyTotal { get; set; }
        public decimal RentalCost { get; set; }
    }
}
