using Challenge.Models;
using Challenge.Models.DTOs;

namespace Challenge.Data
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<CategoryDTO?>> GetCategories();
        Task<CategoryDTO?> GetCategoryById(string categoryId);
        Task<CategoryDTO?> AddCategory(AddCategoryRequest category);
        Task<CategoryDTO?> UpdateCategory(CategoryRequest category);
        Task<bool> CategoryExists(string categoryId);
        void DeleteCategory(string categoryId);
    }
}
