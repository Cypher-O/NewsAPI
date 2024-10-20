// DTOs/UpdateNewsArticleDto.cs
using System.ComponentModel.DataAnnotations;
namespace NewsAPI.DTOs
{
    public class UpdateNewsArticleDto
    {
        [MaxLength(200)]
        public string Title { get; set; }

        public string Content { get; set; }

        public string Author { get; set; }

        [MaxLength(100)]
        public string Category { get; set; }
    }
}