using Microsoft.AspNetCore.Mvc;
using MoneyTracking.Web.Models.IncomeCategoryModels;
using MoneyTracking.Web.Services.IncomeCategoryServ;

namespace MoneyTracking.Web.Controllers
{
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
        public async Task<IActionResult> AddIncomeCategory(string CategoryName)
        {
            try
            {
                await _incomeCategoryService.CreateIncomeCategory(CategoryName);
                return Ok($"Категория {CategoryName} добавлена!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // получить все категории дохода
        [HttpGet]
        public async Task<IActionResult> GetAllIncomeCategories()
        {
            var listIncomeCat = await _incomeCategoryService.GetAllIncomeCategories();

            if (listIncomeCat != null)
            {
                return Ok(listIncomeCat);
            }
            else
            {
                return NotFound("Не найдено ни одной категории дохода");
            }
        }

        // обновить категорию дохода
        [HttpPost]
        public async Task<IActionResult> UpdateIncomeCategory([FromBody] IncomeCategoryUpdate request)
        {
            if (request != null)
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
                return BadRequest("Введите название категории");
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