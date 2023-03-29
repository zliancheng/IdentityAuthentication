using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mongo.Web.Models;
using Mongo.Web.Services;
using Newtonsoft.Json;
using System.Reflection;

namespace Mongo.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        public async Task<IActionResult> ProductIndex()
        {
			var accessToken = await HttpContext.GetTokenAsync("access_token");
			var response = await _productService.GetAllProductsAsync<ResponseDto>(accessToken);
            if(response != null && response.isSuccess)
            {
                var list = JsonConvert.DeserializeObject<List<ProductDto>>(Convert.ToString(response.Result));
                return View(list);
            }
            return View();
        }

		public async Task<IActionResult> ProductEdit(int productId)
		{
			var accessToken = await HttpContext.GetTokenAsync("access_token");
			var response = await _productService.GetProductByIdAsync<ResponseDto>(productId, accessToken);
            if (response != null && response.isSuccess)
            {
                ProductDto model = JsonConvert.DeserializeObject<ProductDto>(Convert.ToString(response.Result));
                return View(model);
            }
            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProductEdit(ProductDto model)
        {
            if (ModelState.IsValid)
            {
				var accessToken = await HttpContext.GetTokenAsync("access_token");
				var response = await _productService.UpdateProductAsync<ResponseDto>(model, accessToken);
                if (response != null && response.isSuccess)
                {
                    return RedirectToAction(nameof(ProductIndex));
                }
            }
            return View(model);
        }

        public IActionResult ProductCreate()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProductCreate(ProductDto model)
        {
            if (ModelState.IsValid)
            {
				var accessToken = await HttpContext.GetTokenAsync("access_token");
				var response = await _productService.CreateProductAsync<ResponseDto>(model, accessToken);
                if (response != null && response.isSuccess)
                {
                    return RedirectToAction(nameof(ProductIndex));
                }
            }
            return View(model);
        }
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> ProductDelete(int productId)
        {
			var accessToken = await HttpContext.GetTokenAsync("access_token");
			var response = await _productService.GetProductByIdAsync<ResponseDto>(productId, accessToken);
            if (response != null && response.isSuccess)
            {
                ProductDto model = JsonConvert.DeserializeObject<ProductDto>(Convert.ToString(response.Result));
                return View(model);
            }
            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProductDelete(ProductDto model)
        {
			var accessToken = await HttpContext.GetTokenAsync("access_token");
			var response = await _productService.DeleteProductAsync<ResponseDto>(model.Id, accessToken);
            if (response != null && response.isSuccess)
            {
                return RedirectToAction(nameof(ProductIndex));
            }
            return View();
        }

    }
}
