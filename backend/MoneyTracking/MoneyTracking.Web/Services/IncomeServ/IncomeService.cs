using MoneyTracking.Data.Entities;
using MoneyTracking.Data.UnitOfWork;
using MoneyTracking.Web.Models.IncomeModels;

namespace MoneyTracking.Web.Services.IncomeServ
{
    public class IncomeService : IIncomeService
    {
        private readonly IUnitOfWork _unitOfWork;

        public IncomeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task CreateIncome(IncomeAdd incomeAdd)
        {
            var user = await _unitOfWork.Users.GetById(incomeAdd.UserId) ?? throw new Exception();
            var incomeCategory = await _unitOfWork.IncomesCategeries.GetById(incomeAdd.IncomeCategoryId) ?? throw new Exception();

            var income = new Income()
            {
                Amount = incomeAdd.Amount,
                Description = incomeAdd.Description,
                IncomeDate = DateTime.UtcNow,
                User = user,
                IncomeCategory = incomeCategory
            };

            await _unitOfWork.Incomes.Add(income);
            await _unitOfWork.Save();
        }

        public async Task DeleteIncome(int incomeId)
        {
            var income = await GetIncomeById(incomeId) ?? throw new Exception();

            _unitOfWork.Incomes.Delete(income);
            await _unitOfWork.Save();
        }

        public async Task<IEnumerable<Income>> GetAllIncomes()
        {
            return await _unitOfWork.Incomes.GetAll();
        }

        public async Task<IEnumerable<Income>> GetAllIncomesByUser(int userId)
        {
            return await _unitOfWork.Incomes.GetAllByUser(userId);
        }

        public async Task<Income?> GetIncomeById(int id)
        {
            return await _unitOfWork.Incomes.GetById(id);
        }

        public async Task UpdateIncome(IncomeUpdate incomeUpdate)
        {
            var toUpdateIncome = await GetIncomeById(incomeUpdate.ToUpdateIncomeId) ?? throw new Exception();
            var updateIncomeCategory = await _unitOfWork.IncomesCategeries.GetById(incomeUpdate.UpdateIncomeCategoryId) ?? throw new Exception();

            toUpdateIncome.IncomeCategory = updateIncomeCategory;
            toUpdateIncome.Description = incomeUpdate.Description;
            toUpdateIncome.Amount = incomeUpdate.Amount;

            _unitOfWork.Incomes.Update(toUpdateIncome);
            await _unitOfWork.Save();
        }
    }
}