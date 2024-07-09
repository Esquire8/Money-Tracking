using MoneyTracking.Data.Entities;
using MoneyTracking.Data.UnitOfWork;

namespace MoneyTracking.Web.Services.ExpenseServ
{
    public class ExpenseService : IExpenseService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ExpenseService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task CreateExpense(Expense expense)
        {
            await _unitOfWork.Expenses.Add(expense);
            await _unitOfWork.Save();
        }

        public async Task DeleteExpense(Expense expense)
        {
            _unitOfWork.Expenses.Delete(expense);
            await _unitOfWork.Save();
        }

        public async Task<IEnumerable<Expense>> GetAllExpenses()
        {
            return await _unitOfWork.Expenses.GetAll();
        }

        public async Task<Expense?> GetExpenseById(int id)
        {
            return await _unitOfWork.Expenses.GetById(id);
        }

        public async Task UpdateExpense(Expense expense)
        {
            _unitOfWork.Expenses.Update(expense);
            await _unitOfWork.Save();
        }
    }
}