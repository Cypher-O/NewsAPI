// Data/NewsDbContext.cs
using Microsoft.EntityFrameworkCore;
using NewsAPI.Models;

namespace NewsAPI.Data
{
    public class NewsDbContext : DbContext
    {
        public NewsDbContext(DbContextOptions<NewsDbContext> options) : base(options) { }

        public DbSet<NewsArticle> NewsArticles { get; set; }
    }
}