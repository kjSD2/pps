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
    public class PaymentAccountController : ControllerBase
    {
        private IPaymentAccountService _paymentAccountService;

        public PaymentAccountController(IPaymentAccountService paymentAccountService)
        {
            _paymentAccountService = paymentAccountService;
        }

        [HttpGet("{id}")]
        public IActionResult GetBankOffice(uint id)
        {
            var bank = _paymentAccountService.GetPaymentAccountById(id);
            if (bank == null)
                return NotFound();
            return Ok(_paymentAccountService.MapToPaymentAccountResponse(bank));
        }

        [HttpGet]
        public IActionResult GetBankOffice([FromQuery] decimal? minMoneyTotal, [FromQuery] decimal? maxMoneyTotal, [FromQuery] uint? userId)
        {
            var banks = _paymentAccountService.GetPaymentAccountByCriteria(minMoneyTotal, maxMoneyTotal, userId);
            return Ok(banks.Select(b => _paymentAccountService.MapToPaymentAccountResponse(b)).ToList());
        }

        [HttpPost]
        public IActionResult CreateBankOffice([FromBody] PaymentAccountCreate paymentAccountCreate)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            PaymentAccount newUser = _paymentAccountService.CreatePaymentAccount(paymentAccountCreate);
            if (newUser == null)
                return BadRequest("Ошибка при создании банка");

            return CreatedAtAction(nameof(GetBankOffice), new { id = newUser.Id }, _paymentAccountService.MapToPaymentAccountResponse(newUser));
        }

        // Обновить данные банка
        [HttpPut("{id}")]
        public IActionResult UpdateBank(uint id, [FromBody] PaymentAccountUpdate paymentAccountUpdate)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existingBankOffice = _paymentAccountService.GetPaymentAccountById(id);
            if (existingBankOffice == null)
                return NotFound();

            _paymentAccountService.UpdatePaymentAccount(existingBankOffice, paymentAccountUpdate);

            return Ok(_paymentAccountService.MapToPaymentAccountResponse(existingBankOffice));
        }

        // Удалить банк
        [HttpDelete("{id}")]
        public IActionResult DeleteBank(uint id)
        {
            var existingBank = _paymentAccountService.GetPaymentAccountById(id);
            if (existingBank == null)
                return NotFound();

            _paymentAccountService.DeletePaymentAccount(existingBank);
            return NoContent(); // Возвращаем статус 204 (без содержимого, т.е. успешное удаление)
        }
    }
}
