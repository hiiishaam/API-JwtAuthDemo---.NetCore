using JwtAuthDemo.Models;
using JwtAuthDemo.Services;
using Microsoft.AspNetCore.Mvc;

namespace JwtAuthDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        public readonly ProductService _Service;
        public ProductController(ProductService service)
        {
            _Service = service;
        }

        [HttpGet]
        public List<Product> GetProducts()
        {
            return _Service.GetProducts();
        }
        [HttpPost]
        public Product AddProduct([FromBody]Product product)
        {
            return _Service.AddProduct(product);
        }
        public Product UpdateProduct([FromBody]Product product)
        {
            return _Service.UpdateProduct(product);
        }

    }
}
