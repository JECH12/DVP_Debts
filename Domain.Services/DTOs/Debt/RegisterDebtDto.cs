using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.DTOs.Debt
{
    public class RegisterDebtDto
    {
        public int DebtorId { get; set; }
        public int CreditorId { get; set; }
        public double Amount { get; set; }
        public string? Description { get; set; }
    }
}
