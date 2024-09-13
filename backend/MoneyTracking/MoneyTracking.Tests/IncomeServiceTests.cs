using MoneyTracking.Data.Entities;
using MoneyTracking.Data.UnitOfWork;
using MoneyTracking.Web.Services.IncomeServ;
using Moq;

namespace MoneyTracking.Tests
{
    [TestClass]
    public class IncomeServiceTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly IIncomeService _incomeService;

        public IncomeServiceTests()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _incomeService = new IncomeService(_mockUnitOfWork.Object);
        }

        [TestInitialize]
        public void Setup()
        {
            _mockUnitOfWork.Setup(x => x.Incomes.GetById(TestData.userId)).ReturnsAsync(TestData.income);
            _mockUnitOfWork.Setup(x => x.Incomes.GetAll()).ReturnsAsync(TestData.GetListIncomes());
            _mockUnitOfWork.Setup(x => x.Incomes.GetAllByUser(TestData.userId)).ReturnsAsync(TestData.user.Incomes);
            _mockUnitOfWork.Setup(x => x.IncomesCategeries.GetById(TestData.categoryId)).ReturnsAsync(TestData.incomeCategory);
        }

        [TestMethod]
        public void CreateIncome_SuccessfullyCreated()
        {
            // Arrange
            _mockUnitOfWork.Setup(x => x.Users.GetById(TestData.userId)).ReturnsAsync(TestData.user);
            _mockUnitOfWork.Setup(x => x.IncomesCategeries.GetById(TestData.categoryId)).ReturnsAsync(TestData.incomeCategory);

            // Act
            var result = _incomeService.CreateIncome(TestData.incomeAdd);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.IsCompletedSuccessfully);
            Assert.IsNull(result.Exception);
        }

        [TestMethod]
        public void DeleteIncome_WhenIncomeExist_SuccessfullyDeleted()
        {
            // Arrange

            // Act
            var result = _incomeService.DeleteIncome(TestData.income.Id);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.IsCompletedSuccessfully);
            Assert.IsNull(result.Exception);
        }

        [TestMethod]
        public void DeleteIncome_WhenIncomeNotExist_DeleteFailed()
        {
            // Arrange
            _mockUnitOfWork.Setup(x => x.Incomes.GetById(It.IsAny<int>())).ReturnsAsync(() => null);

            // Act
            var result = _incomeService.DeleteIncome(TestData.income.Id);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsCompletedSuccessfully);
            Assert.IsNotNull(result.Exception);
        }

        [TestMethod]
        public async Task GetAllIncomes_WhenIncomeExist_SuccessfullyFound()
        {
            // Arrange

            // Act
            var result = await _incomeService.GetAllIncomes();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Count());
            Assert.IsInstanceOfType(result, typeof(IEnumerable<Income>));
        }

        [TestMethod]
        public async Task GetAllIncomesByUser_WhenUserIncomesExist_SuccessfullyFound()
        {
            // Arrange

            // Act
            var result = await _incomeService.GetAllIncomesByUser(TestData.userId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Count());
            Assert.IsInstanceOfType(result, typeof(IEnumerable<Income>));
        }

        [TestMethod]
        public async Task GetIncomeById_WhenIncomeExist_SuccessfullyFound()
        {
            // Arrange

            // Act
            var result = await _incomeService.GetIncomeById(TestData.userId);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Income));
            Assert.AreEqual(TestData.income.Amount, result.Amount);
        }

        [TestMethod]
        public async Task GetIncomeById_WhenIncomeNotExist_NotFound()
        {
            // Arrange
            _mockUnitOfWork.Setup(x => x.Incomes.GetById(It.IsAny<int>())).ReturnsAsync(() => null);

            // Act
            var result = await _incomeService.GetIncomeById(TestData.userId);

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public async Task UpdateIncome_WhenIncomeExist_SuccessfullyUpdate()
        {
            // Arrange

            // Act
            var result = _incomeService.UpdateIncome(TestData.incomeUpdate);
            var resultIncome = await _incomeService.GetIncomeById(TestData.income.Id);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(resultIncome);
            Assert.IsTrue(result.IsCompletedSuccessfully);
            Assert.IsNull(result.Exception);
            Assert.AreEqual(TestData.incomeUpdate.Amount, resultIncome.Amount);
        }

        [TestMethod]
        public void UpdateIncome_WhenIncomeNotExist_UpdateFailed()
        {
            // Arrange
            _mockUnitOfWork.Setup(x => x.Incomes.GetById(It.IsAny<int>())).ReturnsAsync(() => null);

            // Act
            var result = _incomeService.UpdateIncome(TestData.incomeUpdate);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsCompletedSuccessfully);
            Assert.IsNotNull(result.Exception);
        }
    }
}