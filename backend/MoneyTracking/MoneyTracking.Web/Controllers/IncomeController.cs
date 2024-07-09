using Microsoft.AspNetCore.Mvc;
using MoneyTracking.Data.Entities;
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
            var user = await _userService.GetUserById(incomeAdd.UserId);
            var incomeCategory = await _incomeCategoryService.GetIncomeCategoryById(incomeAdd.IncomeCategoryId);

            if (user != null && incomeCategory != null)
            {
                var income = new Income()
                {
                    Amount = incomeAdd.Amount,
                    Description = incomeAdd.Description,
                    IncomeDate = DateTime.UtcNow,
                    User = user,
                    IncomeCategory = incomeCategory
                };

                if (incomeAdd != null)
                {
                    try
                    {
                        await _incomeService.CreateIncome(income);

                        return Ok();
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
            else
            {
                return BadRequest("Пользователь не существует!");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllIncomes()
        {
            var listIncomes = await _incomeService.GetAllIncomes();
            var resultList = new List<string>();

            if (listIncomes != null)
            {
                foreach (var income in listIncomes)
                {
                    resultList.Add($" Id : {income.Id}, Сумма : {income.Amount}, Описание : {income.Description}, ДатаСоздания : {income.IncomeDate}, Пользователь : {income.User.Login}, Категория : {income.IncomeCategory.Name}");
                }
                return Ok(resultList);
            }
            else
            {
                return BadRequest("Доходов нет!");
            }
        }

        // список доходов пользователя
        [HttpGet]
        public async Task<IActionResult> GetUserIncomes(int userId)
        {
            var listIncome = await _incomeService.GetUserAllIncomes(userId);

            var resultList = new List<string>();

            if (listIncome != null)
            {
                foreach (var income in listIncome)
                {
                    if (income.Description == null)
                    {
                        income.Description = "Описание отсутствует";
                    }

                    resultList.Add($"Пользователь: {income.User.Login}, Сумма: {income.Amount}, Категория: {income.IncomeCategory.Name}, Описание: {income.Description}, Дата: {income.IncomeDate.ToShortDateString()}");
                }

                return Ok(resultList);
            }
            else
            {
                return BadRequest("Доходов вообще ни у кого нет)");
            }
        }

        // найти доход по id
        [HttpGet]
        public async Task<IActionResult> GetIncomeById(int id)
        {
            var income = await _incomeService.GetIncomeById(id);

            if (income != null)
            {
                if (income.Description == null)
                {
                    income.Description = "Описание отсутствует";
                }

                return Ok($"Пользователь: {income.User.Login}, Сумма: {income.Amount}, Категория: {income.IncomeCategory.Name}, Описание: {income.Description}, Дата: {income.IncomeDate.ToShortDateString()}");
            }
            else
            {
                return BadRequest("Дохода не существует!");
            }
        }

        // обновить доход пользователя
        [HttpPost]
        public async Task<IActionResult> UpdateIncome([FromBody] IncomeUpdate incomeUpdate)
        {
            var toUpdateIncome = await _incomeService.GetIncomeById(incomeUpdate.ToUpdateIncomeId);
            var updateIncomeCategory = await _incomeCategoryService.GetIncomeCategoryById(incomeUpdate.UpdateIncomeCategoryId);

            if (toUpdateIncome != null && updateIncomeCategory != null)
            {
                toUpdateIncome.IncomeCategory = updateIncomeCategory;

                // пробовал передать null в теле POST, но выдает 400 ошибку, добавил AllowEmptyInputInBodyModelBinding в program.cs в AddControllers и это тоже не помогло, пока так оставлю
                if (toUpdateIncome.Description == string.Empty)
                {
                    toUpdateIncome.Description = null;
                }
                else { toUpdateIncome.Description = incomeUpdate.Description; }

                toUpdateIncome.Amount = incomeUpdate.Amount;

                try
                {
                    await _incomeService.UpdateIncome(toUpdateIncome);

                    return Ok("Все хорошо");
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            else
            {
                return BadRequest("Доход не найден!");
            }
        }

        // удалить доход
        [HttpDelete]
        public async Task<IActionResult> DeleteIncome(int id)
        {
            var income = await _incomeService.GetIncomeById(id);

            if (income != null)
            {
                try
                {
                    await _incomeService.DeleteIncome(income);
                    return Ok("Запись о доходе удалена");
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            else
            {
                return BadRequest("Дохода не существует");
            }
        }
    }
}