using MoneyTracking.Data.Entities;

namespace MoneyTracking.Web.Services.IncomeServ
{
    public interface IIncomeService
    {
        Task<IEnumerable<Income>> GetAllIncomes();

        Task<Income?> GetIncomeById(int id);

        Task CreateIncome(Income income);

        Task DeleteIncome(int id);

        void UpdateIncome(Income income);
    }
}