using GladLogsApi.Attributes;
using GladLogsApi.Data.Services.TwitchConnectionService;
using Microsoft.AspNetCore.Mvc;

namespace GladLogsApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ServiceController : ControllerBase
    {
        private readonly ITwitchConnectionService _twitchConnectionService;
        private readonly ILogger<ServiceController> _logger;

        public ServiceController(ITwitchConnectionService twitchConnectionService, ILogger<ServiceController> logger)
        {
            _twitchConnectionService = twitchConnectionService;
            _logger = logger;
        }

        /// <summary>
        /// Starts the service.
        /// </summary>
        // POST /service/start
        [HttpPost("start")]
        [ServiceFilter(typeof(ValidateAuthKeyAttribute))]
        public IActionResult StartService()
        {
            if (!_twitchConnectionService.Start()) return BadRequest("Tried to start the service before it was stopped");
            return Ok();
        }

        /// <summary>
        /// Stops the service.
        /// </summary>
        // POST /service/stop
        [HttpPost("stop")]
        [ServiceFilter(typeof(ValidateAuthKeyAttribute))]
        public IActionResult StopService()
        {
            if (!_twitchConnectionService.Stop()) return BadRequest("Tried to stop the service before it was started");
            return Ok();
        }

        /// <summary>
        /// Restarts the service.
        /// </summary>
        // POST /service/restart
        [HttpPost("restart")]
        [ServiceFilter(typeof(ValidateAuthKeyAttribute))]
        public IActionResult RestartService()
        {
            if (!_twitchConnectionService.Restart()) return BadRequest("Service couldn't be restarted");
            return Ok();
        }
    }
}
