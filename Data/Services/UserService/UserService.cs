using GladLogsApi.Data.Repositories.CrudRepository;
using GladLogsApi.Models.Dtos;
using GladLogsApi.Models.Entities;

namespace GladLogsApi.Data.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly ICrudRepository<string, User, UserDto, CreateUserDto> _userCrudRepository;
        private readonly ILogger<UserService> _logger;

        public UserService(ICrudRepository<string, User, UserDto, CreateUserDto> userCrudRepository, ILogger<UserService> logger)
        {
            _userCrudRepository = userCrudRepository;
            _logger = logger;
        }

        public UserDto? CreateUser(CreateUserDto createUserDto)
        {
            try
            {
                _logger.LogInformation("Creating user with username {Username}", createUserDto.Id);

                var user = _userCrudRepository.Create(createUserDto);

                return user;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating user with username {Username}", createUserDto.Id);
                return null;
            }
        }

        public Task<UserDto>? CreateUserAsync(CreateUserDto createUserDto)
        {
            try
            {
                _logger.LogInformation("Creating user with username {Username}", createUserDto.Id);

                var user = _userCrudRepository.CreateAsync(createUserDto);

                return user;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating user with username {Username}", createUserDto.Id);
                return null;
            }
        }

        public bool DeleteUser(string UserId)
        {
            try
            {
                _logger.LogInformation("Deleting user with id {UserId}", UserId);

                _userCrudRepository.Delete(UserId);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting user with id {UserId}", UserId);
                return false;
            }
        }

        public IEnumerable<UserDto>? GetAllUsers()
        {
            try
            {
                _logger.LogInformation("Getting all users");

                var users = _userCrudRepository.GetAll();

                return users;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting all users");
                return null;
            }
        }

        public UserDto? GetUserByUsername(string username)
        {
            throw new NotImplementedException();
        }

        public Task<UserDto?> GetUserByUsernameAsync(string username)
        {
            throw new NotImplementedException();
        }
    }
}
