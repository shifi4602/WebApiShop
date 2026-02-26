using Enteties;
using Repositories;

namespace Services
{
    public class ProtuctService : IProtuctService
    {
        private readonly IProductReposetory _productRepository;

        public ProtuctService(IProductReposetory productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<IEnumerable<Product>> GetProducts(string? name, int[]? categories, int? minPrice, int? maxPrice, int? limit, string? orderBy, int? offset)
        {
            return await _productRepository.GetProducts(name, categories, minPrice, maxPrice, limit, orderBy, offset);
        }
    }
}
