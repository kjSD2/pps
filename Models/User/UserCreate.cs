namespace pps.Models
{
    public class UserCreate
    {
        public string FullName { get; set; }
        public DateTime Birthday { get; set; }
        public string PlaceWork { get; set; }
        public decimal MonthlyIncome { get; set; }
        public byte CreditRating { get; set; }
    }
}
