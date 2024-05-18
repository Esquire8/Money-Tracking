using Microsoft.EntityFrameworkCore;
using MoneyTracking.Data.Entities;

namespace MoneyTracking.Data.Repositories.IncomeCategoryRep
{
    public class IncomeCategoryRepository : IIncomeCategeryRepository
    {
        private readonly MoneyTrackingContext _context;

        public IncomeCategoryRepository(MoneyTrackingContext context)
        {
            _context = context;
        }

        public async Task Delete(int id)
        {
            var incomeCategory = await GetById(id);
            if (incomeCategory != null)
            {
                _context.IncomeCategories.Remove(incomeCategory);
            }
        }

        public async Task<IEnumerable<IncomeCategory>> GetAll()
        {
            return await _context.IncomeCategories.ToListAsync();
        }

        public async Task<IncomeCategory?> GetById(int id)
        {
            return await _context.IncomeCategories.FindAsync(id);
        }

        public async Task Add(IncomeCategory incomeCategory)
        {
            await _context.IncomeCategories.AddAsync(incomeCategory);
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public void Update(IncomeCategory incomeCategory)
        {
            _context.IncomeCategories.Update(incomeCategory);
        }
    }
}