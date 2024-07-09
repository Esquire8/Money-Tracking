using MoneyTracking.Data.Entities;
using MoneyTracking.Data.UnitOfWork;

namespace MoneyTracking.Web.Services.IncomeCategoryServ
{
    public class IncomeCategoryService : IIncomeCategoryService
    {
        private readonly IUnitOfWork _unitOfWork;

        public IncomeCategoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task CreateIncomeCategory(IncomeCategory incomeCategory)
        {
            await _unitOfWork.IncomesCategeries.Add(incomeCategory);
            await _unitOfWork.Save();
        }

        public async Task DeleteIncomeCategory(IncomeCategory incomeCategory)
        {
            _unitOfWork.IncomesCategeries.Delete(incomeCategory);
            await _unitOfWork.Save();
        }

        public async Task<IEnumerable<IncomeCategory>> GetAllIncomeCategories()
        {
            return await _unitOfWork.IncomesCategeries.GetAll();
        }

        public async Task<IncomeCategory?> GetIncomeCategoryById(int id)
        {
            return await _unitOfWork.IncomesCategeries.GetById(id);
        }

        public async Task UpdateIncomeCategory(IncomeCategory incomeCategory)
        {
            _unitOfWork.IncomesCategeries.Update(incomeCategory);
            await _unitOfWork.Save();
        }
    }
}