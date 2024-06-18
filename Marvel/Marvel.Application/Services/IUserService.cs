using Marvel.Application.DTO;

namespace Marvel.Application.Services
{
    public interface IUserService
    {
        Task<UserDTO> RegisterAsync(UserDTO user);
        Task<bool> Login(string? nickname, string? email, string password);
        Task<LoginDTO> GetUserByEmailAsync(string email);
    }
}
