using Enteties;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace WebApiShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProtuctService _productService;

        public ProductsController(IProtuctService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IEnumerable<Product>> GetProducts(string? name, int[]? categories, int? minPrice, int? maxPrice, int? limit, string? orderBy, int? offset)
        {
            return await _productService.GetProducts(name, categories, minPrice, maxPrice, limit, orderBy, offset);
        }
    }
}
