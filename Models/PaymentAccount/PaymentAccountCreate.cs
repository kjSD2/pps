namespace pps.Models
{
    public class PaymentAccountCreate
    {
        public uint Id { get; set; }
        public decimal MoneyTotal { get; set; } 
        public uint UserId { get; set; }
    }
}
