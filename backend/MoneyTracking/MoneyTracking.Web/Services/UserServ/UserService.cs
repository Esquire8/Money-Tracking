using MoneyTracking.Data.Entities;
using MoneyTracking.Data.UnitOfWork;

namespace MoneyTracking.Web.Services.UserServ
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task CreateUser(User user)
        {
            await _unitOfWork.Users.Add(user);
            await _unitOfWork.Save();
        }

        public async Task DeleteUser(int id)
        {
            await _unitOfWork.Users.Delete(id);
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

        public void UpdateUser(User user)
        {
            _unitOfWork.Users.Update(user);
            _unitOfWork.Save();
        }
    }
}