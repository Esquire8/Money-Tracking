using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoneyTracking.Web.Models.UserModels;
using MoneyTracking.Web.Services.UserServ;
using System.Security.Claims;

namespace MoneyTracking.Web.Controllers
{
    [Authorize(Policy = "ApiKeyPolicy")]
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

        // получить конкретного пользователя по id
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

        // получить конкретного пользователя по login
        [HttpGet]
        public async Task<IActionResult> GetUserByLogin(string login)
        {
            var user = await _userService.GetUserByLogin(login);

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
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> RegisterUser([FromBody] UserAdd user)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _userService.RegisterUser(user);

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

        // логин
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> LoginUser([FromBody] UserLogin user)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _userService.LoginUser(user.Login, user.Password);

                    var claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.Name, user.Login)
                    };

                    ClaimsIdentity claimId = new ClaimsIdentity(claims, "Cookies");

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimId));

                    return Ok($"Успешный вход в систему!");
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

        // выход пользователя из приложения
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Ok("Вы вышли из системы!");
        }

        // обновить данные пользователя
        [HttpPost]
        public async Task<IActionResult> UpdateUser([FromBody] UserUpdate userUpdate)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _userService.UpdateUser(userUpdate);

                    return Ok($"Пользователь {userUpdate.NewLogin} обновлен");
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