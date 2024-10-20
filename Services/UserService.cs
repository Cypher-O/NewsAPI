// Create Services/UserService.cs
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using NewsAPI.Models;
using NewsAPI.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace NewsAPI.Services
{
    public class UserService : IUserService
    {
        private readonly NewsDbContext _context;
        private readonly IConfiguration _configuration;

        public UserService(NewsDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<User> AuthenticateAsync(string username, string password)
        {
            var user = await _context.Users.SingleOrDefaultAsync(x => x.Username == username);

            // Validate
            if (user == null || !VerifyPasswordHash(password, user.PasswordHash))
                return null;

            // Authentication successful
            return user;
        }

        public async Task<User> GetByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        private bool VerifyPasswordHash(string password, string storedHash)
        {
            // In a real application, use a proper password hashing library
            return storedHash == ComputeHash(password);
        }

        private string ComputeHash(string password)
        {
            // In a real application, use a proper password hashing library
            using (var sha256 = System.Security.Cryptography.SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(hashedBytes);
            }
        }
    }
}