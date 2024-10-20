// Services/INewsService.cs
using NewsAPI.Models;
using NewsAPI.DTOs;

namespace NewsAPI.Services
{
    public interface INewsService
    {
        Task<NewsArticleDto> GetArticleByIdAsync(int id);
        Task<NewsResponseDto> GetArticlesAsync(int page, int pageSize);
        Task<NewsArticleDto> CreateArticleAsync(CreateNewsArticleDto articleDto);
        Task<NewsArticleDto> UpdateArticleAsync(int id, UpdateNewsArticleDto articleDto);
        Task DeleteArticleAsync(int id);
        
        // Task<NewsArticle> GetArticleByIdAsync(int id);
        // Task<NewsResponse> GetArticlesAsync(int page, int pageSize);
        // Task<NewsArticle> CreateArticleAsync(NewsArticle article);
        // Task<NewsArticle> UpdateArticleAsync(int id, NewsArticle article);
        // Task DeleteArticleAsync(int id);
    }
}