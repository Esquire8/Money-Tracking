using Microsoft.EntityFrameworkCore;
using MoneyTracking.Data.Entities;

namespace MoneyTracking.Data.Repositories.ExpenseCategoryRep
{
    public class ExpenseCategoryRepository : IExpenseCategoryRepository
    {
        private readonly MoneyTrackingContext _context;

        public ExpenseCategoryRepository(MoneyTrackingContext context)
        {
            _context = context;
        }

        public async Task Add(ExpenseCategory entity)
        {
            await _context.ExpenseCategories.AddAsync(entity);
        }

        public void Delete(ExpenseCategory entity)
        {
            _context.Remove(entity);
        }

        public async Task<IEnumerable<ExpenseCategory>> GetAll()
        {
            return await _context.ExpenseCategories.ToListAsync();
        }

        public async Task<ExpenseCategory?> GetById(int id)
        {
            return await _context.ExpenseCategories.FindAsync(id);
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public void Update(ExpenseCategory entity)
        {
            _context.ExpenseCategories.Update(entity);
        }
    }
}