using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using pps.Models;
using pps.Services;
using System.IO;
using System.Xml.Linq;

namespace MyApiProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BankOfficeController : ControllerBase
    {
        private IBankOfficeService _bankOfficeService;

        public BankOfficeController(IBankOfficeService bankService)
        {
            _bankOfficeService = bankService;
        }

        [HttpGet("{id}")]
        public IActionResult GetBankOffice(uint id)
        {
            var bank = _bankOfficeService.GetBankOfficeById(id);
            if (bank == null)
                return NotFound();  
            return Ok(_bankOfficeService.MapToBankOfficeResponse(bank));
        }

        [HttpGet]
        public IActionResult GetBankOffice([FromQuery] string? name, [FromQuery] string? street, [FromQuery] bool? isWork, [FromQuery] bool? isPlaceBankAtm,
            [FromQuery] bool? isGiveCredit, [FromQuery] bool? isGiveMoney, [FromQuery] bool? isDepositMoney, [FromQuery] uint? bankId,
            [FromQuery] decimal? minMoneyTotal, [FromQuery] decimal? maxMoneyTotal, [FromQuery] decimal? minRentalCost, [FromQuery] decimal? maxRentalCost)
        {
            var banks = _bankOfficeService.GetBankOfficesByCriteria(name, street, isWork, isPlaceBankAtm, isGiveCredit, isGiveMoney,
                                                                    isDepositMoney, bankId, minMoneyTotal, maxMoneyTotal, minRentalCost, maxRentalCost);
            return Ok(banks.Select(b => _bankOfficeService.MapToBankOfficeResponse(b)).ToList());
        }

        [HttpPost]
        public IActionResult CreateBankOffice([FromBody] BankOfficeCreate bankOfficeCreate)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            BankOffice newBankOffice = _bankOfficeService.CreateBankOffice(bankOfficeCreate);
            if (newBankOffice == null)
                return BadRequest("Ошибка при создании банка");

            return CreatedAtAction(nameof(GetBankOffice), new { id = newBankOffice.Id }, _bankOfficeService.MapToBankOfficeResponse(newBankOffice));
        }

        // Обновить данные банка
        [HttpPut("{id}")]
        public IActionResult UpdateBank(uint id, [FromBody] BankOfficeUpdate bank)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existingBankOffice = _bankOfficeService.GetBankOfficeById(id);
            if (existingBankOffice == null)
                return NotFound();

            _bankOfficeService.UpdateBankOffice(existingBankOffice, bank);

            return Ok(_bankOfficeService.MapToBankOfficeResponse(existingBankOffice));
        }

        // Удалить банк
        [HttpDelete("{id}")]
        public IActionResult DeleteBank(uint id)
        {
            var existingBank = _bankOfficeService.GetBankOfficeById(id);
            if (existingBank == null)
                return NotFound();

            _bankOfficeService.DeleteBankOffice(existingBank);
            return NoContent(); // Возвращаем статус 204 (без содержимого, т.е. успешное удаление)
        }
    }
}
