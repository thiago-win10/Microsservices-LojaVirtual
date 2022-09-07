using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VShopping.ProductAPI.DTOs;
using VShopping.ProductAPI.Roles;
using VShopping.ProductAPI.Services.Interfaces;

namespace VShopping.ProductAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> Get()
        {
            var productsDTO = await _productService.GetProdcuts();
            if (productsDTO is null)
            {
                return NotFound("Products Not Found");

            }
            return Ok(productsDTO);

        }

        [HttpGet("{id:int}", Name = "GetProduct")]
        public async Task<ActionResult<ProductDTO>> Get(int id)
        {

            var productsDTO = await _productService.GetProductById(id);
            if (productsDTO is null)
            {
                return NotFound("Products Not Found");

            }
            return Ok(productsDTO);

        }

        [HttpPost]
        [Authorize(Roles = Role.Admin)]
        public async Task<ActionResult<ProductDTO>> Cadastrar([FromBody] ProductDTO productDTO)
        {
           
            if (productDTO is null)
            {
                return NotFound("Categories Not Found");

            }
            try
            {
                await _productService.AddProduct(productDTO);
                return new CreatedAtRouteResult("GetProduct", new { id = productDTO.Id },
                    productDTO);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


        }

        [HttpPut]
        [Authorize(Roles = Role.Admin)]
        public async Task<ActionResult<ProductDTO>> AtualizarProduto([FromBody] ProductDTO productDTO)
        {
           
            if (productDTO == null)
            {
                return BadRequest("Data invalid");
            }
            await _productService.UpdateProduct(productDTO);
            return Ok(productDTO);

        }

        [HttpDelete("{id}")]
        [Authorize(Roles = Role.Admin)]
        public async Task<ActionResult<ProductDTO>> Delete(int id)
        {

            var productDTO = await _productService.GetProductById(id);

            if (productDTO == null)
            {
                return NotFound("Product not found");
            }
            await _productService.RemoveProduct(id);
            return Ok(productDTO);

        }

    }
}
