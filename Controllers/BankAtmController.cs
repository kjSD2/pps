using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using pps.Models;
using pps.Services;
using System.IO;
using System.Net.NetworkInformation;
using System.Xml.Linq;

namespace MyApiProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BankAtmController : ControllerBase
    {
        private IBankAtmService _bankAtmService;

        public BankAtmController(IBankAtmService bankAtmService)
        {
            _bankAtmService = bankAtmService;
        }

        [HttpGet("{id}")]
        public IActionResult GetBankAtm(uint id)
        {
            var bank = _bankAtmService.GetBankAtmById(id);
            if (bank == null)
                return NotFound();
            return Ok(_bankAtmService.MapToBankAtmResponse(bank));
        }

        [HttpGet]
        public IActionResult GetBankOffice([FromQuery] string? name, [FromQuery] string? status, [FromQuery] bool? isGiveMoney, [FromQuery] bool? isDepositMoney,
                                           [FromQuery] decimal? minMoneyTotal, [FromQuery] decimal? maxMoneyTotal, [FromQuery] decimal? minMaintenanceCost,
                                           [FromQuery] decimal? maxMaintenanceCost, [FromQuery] uint? EmployeeAccompanyingId)
        {
            var banks = _bankAtmService.GetBankAtmByCriteria(name, status, isGiveMoney, isDepositMoney, minMoneyTotal, maxMoneyTotal, 
                                                             minMaintenanceCost, maxMaintenanceCost, EmployeeAccompanyingId);
            return Ok(banks.Select(b => _bankAtmService.MapToBankAtmResponse(b)).ToList());
        }

        [HttpPost]
        public IActionResult CreateBankOffice([FromBody] BankAtmCreate bankAtmCreate)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            BankAtm newBankAtmCreate = _bankAtmService.CreateBankAtm(bankAtmCreate);
            if (newBankAtmCreate == null)
                return BadRequest("Ошибка при создании банка");

            return CreatedAtAction(nameof(GetBankOffice), new { id = newBankAtmCreate.Id }, _bankAtmService.MapToBankAtmResponse(newBankAtmCreate));
        }

        // Обновить данные банка
        [HttpPut("{id}")]
        public IActionResult UpdateBank(uint id, [FromBody] BankAtmUpdate bankAtm)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existingBankOffice = _bankAtmService.GetBankAtmById(id);
            if (existingBankOffice == null)
                return NotFound();

            _bankAtmService.UpdateBankAtm(existingBankOffice, bankAtm);

            return Ok(_bankAtmService.MapToBankAtmResponse(existingBankOffice));
        }

        // Удалить банк
        [HttpDelete("{id}")]
        public IActionResult DeleteBank(uint id)
        {
            var existingBank = _bankAtmService.GetBankAtmById(id);
            if (existingBank == null)
                return NotFound();

            _bankAtmService.DeleteBankAtm(existingBank);
            return NoContent(); // Возвращаем статус 204 (без содержимого, т.е. успешное удаление)
        }
    }
}
