using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoneyTracking.Web.Models.ExpenseCategoryModels;
using MoneyTracking.Web.Services.ExpenseCategoryServ;

namespace MoneyTracking.Web.Controllers
{
    [Authorize(Policy = "ApiKeyPolicy")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ExpenseCategoryController : ControllerBase
    {
        private readonly IExpenseCategoryService _expenseCategoryService;

        public ExpenseCategoryController(IExpenseCategoryService expenseCategoryService)
        {
            _expenseCategoryService = expenseCategoryService;
        }

        // добавить категорию расхода
        [HttpPost]
        public async Task<IActionResult> AddExpenseCategory([FromBody] ExpenseCategoryAdd expenseCategory)
        {
            try
            {
                await _expenseCategoryService.CreateExpenseCategory(expenseCategory);
                return Ok($"Категория Name : {expenseCategory.CategoryName}, ParentId : {expenseCategory.ParentId} добавлена");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // получить все категории расходов
        [HttpGet]
        public async Task<IActionResult> GetAllExpenseCategories()
        {
            var listExpenseCategory = await _expenseCategoryService.GetAllExpenseCategories();

            if (listExpenseCategory.Any())
            {
                return Ok(listExpenseCategory);
            }
            else
            {
                return NotFound("Не найдено ни одной категории расхода!");
            }
        }

        // обновить категорию расхода
        [HttpPost]
        public async Task<IActionResult> UpdateExpenseCategory([FromBody] ExpenseCategoryUpdate expenseCategory)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _expenseCategoryService.UpdateExpenseCategory(expenseCategory);
                    return Ok($"Категория Id : {expenseCategory.ExpenseCategoryId}, Name : {expenseCategory.UpdateExpenseCategoryName}, ParentId : {expenseCategory.UpdateParentId} обновлена");
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

        // удалить категорию дохода
        [HttpDelete]
        public async Task<IActionResult> DeleteExpenseCategory(int expenseCategoryId)
        {
            try
            {
                await _expenseCategoryService.DeleteExpenseCategory(expenseCategoryId);
                return Ok("Категория удалена");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}