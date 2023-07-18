namespace Challenge.Models.DTOs
{
    public class ProductDTO
    {
        public string ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal UnitPrice { get; set; }
        public string CategoryId { get; set; }
    }
}
