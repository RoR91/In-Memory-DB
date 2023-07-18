using System.ComponentModel.DataAnnotations;

namespace Challenge.Models
{
    public class AddProductRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal UnitPrice { get; set; }
        public string CategoryId { get; set; }
    }
}
