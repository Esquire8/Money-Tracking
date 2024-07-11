using MoneyTracking.Data.Entities;
using MoneyTracking.Web.Models.ExpenseModels;

namespace MoneyTracking.Web.Services.ExpenseServ
{
    public interface IExpenseService
    {
        Task<IEnumerable<Expense>> GetAllExpenses();

        Task<IEnumerable<Expense>> GetAllExpensesByUser(int userId);

        Task<Expense?> GetExpenseById(int id);

        Task CreateExpense(ExpenseAdd newExpense);

        Task DeleteExpense(int expenseId);

        Task UpdateExpense(ExpenseUpdate expense);
    }
}