using MoneyTracking.Data.Entities;
using MoneyTracking.Web.Models.IncomeCategoryModels;

namespace MoneyTracking.Web.Services.IncomeCategoryServ
{
    public interface IIncomeCategoryService
    {
        Task<IEnumerable<IncomeCategory>> GetAllIncomeCategories();

        Task<IncomeCategory?> GetIncomeCategoryById(int id);

        Task CreateIncomeCategory(string categoryName);

        Task DeleteIncomeCategory(int categoryId);

        Task UpdateIncomeCategory(IncomeCategoryUpdate incomeCategory);
    }
}