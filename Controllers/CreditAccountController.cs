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
    public class CreditAccountController : ControllerBase
    {
        private ICreditAccountService _creditAccountService;

        public CreditAccountController(ICreditAccountService creditAccountService)
        {
            _creditAccountService = creditAccountService;
        }

        [HttpGet("{id}")]
        public IActionResult GetBankOffice(uint id)
        {
            var bank = _creditAccountService.GetCreditAccountById(id);
            if (bank == null)
                return NotFound();
            return Ok(_creditAccountService.MapToCreditAccountResponse(bank));
        }

        [HttpGet]
        public IActionResult GetBankOffice([FromQuery] DateTime? minStartDate, [FromQuery] DateTime? maxStartDate, [FromQuery] DateTime? minEndDate,
                                           [FromQuery] DateTime? maxEndDate, [FromQuery] uint? minMountTotal, [FromQuery] uint? maxMountTotal, 
                                           [FromQuery] decimal? minMoneyTotal, [FromQuery] decimal? maxMoneyTotal, [FromQuery] decimal? minMonthlyPayment,
                                           [FromQuery] decimal? maxMonthlyPayment, [FromQuery] float? minInterestrate, [FromQuery] float? maxInterestrate)
        {
            var banks = _creditAccountService.GetCreditAccountByCriteria(minStartDate, maxStartDate, minEndDate, maxEndDate, minMountTotal, maxMountTotal,
                                                                         minMoneyTotal, maxMoneyTotal, minMonthlyPayment, maxMonthlyPayment, minInterestrate, 
                                                                         maxInterestrate);
            return Ok(banks.Select(b => _creditAccountService.MapToCreditAccountResponse(b)).ToList());
        }

        [HttpPost]
        public IActionResult CreateBankOffice([FromBody] CreditAccountCreate creditAccountCreate)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            CreditAccount newCreditAccount = _creditAccountService.CreateCreditAccount(creditAccountCreate);
            if (newCreditAccount == null)
                return BadRequest("Ошибка при создании банка");

            return CreatedAtAction(nameof(GetBankOffice), new { id = newCreditAccount.Id }, _creditAccountService.MapToCreditAccountResponse(newCreditAccount));
        }

        // Обновить данные банка
        [HttpPut("{id}")]
        public IActionResult UpdateBank(uint id, [FromBody] CreditAccountUpdate creditAccount)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existingBankOffice = _creditAccountService.GetCreditAccountById(id);
            if (existingBankOffice == null)
                return NotFound();

            _creditAccountService.UpdateCreditAccount(existingBankOffice, creditAccount);

            return Ok(_creditAccountService.MapToCreditAccountResponse(existingBankOffice));
        }

        // Удалить банк
        [HttpDelete("{id}")]
        public IActionResult DeleteBank(uint id)
        {
            var existingBank = _creditAccountService.GetCreditAccountById(id);
            if (existingBank == null)
                return NotFound();

            _creditAccountService.DeleteCreditAccount(existingBank);
            return NoContent(); // Возвращаем статус 204 (без содержимого, т.е. успешное удаление)
        }
    }
}
