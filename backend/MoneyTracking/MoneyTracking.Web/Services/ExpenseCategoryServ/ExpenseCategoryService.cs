using MoneyTracking.Data.Entities;
using MoneyTracking.Data.UnitOfWork;
using MoneyTracking.Web.Models.ExpenseCategoryModels;

namespace MoneyTracking.Web.Services.ExpenseCategoryServ
{
    public class ExpenseCategoryService : IExpenseCategoryService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ExpenseCategoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task CreateExpenseCategory(ExpenseCategoryAdd ExpenseCategory)
        {
            var newExpenseCategory = new ExpenseCategory { Name = ExpenseCategory.CategoryName, Parentid = ExpenseCategory.ParentId };

            await _unitOfWork.ExpensesCategories.Add(newExpenseCategory);
            await _unitOfWork.Save();
        }

        public async Task DeleteExpenseCategory(int categoryId)
        {
            var expenseCategory = await GetExpenseCategoryById(categoryId) ?? throw new Exception();

            _unitOfWork.ExpensesCategories.Delete(expenseCategory);
            await _unitOfWork.Save();
        }

        public async Task<IEnumerable<ExpenseCategory>> GetAllExpenseCategories()
        {
            return await _unitOfWork.ExpensesCategories.GetAll();
        }

        public async Task<ExpenseCategory?> GetExpenseCategoryById(int id)
        {
            return await _unitOfWork.ExpensesCategories.GetById(id);
        }

        public async Task UpdateExpenseCategory(ExpenseCategoryUpdate expenseCategory)
        {
            var updatedExpenseCategory = await GetExpenseCategoryById(expenseCategory.ExpenseCategoryId) ?? throw new Exception();

            updatedExpenseCategory.Name = expenseCategory.UpdateExpenseCategoryName;
            updatedExpenseCategory.Parentid = expenseCategory.UpdateParentId;

            _unitOfWork.ExpensesCategories.Update(updatedExpenseCategory);
            await _unitOfWork.Save();
        }
    }
}