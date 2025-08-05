using JwtAuthDemo.Models;
using JwtAuthDemo.Services;

namespace JwtAuthDemo_Test
{
    [TestClass]
    public sealed class Test1
    {
        [TestMethod]
        public void TestMethod1()
        {
            // Arrange
            var product = new Product
            {
                Id = 1,
                Name = "Test Product",
                Price = 9.99m,
                Description = "This is a test product."
            };
            var mockService = new Moq.Mock<ProductService>(null);
            mockService.Setup(service => service.AddProduct(It.IsAny<JwtAuthDemo.Models.Product>()))
                       .Returns(product);
            var controller = new JwtAuthDemo.Controllers.ProductController(mockService.Object);
            // Act
            var result = controller.AddProduct(product);
            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(product.Name, result.Name);
        }
    }
}
