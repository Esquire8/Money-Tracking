using MoneyTracking.Data.Entities;
using MoneyTracking.Data.UnitOfWork;
using MoneyTracking.Web.Models.UserModels;
using MoneyTracking.Web.Services.UserServ;
using Moq;

namespace MoneyTracking.Tests
{
    [TestClass]
    public class TestUserService
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly IUserService _userService;

        public TestUserService()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _userService = new UserService(_mockUnitOfWork.Object);
        }

        [TestInitialize]
        public void Setup()
        {
            _mockUnitOfWork.Setup(x => x.Users.GetById(TestData.userId)).ReturnsAsync(TestData.user);
            _mockUnitOfWork.Setup(x => x.Users.GetAll()).ReturnsAsync(TestData.GetListUsers());
        }

        [TestMethod]
        public async Task Test_GetUserById_WhenUserExist()
        {
            // Arrange

            // Act
            var result = await _userService.GetUserById(TestData.userId);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(User));
            Assert.AreEqual(TestData.user.Login, result.Login);
        }

        [TestMethod]
        public async Task Test_GetUserById_WhenUserNotExist()
        {
            // Arrange
            _mockUnitOfWork.Setup(x => x.Users.GetById(It.IsAny<int>())).ReturnsAsync(() => null);

            // Act
            var result = await _userService.GetUserById(TestData.userId);

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public async Task Test_UpdateUser_WhenUserExist()
        {
            // Arrange

            // Act
            var result = _userService.UpdateUser(TestData.userUpdate);
            var resultUser = await _userService.GetUserById(TestData.userId);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(resultUser);
            Assert.IsTrue(result.IsCompletedSuccessfully);
            Assert.IsNull(result.Exception);
            Assert.AreEqual(TestData.userUpdate.NewEmail, resultUser.Email);
        }

        [TestMethod]
        public void Test_UpdateUser_WhenUserNotExist()
        {
            // Arrange
            _mockUnitOfWork.Setup(x => x.Users.GetById(It.IsAny<int>())).ReturnsAsync(() => null);

            // Act
            var result = _userService.UpdateUser(TestData.userUpdate);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsCompletedSuccessfully);
            Assert.IsNotNull(result.Exception);
        }

        [TestMethod]
        public async Task Test_GetAllUsers_WhenUsersExist()
        {
            // Arrange

            // Act
            var result = await _userService.GetAllUsers();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());
            Assert.IsInstanceOfType(result, typeof(IEnumerable<User>));
        }

        [TestMethod]
        public async Task Test_GetAllUsers_WhenUsersNotExist()
        {
            // Arrange
            _mockUnitOfWork.Setup(x => x.Users.GetAll()).ReturnsAsync(TestData.GetListUsersEmpty());

            // Act
            var result = await _userService.GetAllUsers();

            // Assert
            Assert.AreEqual(0, result.Count());
        }

        [TestMethod]
        public void Test_CreateUser()
        {
            // Arrange

            // Act
            var result = _userService.CreateUser(TestData.userAdd);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.IsCompletedSuccessfully);
            Assert.IsNull(result.Exception);
        }

        [TestMethod]
        public void Test_DeleteUser_WhenUserExist()
        {
            // Arrange

            // Act
            var result = _userService.DeleteUser(TestData.userId);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.IsCompletedSuccessfully);
            Assert.IsNull(result.Exception);
        }

        [TestMethod]
        public void Test_DeleteUser_WhenUserNotExist()
        {
            // Arrange
            _mockUnitOfWork.Setup(x => x.Users.GetById(It.IsAny<int>())).ReturnsAsync(() => null);

            // Act
            var result = _userService.DeleteUser(TestData.userId);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsCompletedSuccessfully);
            Assert.IsNotNull(result.Exception);
        }
    }
}