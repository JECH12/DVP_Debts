using Domain.Services.DTOs;
using Domain.Services.DTOs.Payment;
using Domain.Services.DTOs.User;
using Domain.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DVP_Debts.Controllers
{
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _loginService;

        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }
        [HttpPost]
        [Route("login/login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            if (dto != null)
            {
                UserDto? user = await _loginService.Login(dto);
                if (user != null) return Ok(user);
                else return BadRequest("El usuario no existe");
            }
            return BadRequest();
        }
    }
}
