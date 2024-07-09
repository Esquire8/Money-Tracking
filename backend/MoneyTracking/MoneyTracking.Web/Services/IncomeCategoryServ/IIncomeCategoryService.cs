using MoneyTracking.Data.Entities;

namespace MoneyTracking.Web.Services.IncomeCategoryServ
{
    public interface IIncomeCategoryService
    {
        Task<IEnumerable<IncomeCategory>> GetAllIncomeCategories();

        Task<IncomeCategory?> GetIncomeCategoryById(int id);

        Task CreateIncomeCategory(IncomeCategory incomeCategory);

        Task DeleteIncomeCategory(IncomeCategory incomeCategory);

        Task UpdateIncomeCategory(IncomeCategory incomeCategory);
    }
}