using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoneyTracking.Web.Models.ExpenseModels;
using MoneyTracking.Web.Services.ExpenseServ;
using System.Globalization;

namespace MoneyTracking.Web.Controllers
{
    [Authorize(Policy = "ApiKeyPolicy")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ExpenseController : ControllerBase
    {
        private readonly IExpenseService _expenseService;

        public ExpenseController(IExpenseService expenseService)
        {
            _expenseService = expenseService;
        }

        // добавить расход
        [HttpPost]
        public async Task<IActionResult> AddExpense([FromBody] ExpenseAdd newExpense)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _expenseService.CreateExpense(newExpense);

                    return Ok($"Расход добавлен {newExpense}");
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

        // получить все расходы
        [HttpGet]
        public async Task<IActionResult> GetAllExpense()
        {
            var listExpenses = await _expenseService.GetAllExpenses();
            var resultList = new List<string>();

            if (listExpenses.Any())
            {
                foreach (var expense in listExpenses)
                {
                    resultList.Add($"Id : {expense.Id}, Сумма : {expense.Amount}, Описание : {expense.Description}, ДатаСоздания : {expense.ExpenseDate}, Пользователь : ({expense.User.Id} {expense.User.Login}), Категория : {expense.ExpenseCategory.Name}");
                }
                return Ok(resultList);
            }
            else
            {
                return NotFound("Расходы не найдены!");
            }
        }

        // получить все расходы пользователя
        [HttpGet]
        public async Task<IActionResult> GetUserExpenses(int userId)
        {
            var listExpenses = await _expenseService.GetAllExpensesByUser(userId);

            var resultList = new List<string>();

            if (listExpenses.Any())
            {
                foreach (var expense in listExpenses)
                {
                    resultList.Add($"Пользователь: {expense.User.Login}, Сумма: {expense.Amount}, Категория: {expense.ExpenseCategory.Name}, Описание: {expense.Description}, Дата и время: {expense.ExpenseDate.ToString("g", CultureInfo.GetCultureInfo("ru-RU"))}");
                }

                return Ok(resultList);
            }
            else
            {
                return NotFound("У вас нет расходов!");
            }
        }

        // получить расход по id
        [HttpGet]
        public async Task<IActionResult> GetExpenseById(int id)
        {
            var expense = await _expenseService.GetExpenseById(id);

            if (expense != null)
            {
                return Ok($"Пользователь: {expense.User.Login}, Сумма: {expense.Amount}, Категория: {expense.ExpenseCategory.Name}, Описание: {expense.Description}, Дата: {expense.ExpenseDate.ToString("g", CultureInfo.GetCultureInfo("ru-RU"))}");
            }
            else
            {
                return NotFound("Расхода не существует!");
            }
        }

        // обновить расходы пользователя
        [HttpPost]
        public async Task<IActionResult> UpdateExpense([FromBody] ExpenseUpdate request)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _expenseService.UpdateExpense(request);

                    return Ok("Данные о расходе обновлены");
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

        // удалить расход
        [HttpDelete]
        public async Task<IActionResult> DeleteExpense(int expenseId)
        {
            try
            {
                await _expenseService.DeleteExpense(expenseId);
                return Ok("Запись о расходе удалена");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}