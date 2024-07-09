using Microsoft.AspNetCore.Mvc;
using MoneyTracking.Web.Models.IncomeModels;
using MoneyTracking.Web.Services.IncomeCategoryServ;
using MoneyTracking.Web.Services.IncomeServ;
using MoneyTracking.Web.Services.UserServ;

namespace MoneyTracking.Web.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class IncomeController : ControllerBase
    {
        private readonly IIncomeService _incomeService;
        private readonly IUserService _userService;
        private readonly IIncomeCategoryService _incomeCategoryService;

        public IncomeController(IIncomeService incomeService, IUserService userService, IIncomeCategoryService incomeCategoryService)
        {
            _incomeService = incomeService;
            _userService = userService;
            _incomeCategoryService = incomeCategoryService;
        }

        // добавить доход
        [HttpPost]
        public async Task<IActionResult> AddIncome([FromBody] IncomeAdd incomeAdd)
        {
            if (incomeAdd != null)
            {
                try
                {
                    await _incomeService.CreateIncome(incomeAdd);

                    return Ok($"Доход добавлен {incomeAdd}");
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            else
            {
                return BadRequest("Введите данные о доходе!");
            }
        }

        // получить все доходы
        [HttpGet]
        public async Task<IActionResult> GetAllIncomes()
        {
            var listIncomes = await _incomeService.GetAllIncomes();
            var resultList = new List<string>();

            if (listIncomes != null)
            {
                foreach (var income in listIncomes)
                {
                    resultList.Add($" Id : {income.Id}, Сумма : {income.Amount}, Описание : {income.Description}, ДатаСоздания : {income.IncomeDate}, Пользователь : ({income.User.Id} {income.User.Login}), Категория : {income.IncomeCategory.Name}");
                }
                return Ok(resultList);
            }
            else
            {
                return NotFound("Доходы не найдены!");
            }
        }

        // получить все доходы пользователя
        [HttpGet]
        public async Task<IActionResult> GetUserIncomes(int userId)
        {
            var listIncome = await _incomeService.GetAllIncomesByUser(userId);

            var resultList = new List<string>();

            if (listIncome != null)
            {
                foreach (var income in listIncome)
                {
                    resultList.Add($"Пользователь: {income.User.Login}, Сумма: {income.Amount}, Категория: {income.IncomeCategory.Name}, Описание: {income.Description}, Дата и время: {income.IncomeDate.ToShortDateString()} в {income.IncomeDate.ToShortTimeString()}");
                }

                return Ok(resultList);
            }
            else
            {
                return NotFound("Доходов вообще ни у кого нет)");
            }
        }

        // получить доход по id
        [HttpGet]
        public async Task<IActionResult> GetIncomeById(int id)
        {
            var income = await _incomeService.GetIncomeById(id);

            if (income != null)
            {
                return Ok($"Пользователь: {income.User.Login}, Сумма: {income.Amount}, Категория: {income.IncomeCategory.Name}, Описание: {income.Description}, Дата: {income.IncomeDate.ToShortDateString()}");
            }
            else
            {
                return NotFound("Дохода не существует!");
            }
        }

        // обновить доход пользователя
        [HttpPost]
        public async Task<IActionResult> UpdateIncome([FromBody] IncomeUpdate incomeUpdate)
        {
            if (incomeUpdate != null)
            {
                try
                {
                    await _incomeService.UpdateIncome(incomeUpdate);

                    return Ok("Данные о доходе обновлены");
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            else
            {
                return BadRequest("Введите данные дохода для обновления");
            }
        }

        // удалить доход
        [HttpDelete]
        public async Task<IActionResult> DeleteIncome(int incomeId)
        {
            try
            {
                await _incomeService.DeleteIncome(incomeId);
                return Ok("Запись о доходе удалена");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}