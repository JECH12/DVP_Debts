using Domain.Services.DTOs.Debt;
using Domain.Services.Interfaces;
using Domain.Services.Services;
using Infraestructure.Entity;
using Microsoft.AspNetCore.Mvc;

namespace DVP_Debts.Controllers
{
    [ApiController]
    public class DebtController : ControllerBase
    {
        private readonly IDebtService _debtService;

        public DebtController(IDebtService debtService)
        {
            _debtService = debtService;
        }

        [HttpPost]
        [Route("debt/GetDebt")]
        public async Task<IActionResult> GetDebt([FromBody] int debtId)
        {
            DebtDto debt = await _debtService.GetDebt(debtId);
            return Ok(debt);
        }

        [HttpPost]
        [Route("debt/GetDebts")]
        public async Task<IActionResult> GetListDebts([FromBody] GetDebtsDto dto)
        {
            List<DebtDto> debts = await _debtService.GetDebtsList(dto);
            return Ok(debts);
        }

        [HttpPost]
        [Route("debt/RegisterDebt")]
        public async Task<IActionResult> RegisterDebt([FromBody] RegisterDebtDto dto)
        {
            if (dto != null) 
            {
                if (dto.Amount > 0)
                {
                    int rta = await _debtService.RegisterDebt(dto);
                    if (rta == 1) return Ok(dto);
                }
                else
                {
                    return BadRequest("Amount must be a positive number");
                }
            }
            return BadRequest();
        }

        [HttpPost]
        [Route("debt/EditDebt")]
        public async Task<IActionResult> EditDebt(EditDebtDto dto)
        {
            if (dto != null)
            {
                int rta = await _debtService.EditDebt(dto);
                if (rta == 1) return Ok("Se edito la deuda correctamente");
                else return BadRequest("Una deuda pagada no puede editarse");
            }
            return BadRequest();
        }

        [HttpPost]
        [Route("debt/DeleteDebt")]
        public async Task<IActionResult> DeleteDebt(int DebtId)
        {

            int rta = await _debtService.DeleteDebt(DebtId);
            if (rta == 1) return Ok("Se elimino la deuda correctamente");
            return NoContent();
        }
    }
}
