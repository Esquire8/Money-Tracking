using MoneyTracking.Data.Entities;
using MoneyTracking.Data.UnitOfWork;
using MoneyTracking.Web.Models.UserModels;

namespace MoneyTracking.Web.Services.UserServ
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task CreateUser(UserAdd user)
        {
            User newUser = new User
            {
                Login = user.Login,
                Email = user.Email,
                Password = user.Password,
                RegistrationDate = DateTime.UtcNow
            };

            await _unitOfWork.Users.Add(newUser);
            await _unitOfWork.Save();
        }

        public async Task DeleteUser(int userId)
        {
            var user = await GetUserById(userId) ?? throw new Exception("Пользователь не найден!");

            _unitOfWork.Users.Delete(user);
            await _unitOfWork.Save();
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            return await _unitOfWork.Users.GetAll();
        }

        public async Task<User?> GetUserById(int id)
        {
            return await _unitOfWork.Users.GetById(id);
        }

        public async Task UpdateUser(UserUpdate user)
        {
            var updatedUser = await GetUserById(user.Id) ?? throw new Exception("Пользователь не найден!");

            updatedUser.Login = user.NewLogin;
            updatedUser.Email = user.NewEmail;
            updatedUser.Password = user.NewPassword;

            _unitOfWork.Users.Update(updatedUser);
            await _unitOfWork.Save();
        }
    }
}