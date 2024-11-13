using GladLogsApi.Data.Services.UserService;
using GladLogsApi.Models.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace GladLogsApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }



    }
}
