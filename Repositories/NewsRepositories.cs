// Repositories/NewsRepository.cs
using Microsoft.EntityFrameworkCore;
using NewsAPI.Data;
using NewsAPI.Models;

namespace NewsAPI.Repositories
{
    public class NewsRepository : INewsRepository
    {
        private readonly NewsDbContext _context;

        public NewsRepository(NewsDbContext context)
        {
            _context = context;
        }

        public async Task<NewsArticle> GetByIdAsync(int id)
        {
            return await _context.NewsArticles.FindAsync(id);
        }

        public async Task<NewsResponse> GetAllAsync(int page, int pageSize)
        {
            var totalCount = await _context.NewsArticles.CountAsync();
            var articles = await _context.NewsArticles
                .OrderByDescending(a => a.PublishedDate)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new NewsResponse
            {
                Articles = articles,
                TotalCount = totalCount,
                Page = page,
                PageSize = pageSize
            };
        }

        public async Task<NewsArticle> CreateAsync(NewsArticle article)
        {
            _context.NewsArticles.Add(article);
            await _context.SaveChangesAsync();
            return article;
        }

        public async Task<NewsArticle> UpdateAsync(NewsArticle article)
        {
            _context.Entry(article).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return article;
        }

        public async Task DeleteAsync(int id)
        {
            var article = await _context.NewsArticles.FindAsync(id);
            if (article != null)
            {
                _context.NewsArticles.Remove(article);
                await _context.SaveChangesAsync();
            }
        }
    }
}