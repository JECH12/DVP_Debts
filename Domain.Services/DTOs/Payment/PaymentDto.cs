using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.DTOs.Payment
{
    public class PaymentDto
    {
        public int DebtId { get; set; }
        public double Payment_amount { get; set; }
    }
}
