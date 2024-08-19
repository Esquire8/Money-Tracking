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
            _mockUnitOfWork.Setup(x => x.Users.Add(It.IsAny<User>())).Returns(Task.CompletedTask);
            _mockUnitOfWork.Setup(x => x.Users.GetById(It.IsAny<int>())).ReturnsAsync(user);
            _mockUnitOfWork.Setup(x => x.Users.GetAll()).ReturnsAsync(GetListUsers());
            _mockUnitOfWork.Setup(x => x.Users.Add(It.IsAny<User>()));
            _mockUnitOfWork.Setup(x => x.Users.Delete(It.IsAny<User>()));
        }

        [TestMethod]
        public async Task Test_GetUserById_WhenUserExist()
        {
            // Arrange

            // Act
            var result = await _userService.GetUserById(userId);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(User));
            Assert.AreEqual(user.Login, result.Login);
        }

        [TestMethod]
        public async Task Test_GetUserById_WhenUserNotExist()
        {
            // Arrange
            _mockUnitOfWork.Setup(x => x.Users.GetById(It.IsAny<int>())).ReturnsAsync(() => null);

            // Act
            var result = await _userService.GetUserById(userId);

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void Test_UpdateUser_WhenUserExist()
        {
            // Arrange
            string newLogin = "Sokoloff4ik";
            var updateUser = new UserUpdate(Id: userId, NewEmail: user.Email, NewLogin: newLogin, NewPassword: user.Password);

            // Act
            var result = _userService.UpdateUser(updateUser);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.IsCompletedSuccessfully);
            Assert.IsNull(result.Exception);
        }

        [TestMethod]
        public void Test_UpdateUser_WhenUserNotExist()
        {
            // Arrange
            var updateUser = new UserUpdate(Id: 1, NewEmail: "test", NewLogin: "test", NewPassword: "test");

            _mockUnitOfWork.Setup(x => x.Users.GetById(It.IsAny<int>())).ReturnsAsync(() => null);

            // Act
            var result = _userService.UpdateUser(updateUser);
            //var result = await _userService.GetUserById(userId);

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
            _mockUnitOfWork.Setup(x => x.Users.GetAll()).ReturnsAsync(GetListUsersEmpty());

            // Act
            var result = await _userService.GetAllUsers();

            // Assert
            Assert.AreEqual(0, result.Count());
        }

        [TestMethod]
        public void Test_CreateUser()
        {
            // Arrange
            var userModel = new UserAdd(Email: user.Email, Login: user.Login, Password: user.Password);

            // Act
            var result = _userService.CreateUser(userModel);

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
            var result = _userService.DeleteUser(userId);

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
            var result = _userService.DeleteUser(userId);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsCompletedSuccessfully);
            Assert.IsNotNull(result.Exception);
        }

        // MockData for User
        private static List<User> GetListUsers()
        {
            var regDate = DateTime.UtcNow;

            var users = new List<User>
            {
                new User { Id = 1, Email = "efim_sokoloff@mail.ru", Login = "Sokol", Password = "efim_sokol123", RegistrationDate = regDate },
                new User { Id = 2, Email = "mishka228@mail.ru", Login = "Sokol", Password = "efim_sokol123", RegistrationDate = regDate }
            };

            return users;
        }

        private static List<User> GetListUsersEmpty()
        {
            var regDate = DateTime.UtcNow;

            var users = new List<User>
            {
            };

            return users;
        }

        private static readonly int userId = 1;

        private static readonly User user = new User
        {
            Id = userId,
            Email = "efim_sokoloff@mail.ru",
            Login = "Sokol",
            Password = "efim_sokol123",
        };
    }
}