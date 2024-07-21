using Microsoft.EntityFrameworkCore;
using MoneyTracking.Data.Entities;

namespace MoneyTracking.Data.Repositories.ExpenseRep
{
    public class ExpenseRepository : IExpenseRepository
    {
        private readonly MoneyTrackingContext _context;

        public ExpenseRepository(MoneyTrackingContext context)
        {
            _context = context;
        }

        public async Task Add(Expense entity)
        {
            await _context.Expenses.AddAsync(entity);
        }

        public void Delete(Expense expense)
        {
            _context.Remove(expense);
        }

        public async Task<IEnumerable<Expense>> GetAll()
        {
            return await _context.Expenses
                .AsNoTracking()
                .Include(u => u.User)
                .Include(e => e.ExpenseCategory)
                .ToListAsync();
        }

        public async Task<IEnumerable<Expense>> GetAllByUser(int userId)
        {
            return await _context.Expenses
                .AsNoTracking()
                .Include(u => u.User)
                .Include(e => e.ExpenseCategory)
                .Where(u => u.User.Id == userId)
                .ToListAsync();
        }

        public async Task<Expense?> GetById(int id)
        {
            return await _context.Expenses
                .Include(u => u.User)
                .Include(e => e.ExpenseCategory)
                .Where(u => u.Id == id)
                .SingleOrDefaultAsync();
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public void Update(Expense entity)
        {
            _context.Expenses.Update(entity);
        }
    }
}