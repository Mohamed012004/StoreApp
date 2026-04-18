using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.Route.Services.Abstractions;
using Store.Route.Shared.DTOs.Products;
using Store.Route.Shared.ErrorsModels;


namespace Store.Route.Presentation
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController(IServiceManager _serviceManager) : ControllerBase
    {
        [HttpGet] // GET: baseUrl/api/products
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PaginationResponse<ProductResponse>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetails))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDetails))]
        //[Cache(180)]
        //[ResponseCache(Duration = 180)]

        //[Authorize] // Bearer Schema
        public async Task<ActionResult<PaginationResponse<ProductResponse>>> GetAllProducts([FromQuery] ProductQueryParameters productParams)
        {
            var result = await _serviceManager.ProductService.GetAllProductsAsync(productParams);
            return Ok(result); // 200
        }

        [HttpGet("{id}")] // GET: baseUrl/api/products/id
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProductResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetails))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetails))]
        public async Task<ActionResult<ProductResponse>> GetProductById(int? id)
        {
            if (id is null) return BadRequest(); // 400
            var result = await _serviceManager.ProductService.GetProductsByIdAsync(id.Value);
            if (result is null) return NotFound(); // 404

            return Ok(result); // 200

        }

        [HttpGet("brands")] // GET: baseUrl/api/products/brands
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BrandTypeResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetails))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDetails))]
        public async Task<ActionResult<BrandTypeResponse>> GetAllBrands()
        {
            var result = await _serviceManager.ProductService.GetAllBrandsAsync();
            if (result is null) return BadRequest(); // 400

            return Ok(result); // 200

        }

        [HttpGet("types")] // GET: baseUrl/api/products/types
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BrandTypeResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetails))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDetails))]
        public async Task<ActionResult<BrandTypeResponse>> GetAllTypes()
        {
            var result = await _serviceManager.ProductService.GetAllTypesAsync();
            if (result is null) return BadRequest(); // 400

            return Ok(result); // 200

        }



    }
}
