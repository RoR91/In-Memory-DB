using Challenge.Models;
using Challenge.Models.DTOs;

namespace Challenge.Data
{
    public interface IProductRepository
    {
        Task<IEnumerable<ProductDTO?>> GetProducts();
        Task<ProductDTO?> GetProductById(string productId);
        Task<ProductDTO?> AddProduct(AddProductRequest product);
        Task<ProductDTO?> UpdateProduct(ProductRequest product);
        Task<bool> ProductExists(string productId);
        void DeleteProduct(string productId);
    }
}
