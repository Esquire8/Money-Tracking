using MoneyTracking.Data.Entities;
using MoneyTracking.Data.UnitOfWork;
using MoneyTracking.Web.Services.IncomeCategoryServ;
using Moq;

namespace MoneyTracking.Tests
{
    [TestClass]
    public class TestIncomeCategoryService
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly IIncomeCategoryService _incomeCategoryService;

        public TestIncomeCategoryService()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _incomeCategoryService = new IncomeCategoryService(_mockUnitOfWork.Object);
        }

        [TestInitialize]
        public void Setup()
        {
            _mockUnitOfWork.Setup(x => x.IncomesCategeries.GetById(TestData.categoryId)).ReturnsAsync(TestData.incomeCategory);
            _mockUnitOfWork.Setup(x => x.IncomesCategeries.GetAll()).ReturnsAsync(TestData.listIncomeCategories);
        }

        [TestMethod]
        public void Test_CreateIncomeCategory()
        {
            // Arrange

            // Act
            var result = _incomeCategoryService.CreateIncomeCategory(TestData.incomeCategory.Name);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.IsCompletedSuccessfully);
            Assert.IsNull(result.Exception);
        }

        [TestMethod]
        public void Test_DeleteIncomeCategory_WhenCategoryExist()
        {
            // Arrange

            // Act
            var result = _incomeCategoryService.DeleteIncomeCategory(TestData.categoryId);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.IsCompletedSuccessfully);
            Assert.IsNull(result.Exception);
        }

        [TestMethod]
        public void Test_DeleteIncomeCategory_WhenCategoryNotExist()
        {
            // Arrange
            _mockUnitOfWork.Setup(x => x.IncomesCategeries.GetById(It.IsAny<int>())).ReturnsAsync(() => null);

            // Act
            var result = _incomeCategoryService.DeleteIncomeCategory(TestData.categoryId);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsCompletedSuccessfully);
            Assert.IsNotNull(result.Exception);
        }

        [TestMethod]
        public async Task Test_GetAllIncomeCategories()
        {
            // Arrange

            // Act
            var result = await _incomeCategoryService.GetAllIncomeCategories();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());
            Assert.IsInstanceOfType(result, typeof(IEnumerable<IncomeCategory>));
        }

        [TestMethod]
        public async Task Test_GetIncomeCategoryById_WhenCategoryExist()
        {
            // Arrange

            // Act
            var result = await _incomeCategoryService.GetIncomeCategoryById(TestData.categoryId);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(IncomeCategory));
            Assert.AreEqual(TestData.incomeCategory.Name, result.Name);
        }

        [TestMethod]
        public async Task Test_GetIncomeCategoryById_WhenCategoryNotExist()
        {
            // Arrange
            _mockUnitOfWork.Setup(x => x.IncomesCategeries.GetById(It.IsAny<int>())).ReturnsAsync(() => null);

            // Act
            var result = await _incomeCategoryService.GetIncomeCategoryById(TestData.categoryId);

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public async Task Test_UpdateIncomeCategory_WhenCategoryExist()
        {
            // Arrange

            // Act
            var result = _incomeCategoryService.UpdateIncomeCategory(TestData.incomeCategoryUpdate);
            var resultCategory = await _incomeCategoryService.GetIncomeCategoryById(TestData.categoryId);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(resultCategory);
            Assert.IsTrue(result.IsCompletedSuccessfully);
            Assert.IsNull(result.Exception);
            Assert.AreEqual(TestData.incomeCategoryUpdate.UpdateIncomeCategoryName, resultCategory.Name);
        }

        [TestMethod]
        public void Test_UpdateIncomeCategory_WhenCategoryNotExist()
        {
            // Arrange
            _mockUnitOfWork.Setup(x => x.IncomesCategeries.GetById(It.IsAny<int>())).ReturnsAsync(() => null);

            // Act
            var result = _incomeCategoryService.UpdateIncomeCategory(TestData.incomeCategoryUpdate);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsCompletedSuccessfully);
            Assert.IsNotNull(result.Exception);
        }
    }
}