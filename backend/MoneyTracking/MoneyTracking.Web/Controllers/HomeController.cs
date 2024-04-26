using Microsoft.AspNetCore.Mvc;

namespace MoneyTracking.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {
        private readonly ILogger<HomeController> _logger;
        //private readonly IRepositoryBase<Income> _userRepository; 
        //или IUserRepository, где IUserRepository : IRepositoryBase<User>


        public HomeController(ILogger<HomeController> logger
            //,userRepository
            )
        {
            _logger = logger;
            //_userRepository = userRepository;
        }


        [HttpGet("hello-world")]
        public string HelloWorld()
        {
            return "HelloWorld";
        }

        /*
        [HttpGet("add-user")]
        public int AddUser()
        {
            var newUser = new User
            {
                Login = "Test",
                Email = "test@test.ru",
                Password = "qwqwe",
                RegistrationDate = DateTime.Now
            };

            var addedUser = _userRepository.Add(newUser);
            //через addedUser можно теперь получить Id добавленной сущности
            return addUser.Id;
        }
        */
    }
}
