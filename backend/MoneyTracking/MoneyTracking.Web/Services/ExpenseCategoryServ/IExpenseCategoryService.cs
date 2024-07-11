using MoneyTracking.Data.Entities;
using MoneyTracking.Web.Models.ExpenseCategoryModels;

namespace MoneyTracking.Web.Services.ExpenseCategoryServ
{
    public interface IExpenseCategoryService
    {
        Task<IEnumerable<ExpenseCategory>> GetAllExpenseCategories();

        Task<ExpenseCategory?> GetExpenseCategoryById(int id);

        Task CreateExpenseCategory(ExpenseCategoryAdd newExpenseCategory);

        Task DeleteExpenseCategory(int categoryId);

        Task UpdateExpenseCategory(ExpenseCategoryUpdate expenseCategory);
    }
}