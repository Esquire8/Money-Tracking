using MoneyTracking.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyTracking.Data.Repositories
{
    public class ExpenseRepository : IRepositoryBase<Expense>
    {
        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Expense>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Expense?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task InsertAsync(Expense entity)
        {
            throw new NotImplementedException();
        }

        public Task SaveAsync()
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Expense entity)
        {
            throw new NotImplementedException();
        }
    }
}