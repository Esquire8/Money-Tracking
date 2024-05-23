using MoneyTracking.Data.Entities;

namespace MoneyTracking.Web.Services.ExpenseServ
{
    public interface IExpenseService
    {
        Task<IEnumerable<Expense>> GetAllExpenses();

        Task<Expense?> GetExpenseById(int id);

        Task CreateExpense(Expense expense);

        Task DeleteExpense(int id);

        void UpdateExpense(Expense expense);
    }
}