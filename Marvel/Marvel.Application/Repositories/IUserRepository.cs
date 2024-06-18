
using Marvel.Domain.Entities;

namespace Marvel.Application.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetByEmailAsync(string email);
    }
}
