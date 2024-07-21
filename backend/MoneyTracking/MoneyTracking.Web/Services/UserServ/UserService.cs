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

        public async Task UpdateUser(UserUpdate userUpdate)
        {
            var user = await GetUserById(userUpdate.Id) ?? throw new Exception("Пользователь не найден!");

            user.Login = userUpdate.NewLogin;
            user.Email = userUpdate.NewEmail;
            user.Password = userUpdate.NewPassword;

            _unitOfWork.Users.Update(user);
            await _unitOfWork.Save();
        }
    }
}