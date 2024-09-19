using MoneyTracking.Data.Entities;
using MoneyTracking.Web.Models.UserModels;

namespace MoneyTracking.Web.Services.UserServ
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAllUsers();

        Task<User?> GetUserById(int id);

        Task<User?> GetUserByLogin(string login);

        Task LoginUser(string login, string password);

        Task RegisterUser(UserAdd user);

        Task DeleteUser(int userId);

        Task UpdateUser(UserUpdate userUpdate);
    }
}