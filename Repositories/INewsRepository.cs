// Repositories/INewsRepository.cs
using NewsAPI.Models;

namespace NewsAPI.Repositories
{
    public interface INewsRepository
    {
        Task<NewsArticle> GetByIdAsync(int id);
        Task<NewsResponse> GetAllAsync(int page, int pageSize);
        Task<NewsArticle> CreateAsync(NewsArticle article);
        Task<NewsArticle> UpdateAsync(NewsArticle article);
        Task DeleteAsync(int id);
    }
}