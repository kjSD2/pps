using System.ComponentModel.DataAnnotations;

namespace pps.Models
{
    public class Bank
    {
        public uint Id { get; set; }
        public string Name { get; set; }

        public ICollection<BankOffice> BankOffices { get; set; }

        private byte _rating { get; set; }
        public byte Rating
        {
            get { return _rating; }
            set
            {
                if (value > 5)
                    throw new ArgumentOutOfRangeException(nameof(value), "Рейтинг должен быть от 0 до 5");
                _rating = value;
            }
        }

        private decimal _moneyTotal { get; set; }
        public decimal MoneyTotal
        {
            get { return _moneyTotal; }
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException(nameof(value), "В банке не может быть денег меньше нуля");
                _moneyTotal = value;
            }
        }

        private float _interestrate { get; set; }
        public float Interestrate
        {
            get { return _interestrate; }
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException(nameof(value), "Процентная ставка должна быть больше нуля");
                _interestrate = value;
            }
        }

        public Bank() { }


        public Bank(string name, byte rating, decimal moneyTotal, float interestrate, ICollection<BankOffice> bankOffices = null)
        {
            Name = name;
            Rating = rating;
            MoneyTotal = moneyTotal;
            Interestrate = interestrate;

            BankOffices = bankOffices ?? new List<BankOffice>();
        }
    }
}
