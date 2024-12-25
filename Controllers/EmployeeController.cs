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
    public class EmployeeController : ControllerBase
    {
        private IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet("{id}")]
        public IActionResult GetBankOffice(uint id)
        {
            var bank = _employeeService.GetEmployeeById(id);
            if (bank == null)
                return NotFound();
            return Ok(_employeeService.MapToEmployeeResponse(bank));
        }

        [HttpGet]
        public IActionResult GetBankOffice([FromQuery] string? fullName, [FromQuery] DateTime? minBirthday, [FromQuery] DateTime? maxBirthday, [FromQuery] string? jobTitle,
                                           [FromQuery] bool? isRemoteWork, [FromQuery] bool? isGiveCredit, [FromQuery] decimal? minSalary, [FromQuery] decimal? maxSalary)
        {
            var banks = _employeeService.GetEmployeeByCriteria(fullName, minBirthday, maxBirthday, jobTitle, isRemoteWork, isGiveCredit, minSalary, maxSalary);
            return Ok(banks.Select(b => _employeeService.MapToEmployeeResponse(b)).ToList());
        }

        [HttpPost]
        public IActionResult CreateBankOffice([FromBody] EmployeeCreate employeeCreate)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            Employee newBankOffice = _employeeService.CreateEmployee(employeeCreate);
            if (newBankOffice == null)
                return BadRequest("Ошибка при создании банка");

            return CreatedAtAction(nameof(GetBankOffice), new { id = newBankOffice.Id }, _employeeService.MapToEmployeeResponse(newBankOffice));
        }

        // Обновить данные банка
        [HttpPut("{id}")]
        public IActionResult UpdateBank(uint id, [FromBody] EmployeeUpdate bank)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existingBankOffice = _employeeService.GetEmployeeById(id);
            if (existingBankOffice == null)
                return NotFound();

            _employeeService.UpdateEmployee(existingBankOffice, bank);

            return Ok(_employeeService.MapToEmployeeResponse(existingBankOffice));
        }

        // Удалить банк
        [HttpDelete("{id}")]
        public IActionResult DeleteBank(uint id)
        {
            var existingBank = _employeeService.GetEmployeeById(id);
            if (existingBank == null)
                return NotFound();

            _employeeService.DeleteEmployee(existingBank);
            return NoContent(); // Возвращаем статус 204 (без содержимого, т.е. успешное удаление)
        }
    }
}
