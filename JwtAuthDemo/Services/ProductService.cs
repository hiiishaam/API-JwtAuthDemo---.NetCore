using JwtAuthDemo.Data;
using JwtAuthDemo.Models;
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
        public Product UpdateProduct(Product product) 
        {
            try
            { 
                _db.Products.Update(product);
                _db.SaveChanges();
                return product;
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating product: " + ex.Message);
            }

        }
    }
}