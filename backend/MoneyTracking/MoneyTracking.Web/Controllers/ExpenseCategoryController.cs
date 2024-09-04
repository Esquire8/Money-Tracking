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
        public async Task<IActionResult> AddExpenseCategory([FromBody] ExpenseCategoryAdd request)
        {
            try
            {
                await _expenseCategoryService.CreateExpenseCategory(request);
                return Ok($"Категория Name : {request.CategoryName}, ParentId : {request.ParentId} добавлена");
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
        public async Task<IActionResult> UpdateExpenseCategory([FromBody] ExpenseCategoryUpdate request)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _expenseCategoryService.UpdateExpenseCategory(request);
                    return Ok($"Категория Id : {request.ExpenseCategoryId}, Name : {request.UpdateExpenseCategoryName}, ParentId : {request.UpdateParentId} обновлена");
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