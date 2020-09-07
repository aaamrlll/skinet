using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using Core.Entities;
using Core.Interfaces;
using Core.Specification;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;


namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {

        private readonly IGenericRepo<Product> _productRepo;
        private readonly IGenericRepo<ProductType> _productTypeRepo;
        private readonly IGenericRepo<ProductBrand> _productBrandRepo;
        private readonly IMapper _mapper;

        public ProductsController(IGenericRepo<Product> productRepo, IGenericRepo<ProductType> productTypeRepo,
            IGenericRepo<ProductBrand> productBrandRepo, IMapper mapper)
        {
            _mapper = mapper;
            _productBrandRepo = productBrandRepo;
            _productTypeRepo = productTypeRepo;
            _productRepo = productRepo;

        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<ProductToReturnDto>>> GetProducts()
        {
            var spec = new ProductsWithTypesAndBrandsSpecification();
            var Fullproducts = await _productRepo.GetEntityListWithSpecAsync(spec);

            return Ok(_mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(Fullproducts));
        }

        [HttpGet("brands")]
        public async Task<ActionResult<List<ProductBrand>>> GetProductBrands() => Ok(await _productBrandRepo.GetAllAsync());

        [HttpGet("types")]
        public async Task<ActionResult<List<ProductType>>> GetProductTypes() => Ok(await _productTypeRepo.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductToReturnDto>> GetProduct(int Id)
        {
            var spec = new ProductsWithTypesAndBrandsSpecification(Id);
            var Fullproduct = await _productRepo.GetEntityWithSpecAsync(spec);

            return Ok(_mapper.Map<Product,ProductToReturnDto>(Fullproduct));
        }

    }
}