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
            _mockUnitOfWork = _mockUnitOfWork = new Mock<IUnitOfWork>();
            _userService = new UserService(_mockUnitOfWork.Object);
        }

        [TestInitialize]
        public void Setup()
        {
            _mockUnitOfWork.Setup(x => x.Users.Add(It.IsAny<User>())).Returns(Task.CompletedTask);
            _mockUnitOfWork.Setup(x => x.Users.GetById(It.IsAny<int>())).ReturnsAsync(user);
            _mockUnitOfWork.Setup(x => x.Users.GetAll()).ReturnsAsync(GetListUsers());
            _mockUnitOfWork.Setup(x => x.Users.Add(It.IsAny<User>()));
            _mockUnitOfWork.Setup(x => x.Users.Delete(It.IsAny<User>()));
        }

        [TestMethod]
        public async Task TestGetUserById()
        {
            // Arrange

            // Act
            var result = await _userService.GetUserById(userId);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(User));
            Assert.AreEqual("Sokol", result.Login);
        }

        [TestMethod]
        public async Task TestUpdateUser()
        {
            // Arrange
            string newLogin = "Sokoloff4ik";
            var updateUser = new UserUpdate(Id: userId, NewEmail: user.Email, NewLogin: newLogin, NewPassword: user.Password);

            // Act
            await _userService.UpdateUser(updateUser);
            var result = await _userService.GetUserById(userId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(updateUser.NewEmail, result.Email);
        }

        [TestMethod]
        public async Task TestGetAllUsers()
        {
            // Arrange

            // Act
            var result = await _userService.GetAllUsers();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());
        }

        [TestMethod]
        public void TestAddUser()
        {
            // Arrange
            var userModel = new UserAdd(Email: user.Email, Login: user.Login, Password: user.Password);

            // Act
            var result = _userService.CreateUser(userModel);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.IsCompletedSuccessfully);
            Assert.AreEqual(null, result.Exception);
        }

        [TestMethod]
        public void TestDeleteUser()
        {
            // Arrange

            // Act
            var result = _userService.DeleteUser(userId);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.IsCompletedSuccessfully);
            Assert.AreEqual(null, result.Exception);
        }

        // MockData for User
        private List<User> GetListUsers()
        {
            var regDate = DateTime.UtcNow;

            var users = new List<User>
            {
                new User { Id = 1, Email = "efim_sokoloff@mail.ru", Login = "Sokol", Password = "efim_sokol123", RegistrationDate = regDate },
                new User { Id = 2, Email = "mishka228@mail.ru", Login = "Sokol", Password = "efim_sokol123", RegistrationDate = regDate }
            };

            return users;
        }

        private static int userId = 1;

        private User user = new User
        {
            Id = userId,
            Email = "efim_sokoloff@mail.ru",
            Login = "Sokol",
            Password = "efim_sokol123",
        };
    }
}