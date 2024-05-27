using Microsoft.AspNetCore.Mvc;
using MoneyTracking.Data.Entities;
using MoneyTracking.Web.Services.IncomeCategoryServ;
using MoneyTracking.Web.Services.UserServ;

namespace MoneyTracking.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUserService _userService;
        private readonly IIncomeCategoryService _incomeCategoryService;

        public HomeController(ILogger<HomeController> logger,
            IUserService userService,
            IIncomeCategoryService incomeCategoryService
            )
        {
            _logger = logger;
            _userService = userService;
            _incomeCategoryService = incomeCategoryService;
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
                try
                {
                    await _userService.CreateUser(newUser);

                    return Ok($"Пользователь {newUser.Login} добавлен ");
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            else
            {
                return BadRequest("Введите данные пользователя");
            }
        }

        //Добавление категории
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
                try
                {
                    await _incomeCategoryService.CreateIncomeCategory(newIncomeCategory);

                    return Ok($"Категория {name} добавлена");
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
        }

        //Вывод всех пользователей
        [HttpGet("get-all-users")]
        public async Task<IActionResult> GetAllUsers()
        {
            var listUsers = await _userService.GetAllUsers();
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
            var user = await _userService.GetUserById(id);

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
            var user = await _userService.GetUserById(id);
            if (user != null)
            {
                try
                {
                    user.Login = newLogin;
                    _userService.UpdateUser(user);

                    return Ok("Пользователь обновлен");
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
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
            try
            {
                await _userService.DeleteUser(id);
                return Ok("Пользователь удален");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}