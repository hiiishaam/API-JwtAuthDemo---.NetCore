using JwtAuthDemo.Data;
using JwtAuthDemo.Models;
using System.Linq;
using System.Linq.Expressions;

namespace JwtAuthDemo.Services
{

    public class ProductService
    {
        public AppDbContext _db;
        public ProductService(AppDbContext appContext)
        {
            _db = appContext;
        }
        /// <summary>
        /// get all products
        /// </summary>
        /// <returns></returns>
        public List<Product> GetProducts()
        {
            try
            {
                return _db.Products.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("pas des produits dans la list: " + ex.Message);
            }
        }
        /// <summary>
        /// add a product
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public Product AddProduct(Product product)
        {
            try
            {
                _db.Products.Add(product);
                _db.SaveChanges();
                return product;
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
        /// <exception cref="Exception"></exception>
        public Product UpdateProduct(int id, Product product)
        {
            try
            {
                var existingProduct = _db.Products.FirstOrDefault(p => p.Id == id);
                if (existingProduct == null)
                {
                    return null;
                }
                existingProduct.Name = product.Name;
                existingProduct.Price = product.Price;
                existingProduct.Stock = product.Stock;
                existingProduct.Description = product.Description;
                existingProduct.ImageUrl = product.ImageUrl;

                _db.SaveChanges();

                return existingProduct;
            }
            catch (Exception)
            {
                throw; // Tu peux améliorer ça en lançant une exception plus explicite si tu veux
            }
        }

        /// <summary>
        /// Get a product by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public Product GetProductById(int id)
        {
            try
            {
                return _db.Products.FirstOrDefault(p => p.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving product: " + ex.Message);
            }
        }
        /// <summary>
        ///  Delete a product
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public Product DeleteProduct(int id) {      
            try
            {
                var product = _db.Products.FirstOrDefault(p => p.Id == id);
                _db.Products.Remove(product);
                _db.SaveChanges();
                return product;
            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting product: " + ex.Message);
            }
        }
    }
}