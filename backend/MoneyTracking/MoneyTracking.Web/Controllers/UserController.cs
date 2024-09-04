using Microsoft.AspNetCore.Mvc;
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

        // получить список всех пользователей
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var listUsers = await _userService.GetAllUsers();

            if (listUsers.Any())
            {
                return Ok(listUsers);
            }
            else
            {
                return NotFound("Нет пользователей!");
            }
        }

        // получить конкретного пользователя
        [HttpGet]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _userService.GetUserById(id);

            if (user != null)
            {
                return Ok($"Пользователь существует : {user.Login}");
            }
            else
            {
                return NotFound("Пользователь не найден!");
            }
        }

        // добавить пользователя
        [HttpPost]
        public async Task<IActionResult> AddUser([FromBody] UserAdd user)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _userService.CreateUser(user);

                    return Ok($"Пользователь {user.Login} добавлен");
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        // обновить данные пользователя
        [HttpPost]
        public async Task<IActionResult> UpdateUser([FromBody] UserUpdate user)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _userService.UpdateUser(user);

                    return Ok($"Пользователь {user.NewLogin} обновлен");
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        // удалить пользователя
        [HttpDelete]
        public async Task<IActionResult> DeleteUser(int userId)
        {
            try
            {
                await _userService.DeleteUser(userId);
                return Ok("Пользователь удален");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}