using MoneyTracking.Data.Entities;
using MoneyTracking.Web.Models.IncomeModels;

namespace MoneyTracking.Web.Services.IncomeServ
{
    public interface IIncomeService
    {
        Task<IEnumerable<Income>> GetAllIncomes();

        Task<IEnumerable<Income>> GetAllIncomesByUser(int userId);

        Task<Income?> GetIncomeById(int id);

        Task CreateIncome(IncomeAdd incomeAdd);

        Task DeleteIncome(int incomeId);

        Task UpdateIncome(IncomeUpdate incomeUpdate);
    }
}