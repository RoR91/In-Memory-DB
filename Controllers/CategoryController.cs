using Challenge.Data;
using Challenge.Data.Entities;
using Challenge.Models;
using Challenge.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using NuGet.Protocol.Core.Types;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Challenge.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ILogger<CategoryController> _logger;
        private readonly ICategoryRepository _repository;

        public CategoryController(ILogger<CategoryController> logger, ICategoryRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        // GET: api/<CategoryController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> Get()
        {
            try
            {
                var response = await _repository.GetCategories();

                if (response != null)
                    return Ok(response);

                return NotFound();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // GET api/<CategoryController>/5
        [HttpGet("{categoryId}")]
        public async Task<ActionResult<CategoryDTO>> Get(string categoryId)
        {
            try
            {
                var response = await _repository.GetCategoryById(categoryId);

                if (response != null)
                    return Ok(response);

                return NotFound();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // POST api/<CategoryController>
        [HttpPost]
        public async Task<ActionResult<CategoryDTO>> Post([FromBody] AddCategoryRequest addCategory)
        {
            try
            {
                var response = await _repository.AddCategory(addCategory);

                if (response != null)
                    return Ok(response);

                return BadRequest();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // PUT api/<CategoryController>/5
        [HttpPut("{categoryId}")]
        public async Task<ActionResult<CategoryDTO>> Put(string categoryId, [FromBody] CategoryRequest category)
        {
            try
            {
                var categoryExists = await _repository.CategoryExists(categoryId);
                if (!categoryExists)
                    return NotFound();

                if (category.CategoryId != categoryId)
                    return Conflict("Category Id mismatch.");

                var response = await _repository.UpdateCategory(category);

                if (response != null)
                    return Ok(response);

                return Conflict();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // DELETE api/<CategoryController>/5
        [HttpDelete("{categoryId}")]
        public ActionResult<string> Delete(string categoryId)
        {
            try
            {
                _repository.DeleteCategory(categoryId);

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
