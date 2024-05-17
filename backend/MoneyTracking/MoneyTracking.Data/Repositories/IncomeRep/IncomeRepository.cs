using Microsoft.EntityFrameworkCore;
using MoneyTracking.Data.Entities;

namespace MoneyTracking.Data.Repositories.IncomeRep
{
    public class IncomeRepository : IIncomeRepository
    {
        private readonly MoneyTrackingContext _context;

        public IncomeRepository(MoneyTrackingContext context)
        {
            _context = context;
        }

        public async Task Delete(int id)
        {
            var income = await GetById(id);
            if (income != null)
            {
                _context.Incomes.Remove(income);
            }
        }

        public async Task<IEnumerable<Income>> GetAll()
        {
            return await _context.Incomes.ToListAsync();
        }

        public async Task<Income?> GetById(int id)
        {
            return await _context.Incomes.FindAsync(id);
        }

        public async Task Add(Income entity)
        {
            await _context.Incomes.AddAsync(entity);
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public void Update(Income entity)
        {
            _context.Update(entity);
        }
    }
}