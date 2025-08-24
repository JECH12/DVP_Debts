using Domain.Services.DTOs.User;
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
        [Route("users/RegisterUser")]
        public async Task<IActionResult> RegisterUser([FromBody] UserDto user)
        {
            if (user != null && (!string.IsNullOrEmpty(user.Email) && !string.IsNullOrEmpty(user.Password))) 
            { 
                int rta = await _userService.RegisterUser(user);

                if (rta == 1) 
                {
                    return Ok(user);
                }
                else
                {
                    return BadRequest();
                }
                
            }
            else
            {
                return BadRequest();
            }     
        }

        [HttpPost]
        [Route("users/GetUsers")]
        public async Task<IActionResult> GetListUsers()
        {
            List<User> users = await _userService.GetListUsers();
            return Ok(users);
        }
    }
}
