using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Product.API.Contracts;
using Product.API.Helper.Filter.cache;
using Product.API.Helper.Security;
using Product.Application.Contracts.Infrastructure;
using Product.Application.Contracts.Persistence;
using Product.Application.Resources.Product.Create;
using Product.Application.Resources.Product.Get;

namespace Product.API.Controllers
{
    public class ProductController : BaseController
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService )
        {
            _productService = productService;
        }

        [Authorize(Roles = Roles.Admin)]
        [HttpPost(ApiRoute.Product.CreateProduct)]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductRequestDto dto)
        {
           var result = await _productService.CreateProduct(dto);

            return Ok(result);
        }

        [Authorize(Roles = Roles.Admin + "," + Roles.User)]
        [Cache(700)]
        [HttpPost(ApiRoute.Product.GetProduct)]
        public IActionResult GetProducts([FromBody] GetProductsRequestDto dto)
        {
            var result = _productService.GetProducts(dto);
            return Ok(result);
        }


    }
}
