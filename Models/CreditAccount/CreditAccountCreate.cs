namespace pps.Models
{
    public class CreditAccountCreate
    {
        public uint Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public uint MountTotal { get; set; }
        public decimal MoneyTotal { get; set; }
        public decimal MonthlyPayment {  get; set; }
        public float Interestrate {  get; set; }

        public uint PaymentAccountId { get; set; }
        public uint EmployeeId { get; set; }

    }
}
