// DTOs/CreateNewsArticleDto.cs
using System.ComponentModel.DataAnnotations;

namespace NewsAPI.DTOs
{
    public class CreateNewsArticleDto
    {
        [Required]
        [MaxLength(200)]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public string Author { get; set; }

        [MaxLength(100)]
        public string Category { get; set; }
    }
}