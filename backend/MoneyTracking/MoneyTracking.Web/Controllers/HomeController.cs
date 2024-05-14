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
        private readonly IUserRepository _userRepository;
        private readonly IIncomeCategeryRepository _incomeCategoryRepository;
        private readonly MoneyTrackingContext _context;

        public HomeController(ILogger<HomeController> logger,
            MoneyTrackingContext context,
            IUserRepository userRepository,
            IIncomeCategeryRepository incomeCategoryRepository
            )
        {
            _logger = logger;
            _context = context;
            _userRepository = userRepository;
            _incomeCategoryRepository = incomeCategoryRepository;
        }

        [HttpPost("add-incomeCategory")]
        public async Task<IActionResult> AddIncomeCategory(string name)
        {
            var newIncomeCategory = new IncomeCategory
            {
                Name = name
            };

            if (string.IsNullOrWhiteSpace(name))
            {
                return BadRequest("Введите название категории");
            }
            else
            {
                await _incomeCategoryRepository.Add(newIncomeCategory);
                //await _incomeCategoryRepository.Save();
                return Ok($"Категория {name} добавлена");
            }
        }

        //Добавление пользователя
        [HttpPost("add-user")]
        public async Task<IActionResult> AddUser(string login, string password)
        {
            var newUser = new User
            {
                Login = login,
                Email = "test@test.ru",
                Password = password,
                RegistrationDate = DateTime.UtcNow
            };

            if (newUser != null)
            {
                await _userRepository.Add(newUser);
                //await _userRepository.SaveAsync();
                return Ok($"Пользователь {newUser.Login} добавлен ");
            }
            else
            {
                return BadRequest("Введите данные пользователя");
            }
        }

        //Вывод всех пользователей
        [HttpGet("get-all-users")]
        public async Task<IActionResult> GetAllUsers()
        {
            var listUsers = await _userRepository.GetAll();
            if (listUsers != null)
            {
                return Ok(listUsers);
            }
            else
            {
                return BadRequest("Нет пользователей");
            }
        }

        //Поиск пользователя по id
        [HttpGet("get-user-by-id")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _userRepository.GetById(id);

            if (user != null)
            {
                return Ok(user.Login);
            }
            else
            {
                return BadRequest("Пользователь не найден");
            }
        }

        //Редактирование пользователя
        [HttpPut("update-user")]
        public async Task<IActionResult> UpdateUser(int id, string newLogin)
        {
            var user = await _userRepository.GetById(id);
            if (user != null)
            {
                user.Login = newLogin;
                _userRepository.Update(user);
                //await _userRepository.SaveAsync();
                return Ok("Пользователь обновлен");
            }
            else
            {
                return BadRequest("Пользователь не найден");
            }
        }

        //Удаление пользователя
        [HttpDelete("delete-user")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _userRepository.GetById(id);

            if (user != null)
            {
                await _userRepository.Delete(user.Id);
                //await _userRepository.SaveAsync();
                return Ok("Пользователь удален");
            }
            else
            {
                return BadRequest("Пользователь не найден");
            }
        }
    }
}