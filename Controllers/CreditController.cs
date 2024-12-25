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
    public class CreditServiceController : ControllerBase
    {
        private ICreditService _creditService;

        public CreditServiceController(ICreditService creditService)
        {
            _creditService = creditService;
        }

        [HttpPost]
        public IActionResult CreateBankOffice([FromQuery] decimal requestedAmount, [FromQuery] uint userId )
        {
            /*if (!ModelState.IsValid)
                return BadRequest(ModelState);
            CreditAccount newCreditAccount = _creditAccountService.CreateCreditAccount(creditAccountCreate);
            if (newCreditAccount == null)
                return BadRequest("Ошибка при создании банка");*/
            return _creditService.getCredit(requestedAmount, userId);

            Ok();//xCreatedAtAction(nameof(GetBankOffice), new { id = newCreditAccount.Id }, _creditAccountService.MapToCreditAccountResponse(newCreditAccount));
        }
    }
}
