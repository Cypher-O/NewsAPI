// Create Models/User.cs
using System.ComponentModel.DataAnnotations;

namespace NewsAPI.Models
{
    public class User
    {
        public int Id { get; set; }
        
        [Required]
        public string Username { get; set; }
        
        [Required]
        public string PasswordHash { get; set; }
        
        public string Role { get; set; }
    }
}