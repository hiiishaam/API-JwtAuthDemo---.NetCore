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
        /// <summary>
        /// get all products
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public List<Product> GetProducts()
        {
            try
            {
                return _Service.GetProducts();
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving products: " + ex.Message);
            }
        }
        /// <summary>
        /// add a product
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        [HttpPost]
        public Product AddProduct([FromBody]Product product)
        {
            try {
                return _Service.AddProduct(product);
                }
            catch (Exception ex)
            {
                throw new Exception("Error adding product: " + ex.Message);
            }


        }
        /// <summary>
        /// update a product    
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public IActionResult UpdateProduct(int id, [FromBody] Product product)
        {
            try
            {
                var updatedProduct = _Service.UpdateProduct(id, product);

                if (updatedProduct == null)
                {
                    return NotFound(new { message = "Product not found" });
                }

                return Ok(new { message = "Product updated successfully", product = updatedProduct });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error updating product", error = ex.Message });
            }
        }

        /// <summary>
        /// Delete a product by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="product"></param>
        /// <returns></returns>
        [HttpDelete]
        public IActionResult DeleteProduct(int id )
        {
            try
            {
                var prod = _Service.GetProductById(id);
                if (prod == null)
                {
                    return NotFound("Product not found");
                }
                _Service.DeleteProduct(id);
                return Ok("Product deleted successfully");

            }
            catch (Exception ex)
            {
                return BadRequest("Error Deleting product: " + ex.Message);
            }

        }

        [HttpGet("{id}")]
        public IActionResult GetProductById(int id)
        {
            try
            {
                var product = _Service.GetProductById(id);
                if (product == null)
                {
                    return NotFound(new { message = "Product not found" });
                }
                return Ok(product);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error retrieving product", error = ex.Message });
            }
        }

    }
}
