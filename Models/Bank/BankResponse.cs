namespace pps.Models
{
    public class BankResponse
    {
        public uint Id { get; set; }
        public string Name { get; set; }
        public byte Rating { get; set; }
        public decimal MoneyTotal { get; set; }
        public float InterestRate { get; set; }
        public int BankOfficeTotal { get; set; }
        public ICollection<BankOfficeResponseShort> BankOffices { get; set; }
    }
}
