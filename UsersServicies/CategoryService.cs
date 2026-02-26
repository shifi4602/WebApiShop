using AutoMapper;
using DTO_s;
using Enteties;
using Repositories;

namespace Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IMapper _mapper;
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CategroryDTO>> GetCategories()
        {
            IEnumerable<Category> categories = await _categoryRepository.GetCategories();
            IEnumerable<CategroryDTO> categroryDTOs = _mapper.Map<IEnumerable<Category>, IEnumerable<CategroryDTO>>(categories);
            return categroryDTOs;
        }
    }
}
