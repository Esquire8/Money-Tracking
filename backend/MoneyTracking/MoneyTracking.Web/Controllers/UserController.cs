using Microsoft.AspNetCore.Mvc;
using MoneyTracking.Data.Entities;
using MoneyTracking.Web.Models.UserModels;
using MoneyTracking.Web.Services.UserServ;

namespace MoneyTracking.Web.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        // список всех пользователей
        [HttpGet]
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

        // найти конкретного пользователя
        [HttpGet]
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

        // добавить пользователя
        [HttpPost]
        public async Task<IActionResult> AddUser([FromBody] UserAdd user)
        {
            var newUser = new User
            {
                Login = user.Login,
                Email = user.Email,
                Password = user.Password,
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

        // обновить данные пользователя
        [HttpPost]
        public async Task<IActionResult> UpdateUser([FromBody] UserUpdate userUpdate)
        {
            var user = await _userService.GetUserById(userUpdate.Id);

            if (user != null)
            {
                try
                {
                    user.Login = userUpdate.NewLogin;
                    user.Email = userUpdate.NewEmail;
                    user.Password = userUpdate.NewPassword;

                    await _userService.UpdateUser(user);

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

        // удалить пользователя
        [HttpDelete]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _userService.GetUserById(id);

            if (user != null)
            {
                try
                {
                    await _userService.DeleteUser(user);
                    return Ok("Пользователь удален");
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
    }
}