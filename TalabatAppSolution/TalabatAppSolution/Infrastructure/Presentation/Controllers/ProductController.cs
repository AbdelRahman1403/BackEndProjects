using Microsoft.AspNetCore.Mvc;
using ServiceAbstractionLayer.IServices;
using Shared.Common;
using Shared.Dtos.ProductDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController(IServiceManager serviceManager) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<PaginationResult<ProductDto>>> GetAllProducts([FromQuery]ProductQueryPrams productQuery)
        {
            var products = await serviceManager.ProductServices.GetAllProductsAsync(productQuery);

            return Ok(products);
        }
        [HttpGet("Brands")]
        public async Task<ActionResult<IEnumerable<BrandDto>>> GetAllBrands()
        {
            var brands = await serviceManager.ProductServices.GetAllBrandsAsync();

            return Ok(brands);
        }
        [HttpGet("Types")]
        public async Task<ActionResult<IEnumerable<BrandDto>>> GetAllTypes()
        {
            var types = await serviceManager.ProductServices.GetAllTypesAsync();

            return Ok(types);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetProductById(int id)
        {
            var product = await serviceManager.ProductServices.GetProductByIdAsync(id);
            if (product == null)
                return NotFound();
            return Ok(product);
        }
    }
}
