using Domain.Services.DTOs.User;
using Domain.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.Interfaces
{
    public interface ILoginService
    {
        Task<UserDto?> Login(LoginDto dto);
    }
}
