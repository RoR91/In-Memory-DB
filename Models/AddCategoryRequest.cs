using System.ComponentModel.DataAnnotations;

namespace Challenge.Models
{
    public class AddCategoryRequest
    {
        [Required]
        public string CategoryName { get; set; }
    }
}
