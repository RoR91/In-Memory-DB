using AutoMapper;
using Challenge.Data.Entities;
using Challenge.Models;
using Challenge.Models.DTOs;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace Challenge.Data.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly IDatabase _database;
        private readonly IMapper _mapper;

        public CategoryRepository(IConnectionMultiplexer connection, IMapper mapper)
        {
            _database = connection.GetDatabase();
            _mapper = mapper;
        }

        public async Task<IEnumerable<CategoryDTO?>> GetCategories()
        {
            var categories = await _database.HashGetAllAsync("categories");

            if (categories.Length > 0)
                return categories.Select(category => JsonConvert.DeserializeObject<CategoryDTO>(category.Value));
            else
                return null;
        }

        public async Task<CategoryDTO?> GetCategoryById(string categoryId)
        {
            var category = await _database.HashGetAsync("categories", categoryId);

            return category.HasValue ? JsonConvert.DeserializeObject<CategoryDTO>(category) : null;
        }

        public async Task<CategoryDTO?> AddCategory(AddCategoryRequest category)
        {
            if (category != null)
            {
                var entity = _mapper.Map<Category>(category);
                entity.CategoryId = $"category:{Guid.NewGuid().ToString()}";

                var json = JsonConvert.SerializeObject(entity);

                var result = await _database.HashSetAsync("categories", entity.CategoryId, json, When.NotExists);

                if (result)
                {
                    return _mapper.Map<CategoryDTO>(entity);
                }
            }

            return null;

        }

        public async Task<CategoryDTO?> UpdateCategory(CategoryRequest category)
        {
            if (category != null)
            {
                var entity = _mapper.Map<Category>(category);
                var json = JsonConvert.SerializeObject(entity);

                var result = await _database.HashSetAsync("categories", entity.CategoryId, json);

                if (!result)
                {
                    return _mapper.Map<CategoryDTO>(entity);
                }
            }

            return null;
        }

        public async Task<bool> CategoryExists(string categoryId)
        {
            return await _database.HashExistsAsync("categories", categoryId);
        }

        public void DeleteCategory(string categoryId)
        {
            _database.HashDelete("categories", categoryId);
        }
    }
}
