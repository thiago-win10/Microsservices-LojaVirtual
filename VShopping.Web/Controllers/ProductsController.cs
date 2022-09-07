using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using VShopping.Web.Models;
using VShopping.Web.Roles;
using VShopping.Web.Services.Interfaces;

namespace VShopping.Web.Controllers
{
    //Definindo restrições
    [Authorize(Roles = Role.Admin)]
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductsController(IProductService productService,
            ICategoryService categoryService, IWebHostEnvironment webHostEnvironment)
        {
            _productService = productService;
            _categoryService = categoryService;
            _webHostEnvironment = webHostEnvironment;

        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductViewModel>>> Index()
        {
             var result = await _productService.GetAllProducts(await GetAccessToken());
            if (result is null)
                return View("Error");

            return View(result);
        }

        [HttpGet] //Carregar a Lista de Categorias(selecionar)
        public async Task<IActionResult> CreateProduct()
        {
            ViewBag.CategoriaId = new SelectList(await
                _categoryService.GetAllCategory(await GetAccessToken()), "CategoryId", "Name");

            return View();


        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(ProductViewModel productView)
        {

            if (ModelState.IsValid)
            {
                var result = _productService.CreateProduct(productView, await GetAccessToken());

                if (result != null)
                    return RedirectToAction(nameof(Index));
            }
            else
            {

                ViewBag.CategoriaId = new SelectList(await
                    _categoryService.GetAllCategory(await GetAccessToken()), "CategoryId", "Name");
            }

            return View(productView);
        }

        [HttpGet]
        public async Task<IActionResult> UpdateProduct(int id)
        {

            ViewBag.CategoriaId = new SelectList(await
                _categoryService.GetAllCategory(await GetAccessToken()), "CategoryId", "Name");

            var result = await _productService.GetProductById(id, await GetAccessToken());

            if (result != null)
                return View("Error");

            return View(result);
        }

        //Update
        [HttpPost]
        public async Task<IActionResult> UpdateProduct(ProductViewModel productView)
        {

            if (ModelState.IsValid)
            {
                var result = _productService.UpdateProduct(productView, await GetAccessToken());

                if (result is not null)
                    return RedirectToAction(nameof(Index));
            }
            return View(productView);
        }

        [HttpDelete]
        public async Task<ActionResult<ProductViewModel>> DeleteProduct(int id)
        {

            //ViewBag.CategoriaId = new SelectList(await
            //    _categoryService.GetAllCategory(), "CategoryId", "Name");

            var result = await _productService.GetProductById(id, await GetAccessToken());

            if (result is null)
                return View("Error");

            return View(result);
        }

        //Delete
        [HttpPost(), ActionName("DeleteProduct")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            var result = await _productService.DeleteProductById(id, await GetAccessToken());

            if (!result)
                return View("Error");

            return RedirectToAction(nameof(Index));
        }

        private async Task<string> GetAccessToken()
        {
            return await HttpContext.GetTokenAsync("access_token");

        }

    }
}
