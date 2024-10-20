// Data/DatabaseSeeder.cs
using Microsoft.AspNetCore.Identity;
using NewsAPI.Models;

namespace NewsAPI.Data
{
    public class DatabaseSeeder
    {
        private readonly NewsDbContext _context;

        public DatabaseSeeder(NewsDbContext context)
        {
            _context = context;
        }

        public async Task SeedAsync()
        {
            if (!_context.Users.Any())
            {
                var adminUser = new User
                {
                    Username = "admin",
                    PasswordHash = HashPassword("admin123"),  // In production, use a secure password
                    Role = "Admin"
                };

                var regularUser = new User
                {
                    Username = "user",
                    PasswordHash = HashPassword("user123"),  // In production, use a secure password
                    Role = "User"
                };

                _context.Users.AddRange(adminUser, regularUser);
            }

            if (!_context.NewsArticles.Any())
            {
                var articles = new List<NewsArticle>
                {
                    new NewsArticle
                    {
                        Title = "First News Article",
                        Content = "This is the content of the first news article.",
                        Author = "John Doe",
                        PublishedDate = DateTime.UtcNow.AddDays(-1),
                        Category = "General"
                    },
                    new NewsArticle
                    {
                        Title = "Second News Article",
                        Content = "This is the content of the second news article.",
                        Author = "Jane Smith",
                        PublishedDate = DateTime.UtcNow,
                        Category = "Technology"
                    }
                };

                _context.NewsArticles.AddRange(articles);
            }

            await _context.SaveChangesAsync();
        }

        private string HashPassword(string password)
        {
            // In a real application, use a proper password hashing library
            return Convert.ToBase64String(System.Security.Cryptography.SHA256.Create().ComputeHash(System.Text.Encoding.UTF8.GetBytes(password)));
        }
    }
}
