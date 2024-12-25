namespace pps.Models
{
    public class PaymentAccountResponse
    {
        public uint Id { get; set; }
        public decimal MoneyTotal { get; set; }
        public UserResponseShort User { get; set; }
    }
}
