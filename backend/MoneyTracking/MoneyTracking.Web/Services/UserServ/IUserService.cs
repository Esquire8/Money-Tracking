using MoneyTracking.Data.Entities;
using MoneyTracking.Web.Models.UserModels;

namespace MoneyTracking.Web.Services.UserServ
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAllUsers();

        Task<User?> GetUserById(int id);

        Task CreateUser(UserAdd user);

        Task DeleteUser(int userId);

        Task UpdateUser(UserUpdate userUpdate);
    }
}