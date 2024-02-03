using System.ComponentModel.DataAnnotations;

namespace EsapApi.Models
{
    public class Review
    {
        [Key]
        public int ReviewId { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; } // Navigation property

        public int UserId { get; set; }
        public User User { get; set; } // Navigation property

        [Range(1, 5)]
        public int Rating { get; set; }

        public string Comment { get; set; } = "";
    }
}
