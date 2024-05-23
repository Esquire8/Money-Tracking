using Microsoft.AspNetCore.Mvc;
using MoneyTracking.Data.Entities;
using MoneyTracking.Web.Services.UserServ;

namespace MoneyTracking.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUserService _userService;

        public HomeController(ILogger<HomeController> logger,
            IUserService userService
            )
        {
            _logger = logger;
            _userService = userService;
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
                await _userService.CreateUser(newUser);
                return Ok($"Пользователь {newUser.Login} добавлен ");
            }
            else
            {
                return BadRequest("Введите данные пользователя");
            }
        }

        //Добавление категории
        /*[HttpPost("add-incomeCategory")]
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
                await _unitOfWork.IncomesCategeries.Add(newIncomeCategory);
                await _unitOfWork.Save();
                return Ok($"Категория {name} добавлена");
            }
        }*/

        //Вывод всех пользователей
        /*[HttpGet("get-all-users")]
        public async Task<IActionResult> GetAllUsers()
        {
            var listUsers = await _unitOfWork.Users.GetAll();
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
            var user = await _unitOfWork.Users.GetById(id);

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
            var user = await _unitOfWork.Users.GetById(id);
            if (user != null)
            {
                user.Login = newLogin;
                _unitOfWork.Users.Update(user);
                await _unitOfWork.Save();
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
            var user = await _unitOfWork.Users.GetById(id);

            if (user != null)
            {
                await _unitOfWork.Users.Delete(user.Id);
                await _unitOfWork.Save();
                return Ok("Пользователь удален");
            }
            else
            {
                return BadRequest("Пользователь не найден");
            }
        }*/
    }
}