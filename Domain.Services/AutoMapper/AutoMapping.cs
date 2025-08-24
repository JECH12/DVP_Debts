using AutoMapper;
using Domain.Services.DTOs.Debt;
using Domain.Services.DTOs.Payment;
using Domain.Services.DTOs.User;
using Infraestructure.Entity;

namespace Domain.Services.AutoMapper
{
    public class AutoMapping : Profile
    {
        public AutoMapping() 
        {
            CreateMap<UserDto, User>();
            CreateMap<DebtDto, Debt>();
            CreateMap<RegisterDebtDto, Debt>();
            CreateMap<PaymentDto, Payment>();

        }
    }
}
