using MoneyTracking.Web.Services.ExpenseCategoryServ;
using MoneyTracking.Web.Services.ExpenseServ;
using MoneyTracking.Web.Services.IncomeCategoryServ;
using MoneyTracking.Web.Services.IncomeServ;
using MoneyTracking.Web.Services.UserServ;

namespace MoneyTracking.Web.Setup
{
    public static class ServiceDependenciesSetup
    {
        public static void AddServiceDependencies(this IServiceCollection services)
        {
            // Inject services
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IIncomeService, IncomeService>();
            services.AddTransient<IIncomeCategoryService, IncomeCategoryService>();
            services.AddTransient<IExpenseService, ExpenseService>();
            services.AddTransient<IExpenseCategoryService, ExpenseCategoryService>();
        }
    }
}