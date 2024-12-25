using System.ComponentModel.DataAnnotations;

namespace pps.Models
{
    public class BankCreate
    {
        [Required(ErrorMessage = "Имя банка обязательно.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Название банка должно быть от 3 до 100 символов")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Рейтинг банка обязателен.")]
        [Range(1, 5, ErrorMessage = "Рейтинг должен быть от 1 до 5.")]
        public byte Rating { get; set; }

        [Required(ErrorMessage = "Сумма денег в банке обязательна.")]
        [Range(1000, double.MaxValue, ErrorMessage = "Сумма должна быть больше 1000.")]
        public decimal MoneyTotal { get; set; }

        [Required(ErrorMessage = "Процентная ставка обязательна.")]
        [Range(0.1, 30, ErrorMessage = "Процентная ставка должна быть в пределах от 0.1 до 30.")]
        public float Interestrate { get; set; }
    }
}
