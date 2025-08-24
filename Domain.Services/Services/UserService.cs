using AutoMapper;
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
    public class UserService :IUser
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICipherSerializerService _cipherSerializerService;

        public UserService(IUnitOfWork unitOfWork, IMapper mapper, ICipherSerializerService cipherSerializerService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _cipherSerializerService = cipherSerializerService;
        }
        public async Task<List<User>> GetListUsers()
        {
            IEnumerable<User> userList = await _unitOfWork.UserRepository.GetAllAsync();

            return userList.ToList();    
        }

        public async Task<int> RegisterUser(UserDto user)
        {
            user.Email = _cipherSerializerService.Encrypt(user.Email);
            user.Password = _cipherSerializerService.Encrypt(user.Password);

            User entity = _mapper.Map<User>(user);

            entity.Register_date = DateTime.UtcNow;
            await _unitOfWork.UserRepository.AddAsync(entity);
            return await _unitOfWork.SaveAsync();
        }
    }
}
