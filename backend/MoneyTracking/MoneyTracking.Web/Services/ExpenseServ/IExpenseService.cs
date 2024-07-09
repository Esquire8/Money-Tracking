using MoneyTracking.Data.Entities;

namespace MoneyTracking.Web.Services.ExpenseServ
{
    public interface IExpenseService
    {
        Task<IEnumerable<Expense>> GetAllExpenses();

        Task<Expense?> GetExpenseById(int id);

        Task CreateExpense(Expense expense);

        Task DeleteExpense(Expense expense);

        Task UpdateExpense(Expense expense);
    }
}