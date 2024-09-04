using MoneyTracking.Data.Entities;
using MoneyTracking.Data.UnitOfWork;
using MoneyTracking.Web.Models.ExpenseModels;

namespace MoneyTracking.Web.Services.ExpenseServ
{
    public class ExpenseService : IExpenseService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ExpenseService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task CreateExpense(ExpenseAdd newExpense)
        {
            var user = await _unitOfWork.Users.GetById(newExpense.UserId) ?? throw new Exception();
            var expenseCategory = await _unitOfWork.ExpensesCategories.GetById(newExpense.ExpenseCategoryId) ?? throw new Exception();

            var expense = new Expense
            {
                Amount = newExpense.Amount,
                Description = newExpense.Description,
                ExpenseCategory = expenseCategory,
                ExpenseDate = DateTime.UtcNow,
                User = user,
            };

            await _unitOfWork.Expenses.Add(expense);
            await _unitOfWork.Save();
        }

        public async Task DeleteExpense(int expenseId)
        {
            var expense = await GetExpenseById(expenseId) ?? throw new Exception();

            _unitOfWork.Expenses.Delete(expense);
            await _unitOfWork.Save();
        }

        public async Task<IEnumerable<Expense>> GetAllExpenses()
        {
            return await _unitOfWork.Expenses.GetAll();
        }

        public async Task<IEnumerable<Expense>> GetAllExpensesByUser(int userId)
        {
            return await _unitOfWork.Expenses.GetAllByUser(userId);
        }

        public async Task<Expense?> GetExpenseById(int id)
        {
            return await _unitOfWork.Expenses.GetById(id);
        }

        public async Task UpdateExpense(ExpenseUpdate expense)
        {
            var updatedExpense = await GetExpenseById(expense.ExpenseId) ?? throw new Exception();
            var updatedExpenseCategory = await _unitOfWork.ExpensesCategories.GetById(expense.ExpenseCategoryId) ?? throw new Exception();

            updatedExpense.Amount = expense.Amount;
            updatedExpense.Description = expense.Description;
            updatedExpense.ExpenseCategory = updatedExpenseCategory;

            _unitOfWork.Expenses.Update(updatedExpense);
            await _unitOfWork.Save();
        }
    }
}