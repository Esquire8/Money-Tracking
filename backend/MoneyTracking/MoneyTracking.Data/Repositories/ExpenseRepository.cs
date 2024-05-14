using Microsoft.EntityFrameworkCore;
using MoneyTracking.Data.Entities;

namespace MoneyTracking.Data.Repositories
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

        public async Task Delete(int id)
        {
            var expense = await _context.Expenses.FindAsync(id);
            if (expense != null)
            {
                _context.Remove(expense);
            }
        }

        public async Task<IEnumerable<Expense>> GetAll()
        {
            return await _context.Expenses.ToListAsync();
        }

        public async Task<Expense?> GetById(int id)
        {
            return await _context.Expenses.FindAsync(id);
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