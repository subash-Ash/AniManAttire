using Infrastructure.Data;
using Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Core.Interfaces;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _repository;

        public ProductsController(IProductRepository repository)
        {
            _repository = repository;
        }
        [HttpGet("Products")]
        public async Task<ActionResult<List<Product>>> GetProducts()
        {
            var products = await _repository.GetProductsAsync();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProducts(int id)
        {
            return await _repository.GetProductByIdAsync(id);
        }

        [HttpGet("Brands")]
        public async Task<ActionResult<List<ProductBrand>>> GetProductBrandsAsync()
        {
            return Ok(await _repository.GetProductBrandsAsync());
        }

        [HttpGet("Types")]
        public async Task<ActionResult<List<ProductType>>> GetProductTypesAsync()
        {
            return Ok(await _repository.GetProductTypesAsync());
        }
    }
}