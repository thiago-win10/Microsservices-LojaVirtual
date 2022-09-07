using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VShopping.ProductAPI.DTOs;
using VShopping.ProductAPI.Roles;
using VShopping.ProductAPI.Services.Interfaces;

namespace VShopping.ProductAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> Get()
        {
            var categoriesDTO = await _categoryService.GetCategories();
            if(categoriesDTO is null)
            {
                return NotFound("Categories Not Found");

            }
            return Ok(categoriesDTO);

        }

        [HttpGet("products")]
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> GetCategoriasProducts()
        {
            var categoriesDTO = await _categoryService.GetCategoriesProducts();
            if (categoriesDTO is null)
            {
                return NotFound("Categories Not Found");

            }
            return Ok(categoriesDTO);

        }

        [HttpGet("{id:int}", Name = "GetCategory")]
        public async Task<ActionResult<CategoryDTO>> Get(int id)
        {
            var categoryDTO = await _categoryService.GetCategorieById(id);
            if (categoryDTO is null)
            {
                return NotFound("Categories Not Found");

            }
            return Ok(categoryDTO);

        }

        [HttpPost]
        public async Task<ActionResult<CategoryDTO>> Cadastrar([FromBody] CategoryDTO categoryDTO)
        {
            if (categoryDTO is null)
            {
                return NotFound("Categories Not Found");

            }
            await _categoryService.AddCategory(categoryDTO);
            return new CreatedAtRouteResult("GetCategory", new { id = categoryDTO.CategoryId },
                categoryDTO);

        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<CategoryDTO>> Atualizar(int id, [FromBody] CategoryDTO categoryDTO)
        {
            if (id != categoryDTO.CategoryId)
            {
                return BadRequest();

            }
            if(categoryDTO is null)
            {
                return BadRequest();
            }
            await _categoryService.UpdateCategory(categoryDTO);
            return Ok(categoryDTO);

        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = Role.Admin)]
        public async Task<ActionResult<CategoryDTO>> Deletar(int id)
        {

            var categoryDTO = await _categoryService.GetCategorieById(id);

            if (categoryDTO == null)
            {
                return NotFound();
            }
            await _categoryService.RemoveCategory(id);
            return Ok(categoryDTO);

        }

    }
}
