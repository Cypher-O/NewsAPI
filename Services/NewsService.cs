// Services/NewsService.cs
using NewsAPI.Models;
using NewsAPI.Repositories;

namespace NewsAPI.Services
{
    public class NewsService : INewsService
    {
        private readonly INewsRepository _newsRepository;

        public NewsService(INewsRepository newsRepository)
        {
            _newsRepository = newsRepository;
        }

        public async Task<NewsArticle> GetArticleByIdAsync(int id)
        {
            return await _newsRepository.GetByIdAsync(id);
        }

        public async Task<NewsResponse> GetArticlesAsync(int page, int pageSize)
        {
            return await _newsRepository.GetAllAsync(page, pageSize);
        }

        public async Task<NewsArticle> CreateArticleAsync(NewsArticle article)
        {
            article.PublishedDate = DateTime.UtcNow;
            return await _newsRepository.CreateAsync(article);
        }

        public async Task<NewsArticle> UpdateArticleAsync(int id, NewsArticle article)
        {
            var existingArticle = await _newsRepository.GetByIdAsync(id);
            if (existingArticle == null)
            {
                return null;
            }

            existingArticle.Title = article.Title;
            existingArticle.Content = article.Content;
            existingArticle.Author = article.Author;
            existingArticle.Category = article.Category;

            return await _newsRepository.UpdateAsync(existingArticle);
        }

        public async Task DeleteArticleAsync(int id)
        {
            await _newsRepository.DeleteAsync(id);
        }
    }
}