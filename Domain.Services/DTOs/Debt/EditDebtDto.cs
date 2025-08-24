using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.DTOs.Debt
{
    public class EditDebtDto
    {
        public int Id { get; set; }
        public string Description { get; set; } = string.Empty;
    }
}
