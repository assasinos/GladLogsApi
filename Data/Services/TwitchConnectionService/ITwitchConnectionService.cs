namespace GladLogsApi.Data.Services.TwitchConnectionService
{
    public interface ITwitchConnectionService
    {
        bool Start();
        bool Stop();
        bool Restart();
    }
}
