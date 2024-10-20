// Create DTOs/NewsResponseDto.cs
namespace NewsAPI.DTOs
{
    public class NewsResponseDto
    {
        public IEnumerable<NewsArticleDto> Articles { get; set; }
        public int TotalCount { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}