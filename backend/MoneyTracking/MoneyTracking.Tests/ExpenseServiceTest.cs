using MoneyTracking.Data.Entities;
using MoneyTracking.Data.UnitOfWork;
using MoneyTracking.Web.Services.ExpenseServ;
using Moq;

namespace MoneyTracking.Tests
{
    [TestClass]
    public class ExpenseServiceTest
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly IExpenseService _expenseService;

        public ExpenseServiceTest()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _expenseService = new ExpenseService(_mockUnitOfWork.Object);
        }

        [TestInitialize]
        public void Setup()
        {
            _mockUnitOfWork.Setup(x => x.Expenses.GetById(TestData.userId)).ReturnsAsync(TestData.expense);
            _mockUnitOfWork.Setup(x => x.Expenses.GetAll()).ReturnsAsync(TestData.GetListExpenses());
            _mockUnitOfWork.Setup(x => x.Expenses.GetAllByUser(TestData.userId)).ReturnsAsync(TestData.user.Expenses);
            _mockUnitOfWork.Setup(x => x.ExpensesCategories.GetById(TestData.categoryId)).ReturnsAsync(TestData.expenseCategory);
        }

        [TestMethod]
        public void CreateExpense_SuccessfullyCreated()
        {
            // Arrange
            _mockUnitOfWork.Setup(x => x.Users.GetById(TestData.userId)).ReturnsAsync(TestData.user);
            _mockUnitOfWork.Setup(x => x.ExpensesCategories.GetById(TestData.categoryId)).ReturnsAsync(TestData.expenseCategory);

            // Act
            var result = _expenseService.CreateExpense(TestData.expenseAdd);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.IsCompletedSuccessfully);
            Assert.IsNull(result.Exception);
        }

        [TestMethod]
        public void DeleteExpense_WhenExpenseExist_SuccessfullyDeleted()
        {
            // Arrange

            // Act
            var result = _expenseService.DeleteExpense(TestData.expense.Id);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.IsCompletedSuccessfully);
            Assert.IsNull(result.Exception);
        }

        [TestMethod]
        public void DeleteExpense_WhenExpenseNotExist_DeleteFailed()
        {
            // Arrange
            _mockUnitOfWork.Setup(x => x.Expenses.GetById(It.IsAny<int>())).ReturnsAsync(() => null);

            // Act
            var result = _expenseService.DeleteExpense(TestData.expense.Id);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsCompletedSuccessfully);
            Assert.IsNotNull(result.Exception);
        }

        [TestMethod]
        public async Task GetAllExpenses_WhenExpenseExist_SuccessfullyFound()
        {
            // Arrange

            // Act
            var result = await _expenseService.GetAllExpenses();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Count());
            Assert.IsInstanceOfType(result, typeof(IEnumerable<Expense>));
        }

        [TestMethod]
        public async Task GetAllExpensesByUser_WhenUserExpensesExist_SuccessfullyFound()
        {
            // Arrange

            // Act
            var result = await _expenseService.GetAllExpensesByUser(TestData.userId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Count());
            Assert.IsInstanceOfType(result, typeof(IEnumerable<Expense>));
        }

        [TestMethod]
        public async Task GetExpenseById_WhenExpenseExist_SuccessfullyFound()
        {
            // Arrange

            // Act
            var result = await _expenseService.GetExpenseById(TestData.userId);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Expense));
            Assert.AreEqual(TestData.expense.Amount, result.Amount);
        }

        [TestMethod]
        public async Task GetExpenseById_WhenExpenseNotExist_NotFound()
        {
            // Arrange
            _mockUnitOfWork.Setup(x => x.Expenses.GetById(It.IsAny<int>())).ReturnsAsync(() => null);

            // Act
            var result = await _expenseService.GetExpenseById(TestData.userId);

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public async Task UpdateExpense_WhenExpenseExist_SuccessfullyUpdated()
        {
            // Arrange

            // Act
            var result = _expenseService.UpdateExpense(TestData.expenseUpdate);
            var resultExpense = await _expenseService.GetExpenseById(TestData.expense.Id);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(resultExpense);
            Assert.IsTrue(result.IsCompletedSuccessfully);
            Assert.IsNull(result.Exception);
            Assert.AreEqual(TestData.expenseUpdate.Amount, resultExpense.Amount);
        }

        [TestMethod]
        public void UpdateExpense_WhenExpenseNotExist_UpdateFailed()
        {
            // Arrange
            _mockUnitOfWork.Setup(x => x.Expenses.GetById(It.IsAny<int>())).ReturnsAsync(() => null);

            // Act
            var result = _expenseService.UpdateExpense(TestData.expenseUpdate);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsCompletedSuccessfully);
            Assert.IsNotNull(result.Exception);
        }
    }
}