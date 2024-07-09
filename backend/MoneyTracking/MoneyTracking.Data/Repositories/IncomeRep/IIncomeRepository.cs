using MoneyTracking.Data.Entities;

namespace MoneyTracking.Data.Repositories.IncomeRep
{
    public interface IIncomeRepository : IRepositoryBase<Income>
    {
        Task<IEnumerable<Income>> GetUserAll(int userId);
    }
}