
using GladLogsApi.Configuration.ConfigTypes;
using GladLogsApi.Data.Services.ChatService;
using GladLogsApi.Data.Services.MessageService;
using GladLogsApi.Data.Services.UserService;
using GladLogsApi.Data.Services.WeekService;
using GladLogsApi.Extensions;
using GladLogsApi.Models.Dtos;
using Microsoft.Extensions.Options;
using TwitchLib.Client;
using TwitchLib.Client.Events;
using TwitchLib.Client.Models;
using TwitchLib.Communication.Clients;
using TwitchLib.Communication.Interfaces;
using TwitchLib.Communication.Models;

namespace GladLogsApi.Data.Services.TwitchConnectionService
{
    public class TwitchConnectionService : ITwitchConnectionService
    {

        private readonly TwitchAuthConfig _twitchAuthConfig;
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<TwitchConnectionService> _logger;

        private CancellationTokenSource? _cts;
        private Task? _currentTask;

        private static TwitchClient? client;

        public TwitchConnectionService(
            IOptions<TwitchAuthConfig> twitchAuthConfig,
            ILogger<TwitchConnectionService> logger, 
            IServiceProvider serviceProvider)
        {
            _twitchAuthConfig = twitchAuthConfig.Value;
            _logger = logger;
            _serviceProvider = serviceProvider;
        }

        private async Task RunTask(CancellationToken stoppingToken)
        {
            try
            {
                _logger.LogInformation("Starting message background service");
                var options = new ClientOptions()
                {
                    MessagesAllowedInPeriod = 10000,
                    ThrottlingPeriod = TimeSpan.FromSeconds(30)
                };

                var customClient = new WebSocketClient(options);
                client = new TwitchClient(customClient);

                var credentials = new ConnectionCredentials(_twitchAuthConfig.BotUsername, _twitchAuthConfig.OAuthToken);

                using var scope = _serviceProvider.CreateScope();
                var _chatService = scope.ServiceProvider.GetRequiredService<IChatService>();

                var channels = await _chatService.GetAllChatsAsync();

                if (channels is null || channels.Count < 1)
                {
                    _logger.LogError("No channels found to join.");
                    return;
                }

                client.Initialize(credentials, channels.Select(x => x.Id).ToList());

                client.OnMessageReceived += OnMessage;
                client.Connect();

                await Task.Delay(Timeout.Infinite, stoppingToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to start the message background service or something went wrong");
            }
            finally
            {
                if (client is not null && client.IsConnected)
                {
                    client.Disconnect();
                }
                _logger.LogInformation("Stopping message background service");
            }
        }

        private void OnMessage(object? sender, OnMessageReceivedArgs e)
        {
            try
            {
                using var scope = _serviceProvider.CreateScope();
                var _userService = scope.ServiceProvider.GetRequiredService<IUserService>();
                var user = GetOrCreateUser(e.ChatMessage.Username, _userService);

                if (user is null) return;

                var _weekService = scope.ServiceProvider.GetRequiredService<IWeekService>();
                var week = GetOrCreateWeek(_weekService);

                if (week is null) return;

                var message = new CreateMessageDto()
                {
                    ChatId = e.ChatMessage.Channel,
                    Content = e.ChatMessage.Message,
                    Timestamp = DateTime.UtcNow.ToIso8601String(),
                    UserId = user.Id,
                    WeekId = week.Id
                };

                var _messageService = scope.ServiceProvider.GetRequiredService<IMessageService>();
                _messageService.CreateMessage(message);
            }
            catch (InvalidOperationException)
            {
                throw; //If week creation fails, throw the exception to stop the service, as it will not work without a week
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to process message from user {user}", e.ChatMessage.Username);
            }
        }

        private UserDto? GetOrCreateUser(string username, IUserService userService)
        {
            var user = userService.GetUserByUsername(username);
            if (user is null)
            {
                _logger.LogInformation("User {user} not found in the database, creating new", username);
                var newUser = new CreateUserDto() { Id = username };
                user = userService.CreateUser(newUser);
                if (user is null)
                {
                    _logger.LogError("Failed to create user {user}", username);
                }
            }
            return user;
        }

        private WeekDto? GetOrCreateWeek(IWeekService weekService)
        {
            var week = weekService.GetCurrentWeek();
            if (week is null)
            {
                _logger.LogInformation("No week found, creating new");
                var newWeek = new CreateWeekDto()
                {
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now.AddDays(7)
                };
                week = weekService.CreateWeek(newWeek);
                if (week is null)
                {
                    _logger.LogError("Failed to create week");
                    throw new InvalidOperationException("Failed to create week");
                }
            }
            return week;
        }

        public bool Start()
        {
            if (_cts is not null)
            {
                _logger.LogWarning("Tried to start the service before it was stopped");
                return false;
            }
            _cts = new CancellationTokenSource();
            _currentTask = Task.Run(() => RunTask(_cts.Token),_cts.Token);
            _logger.LogInformation("Service started");
            return true;
        }

        public bool Stop()
        {
            if (_cts is null)
            {
                _logger.LogWarning("Tried to stop the service before it was started");
                return false;
            }
            _cts.Cancel();
            _cts.Dispose();
            _cts = null;
            _currentTask = null;
            _logger.LogInformation("Service stopped");
            return true;
        }

        public bool Restart()
        {
            if (!Stop())
            {
                return false;
            }
            return Start();
        }
    }
}
