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

        public async Task DeleteExpense(int id)
        {
            await _unitOfWork.Expenses.Delete(id);
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

        public void UpdateExpense(Expense expense)
        {
            _unitOfWork.Expenses.Update(expense);
            _unitOfWork.Save();
        }
    }
}