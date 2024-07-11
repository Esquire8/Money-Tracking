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
            var updateExpense = await GetExpenseById(expense.ExpenseId) ?? throw new Exception();
            var updateExpenseCategory = await _unitOfWork.ExpensesCategories.GetById(expense.ExpenseCategoryId) ?? throw new Exception();

            updateExpense.Amount = expense.Amount;
            updateExpense.Description = expense.Description;
            updateExpense.ExpenseCategory = updateExpenseCategory;

            _unitOfWork.Expenses.Update(updateExpense);
            await _unitOfWork.Save();
        }
    }
}