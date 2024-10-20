// Controllers/NewsController.cs
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NewsAPI.Models;
using NewsAPI.Services;
using NewsAPI.DTOs;

namespace NewsAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class NewsController : ControllerBase
    {
        private readonly INewsService _newsService;

        public NewsController(INewsService newsService)
        {
            _newsService = newsService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<NewsArticleDto>> GetArticle(int id)
        {
            var article = await _newsService.GetArticleByIdAsync(id);
            if (article == null)
            {
                return NotFound();
            }
            return Ok(article);
        }

        [HttpGet]
        public async Task<ActionResult<NewsResponseDto>> GetArticles([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var response = await _newsService.GetArticlesAsync(page, pageSize);
            return Ok(response);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<NewsArticleDto>> CreateArticle([FromBody] CreateNewsArticleDto articleDto)
        {
            var createdArticle = await _newsService.CreateArticleAsync(articleDto);
            return CreatedAtAction(nameof(GetArticle), new { id = createdArticle.Id }, createdArticle);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<ActionResult<NewsArticleDto>> UpdateArticle(int id, [FromBody] UpdateNewsArticleDto articleDto)
        {
            var updatedArticle = await _newsService.UpdateArticleAsync(id, articleDto);
            if (updatedArticle == null)
            {
                return NotFound();
            }
            return Ok(updatedArticle);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteArticle(int id)
        {
            await _newsService.DeleteArticleAsync(id);
            return NoContent();
        }
    }
}