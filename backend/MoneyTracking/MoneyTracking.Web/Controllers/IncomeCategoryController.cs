using Microsoft.AspNetCore.Mvc;
using MoneyTracking.Data.Entities;
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

        [HttpPost]
        public async Task<IActionResult> AddIncomeCategory(string categoryName)
        {
            var incomeCategory = new IncomeCategory { Name = categoryName };

            try
            {
                await _incomeCategoryService.CreateIncomeCategory(incomeCategory);
                return Ok($"Категория id = {incomeCategory.Id}, Название = {incomeCategory.Name}");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

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
                return BadRequest("Не найдено ни одной категории дохода");
            }
        }

        [HttpPost]
        public async Task<IActionResult> UpdateIncomeCategory([FromBody] IncomeCategoryUpdate incomeCtgryUpdate)
        {
            var incomeCategory = await _incomeCategoryService.GetIncomeCategoryById(incomeCtgryUpdate.IncomeCategoryId);
            var updateIncomeCategory = new IncomeCategory() { Name = incomeCtgryUpdate.UpdateIncomeCategoryName };

            if (incomeCategory != null)
            {
                try
                {
                    await _incomeCategoryService.UpdateIncomeCategory(updateIncomeCategory);
                    return Ok("Категория обновлена");
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            else
            {
                return BadRequest("Категория не найдена");
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteIncomeCategory(int incomeCtgryId)
        {
            var incomeCtgry = await _incomeCategoryService.GetIncomeCategoryById(incomeCtgryId);

            if (incomeCtgry != null)
            {
                try
                {
                    await _incomeCategoryService.DeleteIncomeCategory(incomeCtgry);
                    return Ok("Категория удалена");
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            else
            {
                return BadRequest("Категория не найдена");
            }
        }
    }
}