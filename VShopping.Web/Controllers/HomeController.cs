using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using VShopping.Web.Models;
using VShopping.Web.Services.Interfaces;

namespace VShopping.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductService _productService;
        private readonly ICartService _cartService;
        public HomeController(ILogger<HomeController> logger, IProductService productService, ICartService cartService)
        {
            _logger = logger;
            _productService = productService;
            _cartService = cartService;

        }

        public async Task<IActionResult> Index()
        {
            var result = await _productService.GetAllProducts(String.Empty);
            if (result is null)
                return View("Error");

            return View(result);
        }

        //Exibindo as imagens dos produtos por ID
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<ProductViewModel>> ProductDetails(int id)
        {
            var token = await HttpContext.GetTokenAsync("access_token");
            var product = await _productService.GetProductById(id, token);
            if (product is null)
                return View("Error");

            return View(product);
        }

        [HttpPost]
        [ActionName("ProductDetails")]
        [Authorize]
        public async Task<ActionResult<ProductViewModel>> ProductDetailsPost(ProductViewModel productVM)
        {
            var token = await HttpContext.GetTokenAsync("access_token");

            CartViewModel cart = new()
            {
                CartHeader = new CartHeaderViewModel
                {
                    UserId = User.Claims.Where(u => u.Type == "sub")?.FirstOrDefault()?.Value
                }
            };

            CartItemViewModel cartItem = new()
            {
                Quantity = productVM.Quantity,
                ProductId = productVM.Id,
                Product = await _productService.GetProductById(productVM.Id, token)
            };

            List<CartItemViewModel> cartItemsVM = new List<CartItemViewModel>();
            cartItemsVM.Add(cartItem);
            var a = cart.CartItems = cartItemsVM;

            if (a != null)
            {
                var result = await _cartService.AddItemToCartAsync(cart, token);


            }
            else
            {
                //return View(productVM);

                return RedirectToAction(nameof(Index));

            }
            var resultado = await _cartService.AddItemToCartAsync(cart, token);

            if (resultado is not null)
            {
                return RedirectToAction(nameof(Index));

            }
            return View(productVM);


        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        
        [Authorize]
        public async Task<IActionResult> Login()
        {
            var acessToken = await HttpContext.GetTokenAsync("access_token"); 
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Logout()
        {
            return SignOut("Cookies", "oidc");
        }
    }
}