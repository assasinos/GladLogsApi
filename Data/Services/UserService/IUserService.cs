using GladLogsApi.Models.Dtos;

namespace GladLogsApi.Data.Services.UserService
{
    public interface IUserService
    {
        UserDto? CreateUser(CreateUserDto createUserDto);
        Task<UserDto>? CreateUserAsync(CreateUserDto createUserDto);

        UserDto? GetUserByUsername(string username);

        Task<UserDto?> GetUserByUsernameAsync(string username);

        IEnumerable<UserDto>? GetAllUsers();

        bool DeleteUser(string UserId);
    }
}
