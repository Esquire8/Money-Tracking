using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoneyTracking.Web.Models.IncomeCategoryModels;
using MoneyTracking.Web.Services.IncomeCategoryServ;

namespace MoneyTracking.Web.Controllers
{
    [Authorize(Policy = "ApiKeyPolicy")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class IncomeCategoryController : ControllerBase
    {
        private readonly IIncomeCategoryService _incomeCategoryService;

        public IncomeCategoryController(IIncomeCategoryService incomeCategoryService)
        {
            _incomeCategoryService = incomeCategoryService;
        }

        // добавить категорию дохода
        [HttpPost]
        public async Task<IActionResult> AddIncomeCategory(string categoryName)
        {
            try
            {
                await _incomeCategoryService.CreateIncomeCategory(categoryName);
                return Ok($"Категория {categoryName} добавлена");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // получить все категории доходов
        [HttpGet]
        public async Task<IActionResult> GetAllIncomeCategories()
        {
            var listIncomeCategory = await _incomeCategoryService.GetAllIncomeCategories();

            if (listIncomeCategory.Any())
            {
                return Ok(listIncomeCategory);
            }
            else
            {
                return NotFound("Не найдено ни одной категории дохода!");
            }
        }

        // обновить категорию дохода
        [HttpPost]
        public async Task<IActionResult> UpdateIncomeCategory([FromBody] IncomeCategoryUpdate request)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _incomeCategoryService.UpdateIncomeCategory(request);
                    return Ok($"Категория Id : {request.IncomeCategoryId}, Name : {request.UpdateIncomeCategoryName} обновлена");
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
        public async Task<IActionResult> DeleteIncomeCategory(int incomeCategoryId)
        {
            try
            {
                await _incomeCategoryService.DeleteIncomeCategory(incomeCategoryId);
                return Ok("Категория удалена");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}