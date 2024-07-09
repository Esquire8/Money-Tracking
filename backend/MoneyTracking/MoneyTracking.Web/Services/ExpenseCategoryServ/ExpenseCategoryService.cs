using MoneyTracking.Data.Entities;
using MoneyTracking.Data.UnitOfWork;

namespace MoneyTracking.Web.Services.ExpenseCategoryServ
{
    public class ExpenseCategoryService : IExpenseCategoryService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ExpenseCategoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task CreateExpenseCategory(ExpenseCategory expenseCategory)
        {
            await _unitOfWork.ExpensesCategories.Add(expenseCategory);
            await _unitOfWork.Save();
        }

        public async Task DeleteExpenseCategory(ExpenseCategory expenseCategory)
        {
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

        public async Task UpdateExpenseCategory(ExpenseCategory expenseCategory)
        {
            _unitOfWork.ExpensesCategories.Update(expenseCategory);
            await _unitOfWork.Save();
        }
    }
}