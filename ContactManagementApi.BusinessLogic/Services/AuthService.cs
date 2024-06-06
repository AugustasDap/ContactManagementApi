using ContactManagementApi.BusinessLogic.Interfaces;
using ContactManagementApi.Database.Context;
using ContactManagementApi.Database.Models;
using ContactManagementApi.Database.Repositories;
using Microsoft.EntityFrameworkCore;

using System.Text;


namespace ContactManagementApi.BusinessLogic.Services
{
    public class AuthService : IAuthService
    {
        private readonly ApplicationDbContext _context;
        private readonly IJwtService _jwtService;
        private readonly IUserRepository _userRepository;
        public AuthService(ApplicationDbContext context, IJwtService jwtService, IUserRepository userRepository)
        {
            _context = context;
            _jwtService = jwtService;
            _userRepository = userRepository;
        }

        public async Task<User> SignupNewUser(string userName, string password)
        {
            if (_context.Users.Any(a => a.Username == userName))
            {
                throw new Exception("Username already exists.");
            }
            CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);

            var user = new User
            {

                Username = userName,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Role = UserRole.Default


            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        public async Task<string> Login(string userName, string password)
        {
            if (string.IsNullOrWhiteSpace(userName) || string.IsNullOrWhiteSpace(password))
                return null;
            var user = await _context.Users.FirstOrDefaultAsync(a => a.Username == userName);
            if (user == null)
                return null;
            if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                return null;
            //generate jwt token
            var token = _jwtService.GenerateJwtToken(user);
            return token;
        }

        public async Task<bool> DeleteUserAsync(Guid id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            if (user == null)
            {
                return false;
            }
            await _userRepository.DeleteUserAsync(user);
            return true;
        }
        private bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");
            if (storedHash.Length != 64) throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordHash");
            if (storedSalt.Length != 128) throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordHashSalt");

            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }

            return true;
        }


    }

}
