using MoneyTracking.Data.Repositories.ExpenseCategoryRep;
using MoneyTracking.Data.Repositories.ExpenseRep;
using MoneyTracking.Data.Repositories.IncomeCategoryRep;
using MoneyTracking.Data.Repositories.IncomeRep;
using MoneyTracking.Data.Repositories.UserRep;

namespace MoneyTracking.Data.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository Users { get; }
        IIncomeRepository Incomes { get; }
        IIncomeCategeryRepository IncomesCategeries { get; }
        IExpenseRepository Expenses { get; }
        IExpenseCategoryRepository ExpensesCategories { get; }

        Task Save();
    }
}