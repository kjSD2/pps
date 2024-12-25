using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using pps.Models;
using pps.Services;

namespace MyApiProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BankController : ControllerBase
    {
        private IBankService _bankService;

        public BankController(IBankService bankService)
        {
            _bankService = bankService;
        }

        [HttpGet("{id}")]
        public IActionResult GetBank(uint id)
        {
            var bank = _bankService.GetBankById(id);
            if (bank == null)
                return NotFound();  // Если банк не найден
            return Ok(_bankService.MapToBankResponse(bank));
        }

        [HttpGet]
        public IActionResult GetBank([FromQuery] string? name, [FromQuery] byte? minRating, [FromQuery] byte? maxRating,
            [FromQuery] decimal? minMoneyTotal, [FromQuery] decimal? maxMoneyTotal, [FromQuery] float? minInterestrate, [FromQuery] float? maxInterestrate)
        {
            var banks = _bankService.GetBanksByCriteria(null, name, minRating, maxRating, minMoneyTotal, maxMoneyTotal, minInterestrate, maxInterestrate);
            return Ok(banks.Select(b => _bankService.MapToBankResponse(b)).ToList());
        }

        [HttpPost]
        public IActionResult CreateBank([FromBody] BankCreate bank)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            Bank newBank = _bankService.CreateBank(bank);
            if (newBank == null)
                return BadRequest("Ошибка при создании банка");

            return CreatedAtAction(nameof(GetBank), new { id = newBank.Id }, _bankService.MapToBankResponse(newBank)); 
        }

        // Обновить данные банка
        [HttpPut("{id}")]
        public IActionResult UpdateBank(uint id, [FromBody] BankUpdate bank)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            var existingBank = _bankService.GetBankById(id);
            if (existingBank == null)
                return NotFound();  

            _bankService.UpdateBank(existingBank, bank);
            
            return Ok(_bankService.MapToBankResponse(existingBank));
        }

        // Удалить банк
        [HttpDelete("{id}")]
        public IActionResult DeleteBank(uint id)
        {
            var existingBank = _bankService.GetBankById(id);
            if (existingBank == null)
                return NotFound();

            _bankService.DeleteBank(existingBank);
            return NoContent(); // Возвращаем статус 204 (без содержимого, т.е. успешное удаление)
        }
    }
}
