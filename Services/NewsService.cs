// Services/NewsService.cs
using NewsAPI.Models;
using NewsAPI.Repositories;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Caching.Memory;
using NewsAPI.DTOs;
using AutoMapper;

namespace NewsAPI.Services
{
    public class NewsService : INewsService
    {
        private readonly INewsRepository _newsRepository;
        private readonly ILogger<NewsService> _logger;
        private readonly IMemoryCache _cache;
        private readonly IMapper _mapper;

        public NewsService(INewsRepository newsRepository, ILogger<NewsService> logger, IMemoryCache cache, IMapper mapper)
        {
            _newsRepository = newsRepository;
            _logger = logger;
            _cache = cache;
            _mapper = mapper;
        }

        public async Task<NewsArticleDto> GetArticleByIdAsync(int id)
        {
            // _logger.LogInformation("Getting article with id: {ArticleId}", id);
            // return await _newsRepository.GetByIdAsync(id);
            string cacheKey = $"article-{id}";
            if (!_cache.TryGetValue(cacheKey, out NewsArticleDto articleDto))
            {
                _logger.LogInformation("Cache miss for article id: {ArticleId}", id);
                var article = await _newsRepository.GetByIdAsync(id);
                articleDto = _mapper.Map<NewsArticleDto>(article);
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(5));
                _cache.Set(cacheKey, articleDto, cacheEntryOptions);
            }
            else
            {
                _logger.LogInformation("Cache hit for article id: {ArticleId}", id);
            }
            return articleDto;
        }

        public async Task<NewsResponseDto> GetArticlesAsync(int page, int pageSize)
        {
            // _logger.LogInformation("Getting articles. Page: {Page}, PageSize: {PageSize}", page, pageSize);
            // return await _newsRepository.GetAllAsync(page, pageSize);
            string cacheKey = $"articles-{page}-{pageSize}";
            if (!_cache.TryGetValue(cacheKey, out NewsResponseDto responseDto))
            {
                _logger.LogInformation("Cache miss for articles page: {Page}", page);
                var response = await _newsRepository.GetAllAsync(page, pageSize);
                responseDto = _mapper.Map<NewsResponseDto>(response);
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(5));
                _cache.Set(cacheKey, responseDto, cacheEntryOptions);
            }
            else
            {
                _logger.LogInformation("Cache hit for articles page: {Page}", page);
            }
            return responseDto;
        }

        public async Task<NewsArticleDto> CreateArticleAsync(CreateNewsArticleDto articleDto)
        {
            // article.PublishedDate = DateTime.UtcNow;
            // _logger.LogInformation("Creating article with title: {ArticleTitle}", article.Title);
            // return await _newsRepository.CreateAsync(article);
             var article = _mapper.Map<NewsArticle>(articleDto);
            article.PublishedDate = DateTime.UtcNow;

            _logger.LogInformation("Creating article with title: {ArticleTitle}", article.Title);
            var createdArticle = await _newsRepository.CreateAsync(article);

            // Cache the new article
            var cacheEntryOptions = new MemoryCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromMinutes(5));
            _cache.Set($"article-{createdArticle.Id}", createdArticle, cacheEntryOptions);

            // Clear the cache for articles to ensure fresh data for the next fetch
            _cache.Remove($"articles-1-10"); // Adjust page and pageSize as needed

            return _mapper.Map<NewsArticleDto>(createdArticle);
        }

        public async Task<NewsArticleDto> UpdateArticleAsync(int id, UpdateNewsArticleDto articleDto)
        {
            _logger.LogInformation("Updating article with id: {ArticleId}", id);
            var existingArticle = await _newsRepository.GetByIdAsync(id);
            if (existingArticle == null)
            {
                _logger.LogWarning("Article with id: {ArticleId} not found.", id);
                return null;
            }

            _mapper.Map(articleDto, existingArticle);
            var updatedArticle = await _newsRepository.UpdateAsync(existingArticle);

            // Update the cache
            var cacheEntryOptions = new MemoryCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromMinutes(5));
            _cache.Set($"article-{updatedArticle.Id}", updatedArticle, cacheEntryOptions);

            // Clear the cache for articles
            _cache.Remove($"articles-1-10"); // Adjust page and pageSize as needed

            _logger.LogInformation("Article with id: {ArticleId} updated successfully.", id);
            
            return _mapper.Map<NewsArticleDto>(updatedArticle);

            // _logger.LogInformation("Updating article with id: {ArticleId}", id);
            // var existingArticle = await _newsRepository.GetByIdAsync(id);
            // if (existingArticle == null)
            // {
            //     _logger.LogWarning("Article with id: {ArticleId} not found.", id);
            //     return null;
            // }

            // existingArticle.Title = article.Title;
            // existingArticle.Content = article.Content;
            // existingArticle.Author = article.Author;
            // existingArticle.Category = article.Category;

            // _logger.LogInformation("Article with id: {ArticleId} updated successfully.", id);
            // return await _newsRepository.UpdateAsync(existingArticle);
        }

        public async Task DeleteArticleAsync(int id)
        {
            _logger.LogInformation("Deleting article with id: {ArticleId}", id);
            await _newsRepository.DeleteAsync(id);

            // Remove the article from the cache
            _cache.Remove($"article-{id}");

            // Clear the cache for articles
            _cache.Remove($"articles-1-10"); // Adjust page and pageSize as needed

            _logger.LogInformation("Article with id: {ArticleId} deleted successfully.", id);
            // _logger.LogInformation("Deleting article with id: {ArticleId}", id);
            // await _newsRepository.DeleteAsync(id);
            // _logger.LogInformation("Article with id: {ArticleId} deleted successfully.", id);
        }
    }
}