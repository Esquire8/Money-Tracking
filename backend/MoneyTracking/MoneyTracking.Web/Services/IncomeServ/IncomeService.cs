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

        public async Task DeleteIncome(int id)
        {
            await _unitOfWork.Incomes.Delete(id);
            await _unitOfWork.Save();
        }

        public async Task<IEnumerable<Income>> GetAllIncomes()
        {
            return await _unitOfWork.Incomes.GetAll();
        }

        public async Task<Income?> GetIncomeById(int id)
        {
            return await _unitOfWork.Incomes.GetById(id);
        }

        public void UpdateIncome(Income income)
        {
            _unitOfWork.Incomes.Update(income);
            _unitOfWork.Save();
        }
    }
}