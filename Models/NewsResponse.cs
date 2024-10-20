// Models/NewsResponse.cs
namespace NewsAPI.Models
{
    public class NewsResponse
    {
        public IEnumerable<NewsArticle> Articles { get; set; }
        public int TotalCount { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}