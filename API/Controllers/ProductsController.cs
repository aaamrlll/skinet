using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using Core.Entities;
using Core.Interfaces;
using Core.Specification;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using API.Errors;
using Microsoft.AspNetCore.Http;
using API.Helpers;

namespace API.Controllers
{
    public class ProductsController : BaseApiController
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
        public async Task<ActionResult<Pagination<ProductToReturnDto>>> GetProducts([FromQuery]ProductsSpecParams prodParams)
        {
            var spec = new ProductsWithTypesAndBrandsSpecification(prodParams);
            var countSpect = new ProductsWithFiltersForCountSpecification(prodParams);

            var totalItems = await _productRepo.ContAsync(countSpect);

            var Fullproducts = await _productRepo.GetEntityListWithSpecAsync(spec);

           var data =_mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(Fullproducts);

           return Ok(new Pagination<ProductToReturnDto>{
               Count = totalItems,
               Data = data,
               PageIndex = prodParams.PageIndex,
               PageSize = prodParams.PageSize
           });
        }

        [HttpGet("brands")]
        public async Task<ActionResult<List<ProductBrand>>> GetProductBrands() => Ok(await _productBrandRepo.GetAllAsync());

        [HttpGet("types")]
        public async Task<ActionResult<List<ProductType>>> GetProductTypes() => Ok(await _productTypeRepo.GetAllAsync());

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse),StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
        {
            var spec = new ProductsWithTypesAndBrandsSpecification(id);
            var Fullproduct = await _productRepo.GetEntityWithSpecAsync(spec);

            if(Fullproduct == null) return NotFound(new ApiResponse(404));
           

            return Ok(_mapper.Map<Product,ProductToReturnDto>(Fullproduct));
        }

    }
}