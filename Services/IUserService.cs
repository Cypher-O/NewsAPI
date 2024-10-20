// Create Services/IUserService.cs
using NewsAPI.Models;

namespace NewsAPI.Services
{
    public interface IUserService
    {
        Task<User> AuthenticateAsync(string username, string password);
        Task<User> GetByIdAsync(int id);
    }
}