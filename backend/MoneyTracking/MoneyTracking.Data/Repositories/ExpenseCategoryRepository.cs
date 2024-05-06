using MoneyTracking.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyTracking.Data.Repositories
{
    public class ExpenseCategoryRepository : IRepositoryBase<ExpenseCategory>
    {
        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ExpenseCategory>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ExpenseCategory?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task InsertAsync(ExpenseCategory entity)
        {
            throw new NotImplementedException();
        }

        public Task SaveAsync()
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(ExpenseCategory entity)
        {
            throw new NotImplementedException();
        }
    }
}