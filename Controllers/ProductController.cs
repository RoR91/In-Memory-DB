using Challenge.Data;
using Challenge.Data.Entities;
using Challenge.Models;
using Challenge.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Challenge.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IProductRepository _repository;

        public ProductController(ILogger<ProductController> logger, IProductRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        // GET: api/<ProductController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> Get()
        {
            try
            {
                var response = await _repository.GetProducts();

                if (response != null)
                    return Ok(response);

                return NotFound();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // GET api/<ProductController>/5
        [HttpGet("{productId}")]
        public async Task<ActionResult<ProductDTO>> Get(string productId)
        {
            try
            {
                var response = await _repository.GetProductById(productId);

                if (response != null)
                    return Ok(response);

                return NotFound();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // POST api/<ProductController>
        [HttpPost]
        public async Task<ActionResult<ProductDTO>> Post([FromBody] AddProductRequest product)
        {
            try
            {
                var response = await _repository.AddProduct(product);

                if (response != null)
                    return Ok(response);

                return BadRequest();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // PUT api/<ProductController>/5
        [HttpPut("{productId}")]
        public async Task<ActionResult<ProductDTO>> Put(string productId, [FromBody] ProductRequest product)
        {
            try
            {
                var productExists = await _repository.ProductExists(productId);
                if (!productExists)
                    return NotFound();

                if (product.ProductId != productId)
                    return Conflict("Product Id mismatch.");

                var response = await _repository.UpdateProduct(product);

                if (response != null)
                    return Ok(response);

                return Conflict();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // DELETE api/<ProductController>/5
        [HttpDelete("{productId}")]
        public ActionResult<string> Delete(string productId)
        {
            try
            {
                _repository.DeleteProduct(productId);

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
