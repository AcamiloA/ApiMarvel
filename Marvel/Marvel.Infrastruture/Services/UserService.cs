using Marvel.Application.DTO;
using Marvel.Application.Repositories;
using Marvel.Application.Services;
using Marvel.Domain.Entities;
using System.Security.Cryptography;
using System.Text;

namespace Marvel.Infrastruture.Services
{
    public class UserService(IRepository repository, IUserRepository userRepository) : IUserService
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IRepository _repository = repository;

        public async Task<UserDTO> RegisterAsync(UserDTO user)
        {
            await ValidateAsync(user);

            (string passwordHash, string salt) = CreatePasswordHash(user.PasswordHash!);

            User model = new()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                NickName = user.NickName,
                Email = user.Email,
                PasswordHash = passwordHash,
                Salt = salt,
                IsEnabled = true,
                CreationDate = DateTime.Now
            };

            await _repository.AddAsync(model);

            user.ConfirmEmail = null;
            user.PasswordHash = null;
            user.ConfirmPassword = null;

            return user;
        }

        public async Task<bool> Login(string? nickname, string? email, string password)
        {
            List<User> list = await _repository.GetAllAsync<User>();
            if (nickname != null && !list.Where(_ => _.NickName == nickname).Any())
                throw new InvalidDataException("El nickname ingresado no existe");
            else if (email != null && !list.Where(_ => _.Email == email).Any())
                throw new InvalidDataException("El coreo ingresado no existe");

            User user = list.Where(_ => _.NickName == nickname || _.Email == email).FirstOrDefault()!;

            if (VerifyPassword(password, user.PasswordHash, user.Salt))
            {
                return true;
            }

            throw new InvalidDataException("Contraseña incorrecta");
        }

        public static bool VerifyPassword(string password, string storedHash, string storedSalt)
        {
            byte[] saltedPasswordBytes = Encoding.UTF8.GetBytes(password + storedSalt);
            byte[] hashBytes = SHA256.HashData(saltedPasswordBytes);
            string computedHash = Convert.ToBase64String(hashBytes);
            return computedHash == storedHash;
        }

        public async Task<LoginDTO> GetUserByEmailAsync(string email)
        {
            throw new NotImplementedException();
        }
        private static (string passwordHash, string salt) CreatePasswordHash(string password)
        {
            byte[] saltBytes = new byte[16];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(saltBytes);
            }
            string salt = Convert.ToBase64String(saltBytes);
            byte[] saltedPasswordBytes = Encoding.UTF8.GetBytes(password + salt);
            byte[] hashBytes = SHA256.HashData(saltedPasswordBytes);
            string passwordHash = Convert.ToBase64String(hashBytes);
            return (passwordHash, salt);
        }        

        private async Task ValidateAsync(UserDTO user)
        {
            IEnumerable<User> list = await _repository.GetAllAsync<User>();
            if (list.Where(_ => _.Email == user.Email).Any())
                throw new InvalidDataException("El correo ingresado ya existe con otro usuario");
            if (list.Where(_ => _.NickName == user.NickName).Any())
                throw new InvalidDataException("El nickname ingresado ya existe con otro usuario");
            if (user.PasswordHash != user.ConfirmPassword)
                throw new InvalidDataException("La contraeña y la confirmación de la contraseña no coinciden");
            if (user.Email != user.ConfirmEmail)
                throw new InvalidDataException("El correo y la confirmación del correo no coinciden");
        }

        
    }
}
