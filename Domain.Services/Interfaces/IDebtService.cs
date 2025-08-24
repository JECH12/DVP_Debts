using Domain.Services.DTOs.Debt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.Interfaces
{
    public interface IDebtService
    {
        Task<List<DebtDto>> GetDebtsList(GetDebtsDto dto);

        Task<int> RegisterDebt(RegisterDebtDto dto);

        Task<DebtDto> GetDebt(int debtId);
        Task<int> EditDebt(EditDebtDto dto);

        Task<int> DeleteDebt(int DebtId);
    }
}
