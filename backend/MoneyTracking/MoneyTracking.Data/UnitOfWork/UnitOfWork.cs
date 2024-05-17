using MoneyTracking.Data.Repositories;
using MoneyTracking.Data.Repositories.ExpenseCategoryRep;
using MoneyTracking.Data.Repositories.ExpenseRep;
using MoneyTracking.Data.Repositories.IncomeCategoryRep;
using MoneyTracking.Data.Repositories.IncomeRep;
using MoneyTracking.Data.Repositories.UserRep;

namespace MoneyTracking.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MoneyTrackingContext _context;

        public UnitOfWork(MoneyTrackingContext context)
        {
            _context = context;
            Users = new UserRepository(context);
            Incomes = new IncomeRepository(context);
            IncomesCategeries = new IncomeCategoryRepository(context);
            Expenses = new ExpenseRepository(context);
            ExpensesCategories = new ExpenseCategoryRepository(context);
        }

        public IUserRepository Users { get; private set; }

        public IIncomeRepository Incomes { get; private set; }

        public IIncomeCategeryRepository IncomesCategeries { get; private set; }

        public IExpenseRepository Expenses { get; private set; }

        public IExpenseCategoryRepository ExpensesCategories { get; private set; }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public async void Dispose()
        {
            await _context.DisposeAsync();
        }
    }
}