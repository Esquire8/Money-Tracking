using Microsoft.AspNetCore.Mvc;
using MoneyTracking.Data;
using MoneyTracking.Data.Entities;
using MoneyTracking.Data.Repositories;

namespace MoneyTracking.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IRepositoryBase<User> _userRepository;
        private readonly IRepositoryBase<IncomeCategory> _incomeCategoryRepository;
        private readonly MoneyTrackingContext _context;

        public HomeController(ILogger<HomeController> logger,
            MoneyTrackingContext context,
            IRepositoryBase<User> userRepository,
            IRepositoryBase<IncomeCategory> incomeCategoryRepository
            )
        {
            _logger = logger;
            _context = context;
            _userRepository = userRepository;
            _incomeCategoryRepository = incomeCategoryRepository;
        }

        [HttpPost("insert-incomeCategory")]
        public async Task<IncomeCategory> InsertIncomeCategory(string name)
        {
            var newIncomeCategory = new IncomeCategory
            {
                Name = name
            };
            if (ModelState.IsValid)
            {
                await _incomeCategoryRepository.InsertAsync(newIncomeCategory);
                await _incomeCategoryRepository.SaveAsync();
            }
            return newIncomeCategory;
        }

        /*[HttpDelete("delete-incomeCategory")]
        public async Task<IncomeCategory> DeleteIncomeCategory(int id)
        {
            var incomeCategory = await _incomeCategoryRepository.GetByIdAsync(id);

            if (ModelState.IsValid && incomeCategory != null)
            {
                await _incomeCategoryRepository.DeleteAsync(id);
                //await _incomeCategoryRepository.SaveAsync();
            }

            return incomeCategory;
        }*/

        //Добавление пользователя
        [HttpPost("insert-user")]
        public async Task<int> InsertUser(string login, string password)
        {
            var newUser = new User
            {
                Login = login,
                Email = "test@test.ru",
                Password = password,
                RegistrationDate = DateTime.UtcNow
            };

            if (ModelState.IsValid)
            {
                await _userRepository.InsertAsync(newUser);
                //await _userRepository.SaveAsync();
            }
            //Id добавленной сущности
            return newUser.Id;
        }

        //Вывод всех пользователей
        [HttpGet("get-all-users")]
        public async Task<IEnumerable<User>> GetAllUsers()
        {
            var listUsers = await _userRepository.GetAllAsync();
            return listUsers;
        }

        //Поиск пользователя по id
        [HttpGet("get-user-by-id")]
        public async Task<User> GetUserById(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            return user;
        }

        //Редактирование пользователя
        [HttpPut("update-user")]
        public async Task<User> UpdateUser(int id, string newLogin)
        {
            var user = await _userRepository.GetByIdAsync(id);
            user.Login = newLogin;
            if (ModelState.IsValid)
            {
                await _userRepository.UpdateAsync(user);
                //await _userRepository.SaveAsync();
            }
            return user;
        }

        //Удаление пользователя
        [HttpDelete("delete-user")]
        public async Task<User> DeleteUser(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);

            if (ModelState.IsValid && user != null)
            {
                await _userRepository.DeleteAsync(id);
                //await _userRepository.SaveAsync();
            }
            return user;
        }
    }
}