using Marvel.Application.DTO;
using Marvel.Application.Security;

namespace Marvel.Application.Services
{
    public interface IUserService
    {
        Task<UserDTO> RegisterAsync(UserDTO user);
        Task<TokenDTO> Login(string? nickname, string? email, string password);
        Task<LoginDTO> GetUserByEmailAsync(string email);
    }
}
