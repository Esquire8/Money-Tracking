using MoneyTracking.Data.Entities;

namespace MoneyTracking.Data.Repositories.ExpenseRep
{
    public interface IExpenseRepository : IRepositoryBase<Expense>
    {
        Task<IEnumerable<Expense>> GetAllByUser(int userId);
    }
}