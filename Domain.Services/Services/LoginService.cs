using Domain.Services.DTOs;
using Domain.Services.DTOs.User;
using Domain.Services.Interfaces;
using Infraestructure.Core.UnitOfWork;
using Infraestructure.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.Services
{
    public class LoginService: ILoginService
    {
        private ICipherSerializerService _cipherSerializerService;
        private IUnitOfWork _unitOfWork;


        public LoginService(ICipherSerializerService cipherSerializerService, IUnitOfWork unitOfWork)
        {
            _cipherSerializerService = cipherSerializerService;
            _unitOfWork = unitOfWork;
        }
        public async Task<UserDto?> Login(LoginDto dto) 
        { 
            var password = _cipherSerializerService.Encrypt(dto.Password);
            var email = _cipherSerializerService.Encrypt(dto.Email);

            User? user = await _unitOfWork.UserRepository.Find(x => x.Email == email && x.Password == password);
            if (user != null)
            {
                UserDto userDto = new()
                {
                    Id = user.Id,
                    Email = _cipherSerializerService.Decrypt(user.Email),
                    Name = user.Name
                };
                return userDto;
            }

            return null;
        }
    }
}
