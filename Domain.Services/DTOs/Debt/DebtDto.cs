using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.DTOs.Debt
{
    public class DebtDto
    {
        public int Id { get; set; }
        public int DebtorId { get; set; }
        public string DebtorName { get; set; } = string.Empty;
        public int CreditorId { get; set; }
        public string CreditorName { get; set; } = string.Empty;
        public double Amount { get; set; }
        public string? Description { get; set; }
        public DateTime Creation_date { get; set; }
        public int StateId { get; set; }

        public string StateName { get; set; } = string.Empty;
    }
}
