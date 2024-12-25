namespace pps.Models
{
    public class EmployeeResponse
    {
        public uint Id { get; set; }
        public string FullName { get; set; }
        public DateTime Birthday { get; set; }
        public string JobTitle { get; set; }
        public bool IsRemoteWork { get; set; }
        public bool IsGiveCredit { get; set; }
        public decimal Salary { get; set; }
        public BankOfficeResponseShort BankOffice { get; set; }
        public ICollection<BankAtmResponseShort> Atms { get; set; }
        public ICollection<CreditAccountResponseShort> CreditAccounts { get; set; }
    }
}
