// Controllers/NewsController.cs
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NewsAPI.Models;
using NewsAPI.Services;

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
        public async Task<ActionResult<NewsArticle>> GetArticle(int id)
        {
            var article = await _newsService.GetArticleByIdAsync(id);
            if (article == null)
            {
                return NotFound();
            }
            return Ok(article);
        }

        [HttpGet]
        public async Task<ActionResult<NewsResponse>> GetArticles([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var response = await _newsService.GetArticlesAsync(page, pageSize);
            return Ok(response);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<NewsArticle>> CreateArticle([FromBody] NewsArticle article)
        {
            var createdArticle = await _newsService.CreateArticleAsync(article);
            return CreatedAtAction(nameof(GetArticle), new { id = createdArticle.Id }, createdArticle);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<ActionResult<NewsArticle>> UpdateArticle(int id, [FromBody] NewsArticle article)
        {
            var updatedArticle = await _newsService.UpdateArticleAsync(id, article);
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