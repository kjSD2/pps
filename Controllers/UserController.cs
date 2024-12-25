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
    public class UserController : ControllerBase
    {
        private IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("{id}")]
        public IActionResult GetBankOffice(uint id)
        {
            var bank = _userService.GetUserById(id);
            if (bank == null)
                return NotFound();
            return Ok(_userService.MapToUserResponse(bank));
        }

        [HttpGet]
        public IActionResult GetBankOffice([FromQuery] string? fullName, [FromQuery] DateTime? minBirthday, [FromQuery] DateTime? maxBirthday,
                                           [FromQuery] string? placeWork, [FromQuery] decimal? minMonthlyIncome,
                                           [FromQuery] decimal? maxMonthlyIncome, [FromQuery] byte? minCreditRating, [FromQuery] byte? maxCreditRating)
        {
            var banks = _userService.GetUserByCriteria(fullName, minBirthday, maxBirthday, placeWork, minMonthlyIncome,
                                                       maxMonthlyIncome, minCreditRating, maxCreditRating);
            return Ok(banks.Select(b => _userService.MapToUserResponse(b)).ToList());
        }

        [HttpPost]
        public IActionResult CreateBankOffice([FromBody] UserCreate userCreate)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            User newUser = _userService.CreateUser(userCreate);
            if (newUser == null)
                return BadRequest("Ошибка при создании банка");

            return CreatedAtAction(nameof(GetBankOffice), new { id = newUser.Id }, _userService.MapToUserResponse(newUser));
        }

        // Обновить данные банка
        [HttpPut("{id}")]
        public IActionResult UpdateBank(uint id, [FromBody] UserUpdate user)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existingBankOffice = _userService.GetUserById(id);
            if (existingBankOffice == null)
                return NotFound();

            _userService.UpdateUser(existingBankOffice, user);

            return Ok(_userService.MapToUserResponse(existingBankOffice));
        }

        // Удалить банк
        [HttpDelete("{id}")]
        public IActionResult DeleteBank(uint id)
        {
            var existingBank = _userService.GetUserById(id);
            if (existingBank == null)
                return NotFound();

            _userService.DeleteUser(existingBank);
            return NoContent(); // Возвращаем статус 204 (без содержимого, т.е. успешное удаление)
        }
    }
}
