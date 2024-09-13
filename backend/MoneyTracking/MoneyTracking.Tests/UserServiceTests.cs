using MoneyTracking.Data.Entities;
using MoneyTracking.Data.UnitOfWork;
using MoneyTracking.Web.Models.UserModels;
using MoneyTracking.Web.Services.UserServ;
using Moq;

namespace MoneyTracking.Tests
{
    [TestClass]
    public class UserServiceTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly IUserService _userService;

        public UserServiceTests()
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
        public async Task GetUserById_WhenUserExist_SuccessfullyFound()
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
        public async Task GetUserById_WhenUserNotExist_NotFound()
        {
            // Arrange
            _mockUnitOfWork.Setup(x => x.Users.GetById(It.IsAny<int>())).ReturnsAsync(() => null);

            // Act
            var result = await _userService.GetUserById(TestData.userId);

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        [DataRow(1, "efim_sokoloff@mail.ru", "Sokoloff4ik", "efim_sokol123", true)]
        [DataRow(2, "efim_sokoloff@mail.ru", "Sokoloff4ik", "efim_sokol123", false)]
        public void TryUpdateUser(int userId, string email, string login, string password, bool expected)
        {
            // Arrange
            UserUpdate userUpdate = new UserUpdate(
                Id: userId,
                NewEmail: email,
                NewLogin: login,
                NewPassword: password);

            // Act
            var result = _userService.UpdateUser(userUpdate);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result.IsCompletedSuccessfully);
        }

        [TestMethod]
        public async Task GetAllUsers_WhenUsersExist_SuccessfullyFound()
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
        public async Task GetAllUsers_WhenUsersNotExist_NotFound()
        {
            // Arrange
            _mockUnitOfWork.Setup(x => x.Users.GetAll()).ReturnsAsync(TestData.GetListUsersEmpty());

            // Act
            var result = await _userService.GetAllUsers();

            // Assert
            Assert.AreEqual(0, result.Count());
        }

        [TestMethod]
        public void CreateUser_SuccessfullyCreated()
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
        [DataRow(1, true)]
        [DataRow(2, false)]
        public void TryDeleteUser(int userId, bool exptected)
        {
            // Arrange

            // Act
            var result = _userService.DeleteUser(userId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(exptected, result.IsCompletedSuccessfully);
        }
    }
}