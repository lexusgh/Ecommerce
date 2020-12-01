using System.Linq;
using Infrastructure.Data;
using Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Core.Interfaces;
using System.Threading.Tasks;
using System.Collections.Generic;
using Core.Specification;
using AutoMapper;
using API.Dtos;
using API.Errors;
using Microsoft.AspNetCore.Http;
using API.Helpers;

namespace API.Controllers
{
   
    public class ProductsController : BaseApiController
    {
        private readonly IGenericRepository<Product> _productRepo;
        private readonly IGenericRepository<ProductType> _productTypeRepo;
        private readonly IGenericRepository<ProductBrand> _productBrandRepo;
        private readonly IMapper _mapper;

        public ProductsController(IGenericRepository<Product> productRepo,
                                  IGenericRepository<ProductType> productTypeRepo,
                                  IGenericRepository<ProductBrand> productBrandRepo,
                                  IMapper mapper)
        {
            _productRepo = productRepo;
            _productTypeRepo = productTypeRepo;
            _productBrandRepo = productBrandRepo;
            _mapper = mapper;
        }

        /* private readonly IProductRepository _repo;
public ProductsController(IProductRepository repo)
{
    _repo = repo;

} */

        [HttpGet]
        public async Task<ActionResult<Pagination<ProductToReturnDto>>> GetProducts([FromQuery]ProductSpecParams productParams)
        {
            var spec = new ProductsWithTypesAndBrandsSpecification(productParams);
            var countSpec = new ProductWithFiltersForCountSpecification(productParams);
            var totalItems = await _productRepo.CountAsync(spec);
            var prod = await _productRepo.ListAsync(spec);
            var data = _mapper.Map<IReadOnlyList<Product>,IReadOnlyList<ProductToReturnDto>>(prod);

            return  Ok(new Pagination<ProductToReturnDto>(productParams.PageIndex,
            productParams.PageSize,totalItems,data));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse),StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
        {
            var spec = new ProductsWithTypesAndBrandsSpecification(id);
            var prod = await _productRepo.GetEntityWithSpec(spec);
            if( prod  == null)
                return NotFound(new ApiResponse(404));
             return _mapper.Map<Product,ProductToReturnDto>(prod);
        }

         [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetBrands()
        {
            var prod = await _productBrandRepo.ListAllAsync();
            return Ok(prod);
        }
         [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductTypes()
        {
            var prod = await _productTypeRepo.ListAllAsync();
            return Ok(prod);
        }
    }
}