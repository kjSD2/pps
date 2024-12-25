using System.ComponentModel.DataAnnotations;

namespace pps.Models
{
    public class BankUpdate
    {
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Название банка должно быть от 3 до 100 символов.")]
        public string? Name { get; set; }

        [Range(1, 5, ErrorMessage = "Рейтинг должен быть от 1 до 5.")]
        public byte? Rating { get; set; }

        [Range(1000, double.MaxValue, ErrorMessage = "Сумма должна быть больше 1000.")]
        public decimal? MoneyTotal { get; set; }

        [Range(0.1, 30, ErrorMessage = "Процентная ставка должна быть в пределах от 0.1 до 30.")]
        public float? Interestrate { get; set; }
    }
}
