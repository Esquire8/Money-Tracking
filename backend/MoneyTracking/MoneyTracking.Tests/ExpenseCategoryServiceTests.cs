using MoneyTracking.Data.Entities;
using MoneyTracking.Data.UnitOfWork;
using MoneyTracking.Web.Models.ExpenseCategoryModels;
using MoneyTracking.Web.Services.ExpenseCategoryServ;
using Moq;

namespace MoneyTracking.Tests
{
    [TestClass]
    public class ExpenseCategoryServiceTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly IExpenseCategoryService _expenseCategoryService;

        public ExpenseCategoryServiceTests()
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
        [DataRow(1, true)]
        [DataRow(2, false)]
        public void TryDeleteCategory(int categoryId, bool exptected)
        {
            // Arrange

            // Act
            var result = _expenseCategoryService.DeleteExpenseCategory(categoryId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(exptected, result.IsCompletedSuccessfully);
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
        [DataRow(2)]
        public async Task GetExpenseCategoryById_WhenCategoryNotExist_NotFound(int categoryId)
        {
            // Arrange

            // Act
            var result = await _expenseCategoryService.GetExpenseCategoryById(categoryId);

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        [DataRow(1, "Фрукты", 1, true)]
        [DataRow(2, "Фрукты", 1, false)]
        public void TryUpdateCategory(int categoryId, string categoryName, int parentId, bool exptected)
        {
            // Arrange
            ExpenseCategoryUpdate expenseCategoryUpdate = new ExpenseCategoryUpdate(
                ExpenseCategoryId: categoryId,
                UpdateExpenseCategoryName: categoryName,
                UpdateParentId: parentId);

            // Act
            var result = _expenseCategoryService.UpdateExpenseCategory(expenseCategoryUpdate);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(exptected, result.IsCompletedSuccessfully);
        }
    }
}