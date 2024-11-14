using GladLogsApi.Data.Services.WeekService;
using GladLogsApi.Models.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace GladLogsApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeeksController : ControllerBase
    {

        private readonly IWeekService _weekService;

        public WeeksController(IWeekService weekService)
        {
            _weekService = weekService;
        }

        [HttpGet]
        public IActionResult GetUserActiveWeeks(string UserName, string ChatName)
        {
            var weeks = _weekService.GetUserActiveWeeksInChat(UserName, ChatName);  
            return Ok(weeks);
        }




    }
}
