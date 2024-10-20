// Services/INewsService.cs
using NewsAPI.Models;

namespace NewsAPI.Services
{
    public interface INewsService
    {
        Task<NewsArticle> GetArticleByIdAsync(int id);
        Task<NewsResponse> GetArticlesAsync(int page, int pageSize);
        Task<NewsArticle> CreateArticleAsync(NewsArticle article);
        Task<NewsArticle> UpdateArticleAsync(int id, NewsArticle article);
        Task DeleteArticleAsync(int id);
    }
}