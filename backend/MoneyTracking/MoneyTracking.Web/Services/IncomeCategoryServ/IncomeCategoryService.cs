using MoneyTracking.Data.Entities;
using MoneyTracking.Data.UnitOfWork;
using MoneyTracking.Web.Models.IncomeCategoryModels;

namespace MoneyTracking.Web.Services.IncomeCategoryServ
{
    public class IncomeCategoryService : IIncomeCategoryService
    {
        private readonly IUnitOfWork _unitOfWork;

        public IncomeCategoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task CreateIncomeCategory(string categoryName)
        {
            var incomeCategory = new IncomeCategory { Name = categoryName };

            await _unitOfWork.IncomesCategeries.Add(incomeCategory);
            await _unitOfWork.Save();
        }

        public async Task DeleteIncomeCategory(int categoryId)
        {
            var incomeCategory = await GetIncomeCategoryById(categoryId) ?? throw new Exception();

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

        public async Task UpdateIncomeCategory(IncomeCategoryUpdate incomeCategory)
        {
            var updateIncomeCategory = await GetIncomeCategoryById(incomeCategory.IncomeCategoryId) ?? throw new Exception();

            updateIncomeCategory.Name = incomeCategory.UpdateIncomeCategoryName;

            _unitOfWork.IncomesCategeries.Update(updateIncomeCategory);
            await _unitOfWork.Save();
        }
    }
}