using Microsoft.EntityFrameworkCore;
using MoneyTracking.Data.Entities;

namespace MoneyTracking.Data.Repositories
{
    public class IncomeCategoryRepository : IRepositoryBase<IncomeCategory>
    {
        private readonly MoneyTrackingContext _context;

        public IncomeCategoryRepository(MoneyTrackingContext context)
        {
            _context = context;
        }

        public async Task DeleteAsync(int id)
        {
            var incomeCategory = await _context.IncomeCategories.FindAsync(id);
            if (incomeCategory != null)
            {
                _context.IncomeCategories.Remove(incomeCategory);
            }
        }

        public async Task<IEnumerable<IncomeCategory>> GetAllAsync()
        {
            return await _context.IncomeCategories.ToListAsync();
        }

        public async Task<IncomeCategory?> GetByIdAsync(int id)
        {
            var incomeCategory = await _context.IncomeCategories.FirstOrDefaultAsync(i => i.Id == id);
            return incomeCategory;
        }

        public async Task InsertAsync(IncomeCategory incomeCategory)
        {
            await _context.IncomeCategories.AddAsync(incomeCategory);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(IncomeCategory incomeCategory)
        {
            _context.IncomeCategories.Update(incomeCategory);
        }
    }
}