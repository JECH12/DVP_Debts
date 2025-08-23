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
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<List<User>> GetListUsers()
        {
            IEnumerable<User> userList = await _unitOfWork.UserRepository.GetAllAsync();

            return userList.ToList();    
        }
    }
}
