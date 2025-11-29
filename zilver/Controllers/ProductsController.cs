using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using zilver.services;

namespace zilver.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ProductsController : Controller
    {
        private readonly ProductService _productService;
        public ProductsController(ProductService productService) => _productService = productService;

        public async Task<IActionResult> Index()
        {
            var products = await _productService.GetAllProductsAsync();
            return View(products);
        }
    }
}
