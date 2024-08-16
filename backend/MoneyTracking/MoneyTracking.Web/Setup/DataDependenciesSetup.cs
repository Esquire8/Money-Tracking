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
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IIncomeRepository, IncomeRepository>();
            services.AddScoped<IIncomeCategeryRepository, IncomeCategoryRepository>();
            services.AddScoped<IExpenseRepository, ExpenseRepository>();
            services.AddScoped<IExpenseCategoryRepository, ExpenseCategoryRepository>();
        }
    }
}