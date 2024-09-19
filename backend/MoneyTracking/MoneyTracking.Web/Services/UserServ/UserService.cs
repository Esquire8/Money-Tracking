using MoneyTracking.Data.Entities;
using MoneyTracking.Data.UnitOfWork;
using MoneyTracking.Interfaces;
using MoneyTracking.Web.Models.UserModels;

namespace MoneyTracking.Web.Services.UserServ
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPasswordHasher _passwordHasher;

        public UserService(IUnitOfWork unitOfWork, IPasswordHasher passwordHasher)
        {
            _unitOfWork = unitOfWork;
            _passwordHasher = passwordHasher;
        }

        public async Task RegisterUser(UserAdd user)
        {
            var exUser = await GetUserByLogin(user.Login);

            if (exUser != null)
                throw new Exception("Такой пользователь уже зарегистрирован!");

            var passwordHash = _passwordHasher.Generate(user.Password);

            User newUser = new User
            {
                Login = user.Login,
                Email = user.Email,
                Password = passwordHash,
                RegistrationDate = DateTime.UtcNow
            };

            await _unitOfWork.Users.Add(newUser);
            await _unitOfWork.Save();
        }

        public async Task LoginUser(string login, string password)
        {
            var user = await _unitOfWork.Users.GetByLogin(login) ?? throw new Exception("Пользователь не найден!");

            bool resultLogin = _passwordHasher.Verify(password, user.Password);

            if (!resultLogin)
                throw new Exception("Ошибка аутентификации!");
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

        public async Task<User?> GetUserByLogin(string login)
        {
            return await _unitOfWork.Users.GetByLogin(login);
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