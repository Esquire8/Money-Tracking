using MoneyTracking.Data.Entities;
using MoneyTracking.Data.UnitOfWork;
using MoneyTracking.Web.Services.ExpenseServ;
using Moq;

namespace MoneyTracking.Tests
{
    [TestClass]
    public class TestExpenseService
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly IExpenseService _expenseService;

        public TestExpenseService()
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
        public void Test_CreateExpense()
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
        public void Test_DeleteExpense_WhenExpenseExist()
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
        public void Test_DeleteExpense_WhenExpenseNotExist()
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
        public async Task Test_GetAllExpenses()
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
        public async Task Test_GetAllExpensesByUser()
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
        public async Task Test_GetExpenseById_WhenExpenseExist()
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
        public async Task Test_GetExpenseById_WhenExpenseNotExist()
        {
            // Arrange
            _mockUnitOfWork.Setup(x => x.Expenses.GetById(It.IsAny<int>())).ReturnsAsync(() => null);

            // Act
            var result = await _expenseService.GetExpenseById(TestData.userId);

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public async Task Test_UpdateExpense_WhenExpenseExist()
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
        public void Test_UpdateExpense_WhenExpenseNotExist()
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