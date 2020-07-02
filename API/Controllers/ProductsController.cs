using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepo _productRepo;

        public ProductsController(IProductRepo productRepo)
        {
            _productRepo = productRepo;


        }

        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetProducts() => Ok(await _productRepo.GetProductsAsync());
        [HttpGet("brands")]
        public async Task<ActionResult<List<ProductBrand>>> GetProductBrands() => Ok(await _productRepo.GetProductBrandsAsync());

        [HttpGet("types")]
        public async Task<ActionResult<List<ProductType>>> GetProductTypes() => Ok(await _productRepo.GetProductTypesAsync());

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int Id) => Ok(await _productRepo.GetProductAsync(Id));

    }
}