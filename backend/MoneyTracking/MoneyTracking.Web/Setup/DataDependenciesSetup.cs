using MoneyTracking.Data.Repositories;
using MoneyTracking.Data.Repositories.ExpenseCategoryRep;
using MoneyTracking.Data.Repositories.ExpenseRep;
using MoneyTracking.Data.Repositories.IncomeCategoryRep;
using MoneyTracking.Data.Repositories.IncomeRep;
using MoneyTracking.Data.Repositories.UserRep;
using MoneyTracking.Data.UnitOfWork;

namespace MoneyTracking.Web.Setup
{
    public static class DataDependenciesSetup
    {
        public static void AddDataDependencies(this IServiceCollection services)
        {
            // Inject repositories
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IIncomeRepository, IncomeRepository>();
            services.AddTransient<IIncomeCategeryRepository, IncomeCategoryRepository>();
            services.AddTransient<IExpenseRepository, ExpenseRepository>();
            services.AddTransient<IExpenseCategoryRepository, ExpenseCategoryRepository>();
        }
    }
}