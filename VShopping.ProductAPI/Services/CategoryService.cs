using AutoMapper;
using VShopping.ProductAPI.DTOs;
using VShopping.ProductAPI.Models;
using VShopping.ProductAPI.Repository.Interfaces;
using VShopping.ProductAPI.Services.Interfaces;

namespace VShopping.ProductAPI.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<CategoryDTO>> GetCategories()
        {
            var categoriesEntity = await _categoryRepository.GetAll();
            return _mapper.Map<IEnumerable<CategoryDTO>>(categoriesEntity);
        }
        public async Task<IEnumerable<CategoryDTO>> GetCategoriesProducts()
        {
            var categoriesEntity = await _categoryRepository.GetCategoryProducts();
            return _mapper.Map<IEnumerable<CategoryDTO>>(categoriesEntity);
        }
        public async Task<CategoryDTO> GetCategorieById(int id)
        {
            var categoriesEntity = await _categoryRepository.GetById(id);
            return _mapper.Map<CategoryDTO>(categoriesEntity);
        }
        public async Task AddCategory(CategoryDTO categoryDTO)
        {
            var categorieEntity = _mapper.Map<Category>(categoryDTO);
            await _categoryRepository.Create(categorieEntity);
            categoryDTO.CategoryId = categorieEntity.CategoryId;

        }
        public async Task UpdateCategory(CategoryDTO categoryDTO)
        {
            var categorieEntity = _mapper.Map<Category>(categoryDTO);
            await _categoryRepository.Update(categorieEntity);

        }

        public async Task RemoveCategory(int id)
        {
            var categoriesEntity = await _categoryRepository.GetById(id);
            await _categoryRepository.Delete(categoriesEntity.CategoryId);

        }

    }
}
