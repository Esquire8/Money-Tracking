using MoneyTracking.Data.Entities;

namespace MoneyTracking.Web.Services.IncomeServ
{
    public interface IIncomeService
    {
        Task<IEnumerable<Income>> GetAllIncomes();

        Task<IEnumerable<Income>> GetUserAllIncomes(int userId);

        Task<Income?> GetIncomeById(int id);

        Task CreateIncome(Income income);

        Task DeleteIncome(Income income);

        Task UpdateIncome(Income income);
    }
}