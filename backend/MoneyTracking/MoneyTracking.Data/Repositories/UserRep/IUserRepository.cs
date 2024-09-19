using MoneyTracking.Data.Entities;

namespace MoneyTracking.Data.Repositories.UserRep
{
    public interface IUserRepository : IRepositoryBase<User>
    {
        Task<User?> GetByLogin(string login);
    }
}