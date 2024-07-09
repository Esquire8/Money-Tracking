using MoneyTracking.Data.Entities;

namespace MoneyTracking.Web.Services.UserServ
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAllUsers();

        Task<User?> GetUserById(int id);

        Task CreateUser(User user);

        Task DeleteUser(User user);

        Task UpdateUser(User user);
    }
}