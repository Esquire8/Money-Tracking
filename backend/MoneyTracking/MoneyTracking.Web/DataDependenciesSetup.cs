using MoneyTracking.Data.Repositories;
using MoneyTracking.Data.Repositories.ExpenseCategoryRep;
using MoneyTracking.Data.Repositories.ExpenseRep;
using MoneyTracking.Data.Repositories.IncomeCategoryRep;
using MoneyTracking.Data.Repositories.IncomeRep;
using MoneyTracking.Data.Repositories.UserRep;
using MoneyTracking.Data.UnitOfWork;
using MoneyTracking.Web.Services.ExpenseCategoryServ;
using MoneyTracking.Web.Services.ExpenseServ;
using MoneyTracking.Web.Services.IncomeCategoryServ;
using MoneyTracking.Web.Services.IncomeServ;

using MoneyTracking.Web.Services.UserServ;

namespace MoneyTracking.Web
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

            // Inject services
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IIncomeService, IncomeService>();
            services.AddScoped<IIncomeCategoryService, IncomeCategoryService>();
            services.AddScoped<IExpenseService, ExpenseService>();
            services.AddScoped<IExpenseCategoryService, ExpenseCategoryService>();
        }
    }
}