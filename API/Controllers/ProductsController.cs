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
        private readonly IGenericRepository<Product> _productsRepo;

        private readonly IGenericRepository<ProductBrand> _brandRepo;

        private readonly IGenericRepository<ProductType> _productTypeRepo;

        public ProductsController(IGenericRepository<Product> productsRepo, IGenericRepository<ProductBrand> brandRepo
            ,IGenericRepository<ProductType> productTypeRepo)
        {
            _productsRepo= productsRepo;
            _brandRepo= brandRepo;
            _productTypeRepo= productTypeRepo;
        }

        [HttpGet("Products")]
        public async Task<ActionResult<List<Product>>> GetProducts()
        {
            var products = await _productsRepo.GetAllAsync();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProducts(int id)
        {
            return await _productsRepo.GetByIdAsync(id);
        }

        [HttpGet("Brands")]
        public async Task<ActionResult<List<ProductBrand>>> GetProductBrandsAsync()
        {
            return Ok(await _brandRepo.GetAllAsync());
        }

        [HttpGet("Types")]
        public async Task<ActionResult<List<ProductType>>> GetProductTypesAsync()
        {
            return Ok(await _productTypeRepo.GetAllAsync());
        }
    }
}