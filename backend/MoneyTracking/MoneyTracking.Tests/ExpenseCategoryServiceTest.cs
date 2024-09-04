using MoneyTracking.Data.Entities;
using MoneyTracking.Data.UnitOfWork;
using MoneyTracking.Web.Services.ExpenseCategoryServ;
using Moq;

namespace MoneyTracking.Tests
{
    [TestClass]
    public class ExpenseCategoryServiceTest
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly IExpenseCategoryService _expenseCategoryService;

        public ExpenseCategoryServiceTest()
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
        public void CreateExpenseCategory_SuccessfullyCreated()
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
        public void DeleteExpenseCategory_WhenCategoryExist_SuccessfullyDeleted()
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
        public void DeleteExpenseCategory_WhenCategoryNotExist_DeleteFailed()
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
        public async Task GetAllExpenseCategories_WhenCategoryExist_SuccessfullyFound()
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
        public async Task GetExpenseCategoryById_WhenCategoryExist_SuccessfullyFound()
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
        public async Task GetExpenseCategoryById_WhenCategoryNotExist_NotFound()
        {
            // Arrange
            _mockUnitOfWork.Setup(x => x.ExpensesCategories.GetById(It.IsAny<int>())).ReturnsAsync(() => null);

            // Act
            var result = await _expenseCategoryService.GetExpenseCategoryById(TestData.categoryId);

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public async Task UpdateExpenseCategory_WhenCategoryExist_SuccessfullyUpdated()
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
        public void UpdateExpenseCategory_WhenCategoryNotExist_UpdateFailed()
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