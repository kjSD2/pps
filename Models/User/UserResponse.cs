namespace pps.Models
{
    public class UserResponse
    {
        public uint Id {  get; set; }
        public string FullName {  get; set; }
        public DateTime Birthday {  get; set; }
        public string PlaceWork {  get; set; }
        public decimal MonthlyIncome {  get; set; }
        public byte CreditRating {  get; set; }
        public ICollection<PaymentAccountResponseShort> PaymentAccounts {  get; set; }
    }
}
