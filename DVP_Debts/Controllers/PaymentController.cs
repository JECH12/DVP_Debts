using Domain.Services.DTOs.Debt;
using Domain.Services.DTOs.Payment;
using Domain.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DVP_Debts.Controllers
{
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpPost]
        [Route("payments/CreatePayment")]
        public async Task<IActionResult> CreatePayment([FromBody] PaymentDto dto)
        {
            if (dto != null)
            {
                if (dto.Payment_amount > 0)
                {
                    int rta = await _paymentService.CreatePayment(dto);
                    if (rta > 0) return Ok("Pago realizado satisfactoriamente.");
                }
                else
                {
                    return BadRequest("Payment amount must be a positive number");
                }
            }
            return BadRequest();
        }
    }
}
