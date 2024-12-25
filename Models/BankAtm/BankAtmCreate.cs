﻿namespace pps.Models
{
    public class BankAtmCreate
    {
        public uint Id { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public bool IsGiveMoney { get; set; }
        public bool IsDepositMoney { get; set; }
        public decimal MoneyTotal { get; set; }
        public decimal MaintenanceCost { get; set; }
        public uint EmployeeAccompanyingId { get; set; }
    }
}
