using Domain.Services.Interfaces;
using Infraestructure.Entity;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace DVP_Debts.Controllers
{
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUser _userService;

        public UserController(IUser userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [Route("users/Get")]
        public async Task<IActionResult> GetListUsers()
        {
            List<User> users = await _userService.GetListUsers();
            return Ok(users);
        }
    }
}
