using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoneyTracking.Web.Models.IncomeModels;
using MoneyTracking.Web.Services.IncomeServ;
using System.Globalization;

namespace MoneyTracking.Web.Controllers
{
    [Authorize(Policy = "ApiKeyPolicy")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class IncomeController : ControllerBase
    {
        private readonly IIncomeService _incomeService;

        public IncomeController(IIncomeService incomeService)
        {
            _incomeService = incomeService;
        }

        // добавить доход
        [HttpPost]
        public async Task<IActionResult> AddIncome([FromBody] IncomeAdd incomeAdd)
        {
            if (ModelState.IsValid)
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
                return BadRequest(ModelState);
            }
        }

        // получить все доходы
        [HttpGet]
        public async Task<IActionResult> GetAllIncomes()
        {
            var listIncomes = await _incomeService.GetAllIncomes();
            var resultList = new List<string>();

            if (listIncomes.Any())
            {
                foreach (var income in listIncomes)
                {
                    resultList.Add($"Id : {income.Id}, Сумма : {income.Amount}, Описание : {income.Description}, ДатаСоздания : {income.IncomeDate}, Пользователь : ({income.User.Id} {income.User.Login}), Категория : {income.IncomeCategory.Name}");
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

            if (listIncome.Any())
            {
                foreach (var income in listIncome)
                {
                    resultList.Add($"Пользователь: {income.User.Login}, Сумма: {income.Amount}, Категория: {income.IncomeCategory.Name}, Описание: {income.Description}, Дата и время: {income.IncomeDate.ToString("g", CultureInfo.GetCultureInfo("ru-RU"))}");
                }

                return Ok(resultList);
            }
            else
            {
                return NotFound("У вас нет доходов!");
            }
        }

        // получить доход по id
        [HttpGet]
        public async Task<IActionResult> GetIncomeById(int id)
        {
            var income = await _incomeService.GetIncomeById(id);

            if (income != null)
            {
                return Ok($"Пользователь: {income.User.Login}, Сумма: {income.Amount}, Категория: {income.IncomeCategory.Name}, Описание: {income.Description}, Дата: {income.IncomeDate.ToString("g", CultureInfo.GetCultureInfo("ru-RU"))}");
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
            if (ModelState.IsValid)
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
                return BadRequest(ModelState);
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