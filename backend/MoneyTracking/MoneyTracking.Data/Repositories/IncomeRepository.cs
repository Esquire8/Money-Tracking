using MoneyTracking.Data.Entities;

namespace MoneyTracking.Data.Repositories
{
    public class IncomeRepository : IRepositoryBase<Income>
    {
        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Income>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Income?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task InsertAsync(Income entity)
        {
            throw new NotImplementedException();
        }

        public Task SaveAsync()
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Income entity)
        {
            throw new NotImplementedException();
        }
    }
}