// DTOs/NewsArticleDto.cs
namespace NewsAPI.DTOs
{
    public class NewsArticleDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Author { get; set; }
        public DateTime PublishedDate { get; set; }
        public string Category { get; set; }
    }
}
