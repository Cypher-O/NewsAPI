// Models/NewsArticle.cs
using System.ComponentModel.DataAnnotations;

namespace NewsAPI.Models
{
    public class NewsArticle
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public string Author { get; set; }

        public DateTime PublishedDate { get; set; }

        [MaxLength(100)]
        public string Category { get; set; }
    }
}