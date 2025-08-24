using Domain.Services.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.DTOs.Debt
{
    public class GetDebtsDto
    {
        public int UserId { get; set; }
        public DebtType Type { get; set; }

        public StateType State { get; set; }
    }
}
