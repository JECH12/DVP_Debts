using AutoMapper;
using Domain.Services.DTOs.Debt;
using Domain.Services.Enum;
using Domain.Services.Interfaces;
using Infraestructure.Core.UnitOfWork;
using Infraestructure.Entity;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Domain.Services.Services
{
    public class DebtService: IDebtService
    {
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;

        public DebtService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<DebtDto> GetDebt(int debtId)
        {
            Debt? debt = await _unitOfWork.DebtRepository.GetByIdAsync(debtId);
            if (debt != null) 
            {
                User? debtor = await _unitOfWork.UserRepository.GetByIdAsync(debt.DebtorId);
                User? creditor = await _unitOfWork.UserRepository.GetByIdAsync(debt.CreditorId);

                DebtDto debtDto = new DebtDto()
                {
                    Id = debt.Id,
                    DebtorId = debtor!.Id,
                    DebtorName = debtor.Name,
                    CreditorId = creditor!.Id,
                    CreditorName = creditor.Name,
                    Amount = debt.Amount,
                    Description = debt.Description,
                    Creation_date = debt.Creation_date,
                    StateId = debt.StateId,
                };

                return debtDto;
            }         
            
            return new DebtDto();          
        }

        public async Task<List<DebtDto>> GetDebtsList(GetDebtsDto dto)
        {
            List<DebtDto> debtsEntities = new();
            var users = await _unitOfWork.UserRepository.GetAllAsync();

            List<Debt>? debts = new List<Debt>();

            if (dto.Type == DebtType.Debtor)
            {
                debts = (await _unitOfWork.DebtRepository.FindAll(x => x.DebtorId == dto.UserId))!.ToList();
            }
            else if(dto.Type == DebtType.Creditor)
            {
                debts = (await _unitOfWork.DebtRepository.FindAll(x => x.CreditorId == dto.UserId))!.ToList();
            }

            if (debts != null && debts.Any())
            {
                debtsEntities = (
                from debt in debts
                join debtor in users on debt.DebtorId equals debtor.Id
                join creditor in users on debt.CreditorId equals creditor.Id
                    select new DebtDto
                    {
                        Id = debt.Id,
                        DebtorId = debtor.Id,
                        DebtorName = debtor.Name,
                        CreditorId = creditor.Id,
                        CreditorName = creditor.Name,
                        Amount = debt.Amount,
                        Description = debt.Description,
                        Creation_date = debt.Creation_date,
                        StateId = debt.StateId
                    }
                ).ToList();
            }

            return debtsEntities;
        }

        public async Task<int> RegisterDebt(RegisterDebtDto dto)
        {
            Debt entity = _mapper.Map<Debt>(dto);

            entity.Creation_date = DateTime.UtcNow;
            entity.StateId = (int)StateType.Pendiente;

            await _unitOfWork.DebtRepository.AddAsync(entity);
            return await _unitOfWork.SaveAsync();
        }

        public async Task<int> EditDebt(EditDebtDto dto)
        {
            Debt? entity = await _unitOfWork.DebtRepository.GetByIdAsync(dto.Id);
            entity!.Creation_date = DateTime.SpecifyKind(entity.Creation_date, DateTimeKind.Utc);
            entity!.Description = dto.Description;
            _unitOfWork.DebtRepository.Update(entity);

            return await _unitOfWork.SaveAsync();
        }

        public async Task<int> DeleteDebt(int DebtId)
        {
            Debt? entity = await _unitOfWork.DebtRepository.GetByIdAsync(DebtId);
            if (entity != null) 
            {
                _unitOfWork.DebtRepository.Delete(entity); 
                return await _unitOfWork.SaveAsync();
            } 

            return 0;

        }
    }
}
