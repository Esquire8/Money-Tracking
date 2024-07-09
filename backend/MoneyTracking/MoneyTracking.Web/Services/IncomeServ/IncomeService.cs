using MoneyTracking.Data.Entities;
using MoneyTracking.Data.UnitOfWork;

namespace MoneyTracking.Web.Services.IncomeServ
{
    public class IncomeService : IIncomeService
    {
        private readonly IUnitOfWork _unitOfWork;

        public IncomeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task CreateIncome(Income income)
        {
            await _unitOfWork.Incomes.Add(income);
            await _unitOfWork.Save();
        }

        public async Task DeleteIncome(Income income)
        {
            _unitOfWork.Incomes.Delete(income);
            await _unitOfWork.Save();
        }

        public async Task<IEnumerable<Income>> GetAllIncomes()
        {
            return await _unitOfWork.Incomes.GetAll();
        }

        public async Task<IEnumerable<Income>> GetUserAllIncomes(int userId)
        {
            return await _unitOfWork.Incomes.GetUserAll(userId);
        }

        public async Task<Income?> GetIncomeById(int id)
        {
            return await _unitOfWork.Incomes.GetById(id);
        }

        public async Task UpdateIncome(Income income)
        {
            _unitOfWork.Incomes.Update(income);
            await _unitOfWork.Save();
        }
    }
}