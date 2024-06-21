using Marvel.Application.Repositories;
using Marvel.Domain.Entities;

namespace Marvel.Infrastruture.Repositories
{
    public class UserRepository : IUserRepository
    {
        public Task<User> GetByEmailAsync(string email)
        {
            throw new NotImplementedException();
        }
    }
}
