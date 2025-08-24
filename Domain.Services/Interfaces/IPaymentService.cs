using Domain.Services.DTOs.Payment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.Interfaces
{
    public interface IPaymentService
    {
        Task<int> CreatePayment(PaymentDto dto);
    }
}
