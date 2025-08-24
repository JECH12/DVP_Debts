using AutoMapper;
using Domain.Services.DTOs.Payment;
using Domain.Services.Enum;
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
    public class PaymentService: IPaymentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PaymentService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<int> CreatePayment(PaymentDto dto)
        {
            Debt? debt = await _unitOfWork.DebtRepository.GetByIdAsync(dto.DebtId);
            if (debt != null && debt.StateId != (int)StateType.Settled && dto.Payment_amount <= debt.Amount) 
            {
                debt.Amount -= dto.Payment_amount;
                debt!.Creation_date = DateTime.SpecifyKind(debt.Creation_date, DateTimeKind.Utc);

                if (debt.Amount == 0) debt.StateId = (int)StateType.Settled;
                else debt.StateId = (int)StateType.Partialy_Paid;

                Payment entity = _mapper.Map<Payment>(dto);
                entity.Payment_date = DateTime.UtcNow;

                await _unitOfWork.PaymentRepository.AddAsync(entity);
                _unitOfWork.DebtRepository.Update(debt);

                return await _unitOfWork.SaveAsync();
            }

            return 0;
        }
    }
}
