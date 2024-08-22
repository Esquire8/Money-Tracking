using MoneyTracking.Data.Entities;
using MoneyTracking.Web.Models.ExpenseCategoryModels;
using MoneyTracking.Web.Models.ExpenseModels;
using MoneyTracking.Web.Models.IncomeCategoryModels;
using MoneyTracking.Web.Models.IncomeModels;
using MoneyTracking.Web.Models.UserModels;

namespace MoneyTracking.Tests
{
    internal static class TestData
    {
        #region Test Data for UserService

        // TestUserService
        internal static readonly int userId = 1;

        internal static readonly User user = new User
        {
            Id = userId,
            Email = "efim_sokoloff@mail.ru",
            Login = "Sokol",
            Password = "efim_sokol123",
            Incomes = GetListIncomes(),
            Expenses = GetListExpenses()
        };

        internal static readonly UserUpdate userUpdate = new UserUpdate(
            Id: userId,
            NewEmail: user.Email,
            NewLogin: "Sokoloff4ik",
            NewPassword: user.Password);

        internal static readonly UserAdd userAdd = new UserAdd(
                Email: user.Email,
                Login: user.Login,
                Password: user.Password);

        internal static List<User> GetListUsers()
        {
            var regDate = DateTime.UtcNow;

            var users = new List<User>
            {
                new User { Id = 1, Email = "efim_sokoloff@mail.ru", Login = "Sokol", Password = "efim_sokol123", RegistrationDate = regDate },
                new User { Id = 2, Email = "mishka228@mail.ru", Login = "Sokol", Password = "efim_sokol123", RegistrationDate = regDate }
            };

            return users;
        }

        internal static List<User> GetListUsersEmpty()
        {
            var regDate = DateTime.UtcNow;

            var users = new List<User>();

            return users;
        }

        #endregion Test Data for UserService

        #region Test Data for IncomeCategoryService

        // TestIncomeCategoryService
        internal static readonly int categoryId = 1;

        internal static readonly IncomeCategory incomeCategory = new IncomeCategory()
        {
            Id = categoryId,
            Name = "Зарплата",
            Incomes = GetListIncomes()
        };

        internal static readonly IncomeCategoryUpdate incomeCategoryUpdate = new IncomeCategoryUpdate(IncomeCategoryId: categoryId,
            UpdateIncomeCategoryName: "Прочее");

        internal static readonly List<IncomeCategory> listIncomeCategories = new List<IncomeCategory>
        {
            new IncomeCategory { Id = 1, Name = "Зарплата", Incomes = GetListIncomes() },
            new IncomeCategory { Id = 2, Name = "Аванс", Incomes = GetListIncomesEmpty() },
        };

        #endregion Test Data for IncomeCategoryService

        #region Test Data for IncomeService

        // TestIncomeService
        internal static readonly Income income = new Income()
        {
            Id = 1,
            Amount = 22500,
            User = user,
            IncomeCategory = incomeCategory,
            Description = "description"
        };

        internal static readonly IncomeAdd incomeAdd = new IncomeAdd(UserId: userId,
                IncomeCategoryId: categoryId,
                Amount: income.Amount,
                Description: income.Description);

        internal static readonly IncomeUpdate incomeUpdate = new IncomeUpdate(IncomeId: 1,
            Description: income.Description,
            IncomeCategoryId: incomeCategory.Id,
            Amount: 25000);

        internal static List<Income> GetListIncomes()
        {
            var incomeDate = DateTime.UtcNow;
            var incomes = new List<Income>
            {
                new Income { Id = 1, Amount = 10000, User = user, IncomeDate = incomeDate, IncomeCategory = incomeCategory},
                new Income { Id = 2, Amount = 28000, User = user, IncomeDate = incomeDate, IncomeCategory = incomeCategory },
                new Income { Id = 3, Amount = 5000, User = user, IncomeDate = incomeDate, IncomeCategory = incomeCategory },
            };

            return incomes;
        }

        internal static List<Income> GetListIncomesEmpty()
        {
            var incomeDate = DateTime.UtcNow;
            var incomes = new List<Income>();

            return incomes;
        }

        #endregion Test Data for IncomeService

        #region Test Data for ExpenseCategoryService

        // TestExpenseCategoryService
        internal static readonly ExpenseCategory expenseCategory = new ExpenseCategory()
        {
            Id = categoryId,
            Name = "Продукты",
            Expenses = GetListExpenses()
        };

        internal static readonly ExpenseCategoryAdd expenseCategoryAdd = new ExpenseCategoryAdd(CategoryName: expenseCategory.Name, ParentId: null);

        internal static readonly ExpenseCategoryUpdate expenseCategoryUpdate = new ExpenseCategoryUpdate(ExpenseCategoryId: categoryId,
            UpdateExpenseCategoryName: "Фрукты",
            UpdateParentId: 1);

        internal static readonly List<ExpenseCategory> listExpenseCategories = new List<ExpenseCategory>
        {
            new ExpenseCategory { Id = 1, Name = "Зарплата", Expenses = GetListExpenses() },
            new ExpenseCategory { Id = 2, Name = "Аванс", Expenses = GetListExpensesEmpty() },
        };

        #endregion Test Data for ExpenseCategoryService

        #region Test Data for ExpenseService

        // TestExpenseService
        internal static readonly Expense expense = new Expense()
        {
            Id = 1,
            Amount = 2250,
            User = user,
            ExpenseCategory = expenseCategory,
            Description = "description"
        };

        internal static readonly ExpenseAdd expenseAdd = new ExpenseAdd(UserId: userId,
                ExpenseCategoryId: categoryId,
                Amount: expense.Amount,
                Description: expense.Description);

        internal static readonly ExpenseUpdate expenseUpdate = new ExpenseUpdate(ExpenseId: expense.Id,
                ExpenseCategoryId: categoryId,
                Amount: 2500,
                Description: expense.Description);

        internal static List<Expense> GetListExpenses()
        {
            var expenseDate = DateTime.UtcNow;
            var expenses = new List<Expense>
            {
                new Expense { Id = 1, Amount = 1000, User = user, ExpenseDate = expenseDate, ExpenseCategory = expenseCategory},
                new Expense { Id = 2, Amount = 2800, User = user, ExpenseDate = expenseDate, ExpenseCategory = expenseCategory },
                new Expense { Id = 3, Amount = 500, User = user, ExpenseDate = expenseDate, ExpenseCategory = expenseCategory },
            };

            return expenses;
        }

        internal static List<Expense> GetListExpensesEmpty()
        {
            var expenseDate = DateTime.UtcNow;
            var expenses = new List<Expense>();

            return expenses;
        }

        #endregion Test Data for ExpenseService
    }
}