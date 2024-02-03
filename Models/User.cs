using EsapApi.Enums;
using System.ComponentModel.DataAnnotations;

namespace EsapApi.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        public string Name { get; set; } = "";

        [Required]
        public string Email { get; set; } = ""; // Added for authentication

        [Required]
        public string PasswordHash { get; set; } = ""; // Store password securely

        public string Address { get; set; } = "";

        // Role property using UserRole enum
        [Required]
        public UserRole Role { get; set; }

    }
}
