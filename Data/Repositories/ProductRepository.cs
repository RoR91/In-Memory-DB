using AutoMapper;
using Challenge.Data.Entities;
using Challenge.Models;
using Challenge.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace Challenge.Data.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly IDatabase _database;
        private readonly IMapper _mapper;
        private readonly ICategoryRepository _categoryRepository;

        public ProductRepository(IConnectionMultiplexer connection, IMapper mapper, ICategoryRepository categoryRepository)
        {
            _database = connection.GetDatabase();
            _mapper = mapper;
            _categoryRepository = categoryRepository;
        }

        public async Task<IEnumerable<ProductDTO?>> GetProducts()
        {
            var products = await _database.HashGetAllAsync("products");

            if (products.Length > 0)
                return products.Select(product => JsonConvert.DeserializeObject<ProductDTO>(product.Value));
            else
                return null;
        }

        public async Task<ProductDTO?> GetProductById(string productId)
        {
            var product = await _database.HashGetAsync("products", productId);

            return product.HasValue ? JsonConvert.DeserializeObject<ProductDTO>(product) : null;
        }

        public async Task<ProductDTO?> AddProduct(AddProductRequest product)
        {
            if (product != null)
            {
                if (string.IsNullOrEmpty(product.Name) || string.IsNullOrEmpty(product.Description) || string.IsNullOrEmpty(product.CategoryId)
                    || !_categoryRepository.CategoryExists(product.CategoryId).Result)
                    return null;

                var entity = _mapper.Map<Product>(product);
                entity.ProductId = $"product:{Guid.NewGuid().ToString()}";

                var json = JsonConvert.SerializeObject(entity);

                var result = await _database.HashSetAsync("products", entity.ProductId, json, When.NotExists);

                if (result)
                {
                    return _mapper.Map<ProductDTO>(entity);
                }
            }

            return null;
        }

        public async Task<ProductDTO?> UpdateProduct(ProductRequest product)
        {
            if (product != null)
            {
                if (string.IsNullOrEmpty(product.Name) || string.IsNullOrEmpty(product.Description) || string.IsNullOrEmpty(product.CategoryId)
                    || !_categoryRepository.CategoryExists(product.CategoryId).Result)
                    return null;

                var entity = _mapper.Map<Product>(product);
                var json = JsonConvert.SerializeObject(entity);

                var result = await _database.HashSetAsync("products", entity.ProductId, json);

                if (!result)
                {
                    return _mapper.Map<ProductDTO>(entity);
                }
            }

            return null;
        }

        public async Task<bool> ProductExists(string productId)
        {
            return await _database.HashExistsAsync("products", productId);
        }

        public void DeleteProduct(string productId)
        {
            _database.HashDelete("products", productId);

        }

    }
}
