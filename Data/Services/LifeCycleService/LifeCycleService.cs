
using GladLogsApi.Data.Services.TwitchConnectionService;

namespace GladLogsApi.Data.Services.LifeCycleService
{
    public class LifeCycleService : IHostedLifecycleService
    {
        private ITwitchConnectionService _twitchConnectionService;

        public LifeCycleService(ITwitchConnectionService twitchConnectionService)
        {
            _twitchConnectionService = twitchConnectionService;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _twitchConnectionService.Start();
            return Task.CompletedTask;
        }

        public Task StartedAsync(CancellationToken cancellationToken) => Task.CompletedTask;

        public Task StartingAsync(CancellationToken cancellationToken) => Task.CompletedTask;

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _twitchConnectionService.Stop();
            return Task.CompletedTask;
        }

        public Task StoppedAsync(CancellationToken cancellationToken) => Task.CompletedTask;

        public Task StoppingAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}
