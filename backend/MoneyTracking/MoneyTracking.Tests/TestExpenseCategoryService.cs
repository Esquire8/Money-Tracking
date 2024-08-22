using MoneyTracking.Data.Entities;
using MoneyTracking.Data.UnitOfWork;
using MoneyTracking.Web.Services.ExpenseCategoryServ;
using Moq;

namespace MoneyTracking.Tests
{
    [TestClass]
    public class TestExpenseCategoryService
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly IExpenseCategoryService _expenseCategoryService;

        public TestExpenseCategoryService()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _expenseCategoryService = new ExpenseCategoryService(_mockUnitOfWork.Object);
        }

        [TestInitialize]
        public void Setup()
        {
            _mockUnitOfWork.Setup(x => x.ExpensesCategories.GetById(TestData.categoryId)).ReturnsAsync(TestData.expenseCategory);
            _mockUnitOfWork.Setup(x => x.ExpensesCategories.GetAll()).ReturnsAsync(TestData.listExpenseCategories);
        }

        [TestMethod]
        public void Test_CreateExpenseCategory()
        {
            // Arrange

            // Act
            var result = _expenseCategoryService.CreateExpenseCategory(TestData.expenseCategoryAdd);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.IsCompletedSuccessfully);
            Assert.IsNull(result.Exception);
        }

        [TestMethod]
        public void Test_DeleteExpenseCategory_WhenCategoryExist()
        {
            // Arrange

            // Act
            var result = _expenseCategoryService.DeleteExpenseCategory(TestData.categoryId);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.IsCompletedSuccessfully);
            Assert.IsNull(result.Exception);
        }

        [TestMethod]
        public void Test_DeleteExpenseCategory_WhenCategoryNotExist()
        {
            // Arrange
            _mockUnitOfWork.Setup(x => x.ExpensesCategories.GetById(It.IsAny<int>())).ReturnsAsync(() => null);

            // Act
            var result = _expenseCategoryService.DeleteExpenseCategory(TestData.categoryId);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsCompletedSuccessfully);
            Assert.IsNotNull(result.Exception);
        }

        [TestMethod]
        public async Task Test_GetAllExpenseCategories()
        {
            // Arrange

            // Act
            var result = await _expenseCategoryService.GetAllExpenseCategories();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());
            Assert.IsInstanceOfType(result, typeof(IEnumerable<ExpenseCategory>));
        }

        [TestMethod]
        public async Task Test_GetExpenseCategoryById_WhenCategoryExist()
        {
            // Arrange

            // Act
            var result = await _expenseCategoryService.GetExpenseCategoryById(TestData.categoryId);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ExpenseCategory));
            Assert.AreEqual(TestData.expenseCategory.Name, result.Name);
        }

        [TestMethod]
        public async Task Test_GetExpenseCategoryById_WhenCategoryNotExist()
        {
            // Arrange
            _mockUnitOfWork.Setup(x => x.ExpensesCategories.GetById(It.IsAny<int>())).ReturnsAsync(() => null);

            // Act
            var result = await _expenseCategoryService.GetExpenseCategoryById(TestData.categoryId);

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public async Task Test_UpdateExpenseCategory_WhenCategoryExist()
        {
            // Arrange

            // Act
            var result = _expenseCategoryService.UpdateExpenseCategory(TestData.expenseCategoryUpdate);
            var resultCategory = await _expenseCategoryService.GetExpenseCategoryById(TestData.categoryId);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(resultCategory);
            Assert.IsTrue(result.IsCompletedSuccessfully);
            Assert.IsNull(result.Exception);
            Assert.AreEqual(TestData.expenseCategoryUpdate.UpdateExpenseCategoryName, resultCategory.Name);
            Assert.AreEqual(1, resultCategory.Parentid);
        }

        [TestMethod]
        public void Test_UpdateExpenseCategory_WhenCategoryNotExist()
        {
            // Arrange
            _mockUnitOfWork.Setup(x => x.ExpensesCategories.GetById(It.IsAny<int>())).ReturnsAsync(() => null);

            // Act
            var result = _expenseCategoryService.UpdateExpenseCategory(TestData.expenseCategoryUpdate);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsCompletedSuccessfully);
            Assert.IsNotNull(result.Exception);
        }
    }
}