using AutoMapper;
using VShopping.ProductAPI.DTOs;
using VShopping.ProductAPI.Models;
using VShopping.ProductAPI.Repository.Interfaces;
using VShopping.ProductAPI.Services.Interfaces;

namespace VShopping.ProductAPI.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        public ProductService(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }
        

        public async Task<IEnumerable<ProductDTO>> GetProdcuts()
        {
            var productsEntity = await _productRepository.GetAll();
            return _mapper.Map<IEnumerable<ProductDTO>>(productsEntity);
        }

        public async Task<ProductDTO> GetProductById(int id)
        {
            var productsEntity = await _productRepository.GetById(id);
            return _mapper.Map<ProductDTO>(productsEntity);
        }
        public async Task AddProduct(ProductDTO productDTO)
        {
            var productEntity = _mapper.Map<Product>(productDTO);
            await _productRepository.Create(productEntity);
            productDTO.Id = productEntity.Id;

        }

        public async Task UpdateProduct(ProductDTO productDTO)
        {
            var producEntity = _mapper.Map<Product>(productDTO);
            await _productRepository.Update(producEntity);  
            
        }
        public async Task RemoveProduct(int id)
        {
            var productsEntity = await _productRepository.GetById(id);
            await _productRepository.Update(productsEntity);

        }

    }
}
