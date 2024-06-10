using MoneyTracking.Data.Entities;

namespace MoneyTracking.Web.Services.ExpenseCategoryServ
{
    public interface IExpenseCategoryService
    {
        Task<IEnumerable<ExpenseCategory>> GetAllExpenseCategories();

        Task<ExpenseCategory?> GetExpenseCategoryById(int id);

        Task CreateExpenseCategory(ExpenseCategory expenseCategory);

        Task DeleteExpenseCategory(int id);

        void UpdateExpenseCategory(ExpenseCategory expenseCategory);
    }
}