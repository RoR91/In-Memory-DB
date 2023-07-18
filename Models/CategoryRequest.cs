using System.ComponentModel.DataAnnotations;

namespace Challenge.Models
{
    public class CategoryRequest
    {
        public string? CategoryId { get; set; }
        [Required]
        public string CategoryName { get; set; }
    }
}
