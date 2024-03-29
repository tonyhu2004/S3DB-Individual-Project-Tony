using Core.Interfaces;
using Core.Models;
using Core.Services;
using Moq;

namespace Core.UnitTest
{
    public class ProductServiceUnitTest
    {
        [Fact]
        public void GetProducts_ReturnsAllProducts()
        {
            var mock = new Mock<IProductRepository>();
            mock.Setup(p => p.GetProducts()).Returns(new List<Product>
            {
               new Product
               {
                   Name = "Test1",
                   Price = 12.34M,
                   Description = "Test1",
               },
               new Product
               {
                   Name = "Test2",
                   Price = 32.47M,
                   Description = "Test2",
               }
            });
            List<Product> expected = new List<Product>
            {
               new Product
               {
                   Name = "Test1",
                   Price = 12.34M,
                   Description = "Test1",
               },
               new Product
               {
                   Name = "Test2",
                   Price = 32.47M,
                   Description = "Test2",
               }
            };
            ProductService productService = new ProductService(mock.Object);

            List<Product> actual = (List<Product>)productService.GetProducts();

            Assert.Equal(expected[0].Name, actual[0].Name);
            Assert.Equal(expected[0].Price, actual[0].Price);
            Assert.Equal(expected[0].Description, actual[0].Description);
            Assert.Equal(expected[1].Name, actual[1].Name);
            Assert.Equal(expected[1].Price, actual[1].Price);
            Assert.Equal(expected[1].Description, actual[1].Description);
        }

        [Fact]
        public void GetProductBy_ValidId_ReturnsProduct()
        {
            var mock = new Mock<IProductRepository>();
            mock.Setup(p => p.GetProductBy(2)).Returns(
                new Product
                {
                    ID = 2,
                    Name = "Test2",
                    Price = 32.47M,
                    Description = "Test2",
                });
            Product expected = new Product
            {
                ID = 2,
                Name = "Test2",
                Price = 32.47M,
                Description = "Test2",
            };
            ProductService productService = new ProductService(mock.Object);

            Product? actual = productService.GetProductBy(2);

            Assert.Equal(expected.Name, actual.Name);
            Assert.Equal(expected.Price, actual.Price);
            Assert.Equal(expected.Description, actual.Description);
        }

        [Fact]
        public void GetProductBy_InvalidId_ReturnsNull()
        {
            var mock = new Mock<IProductRepository>();
            mock.Setup(p => p.GetProductBy(2)).Returns(null as Product);
            ProductService productService = new ProductService(mock.Object);

            Product? actual = productService.GetProductBy(2);

            Assert.Null(actual);
        }

        [Fact]
        public void CreateProduct_WithValidProduct_ReturnsTrue()
        {
            Product product = new Product
            {
                ID = 2,
                Name = "Test2",
                Price = 32.47M,
                Description = "Test2",
            };
            var mock = new Mock<IProductRepository>();
            mock.Setup(p => p.CreateProduct(product)).Returns(true);
            ProductService productService = new ProductService(mock.Object);

            bool result = productService.CreateProduct(product);

            Assert.True(result);
        }

        [Fact]
        public void CreateProduct_Null_ReturnsFalse()
        {
            var mock = new Mock<IProductRepository>();
            mock.Setup(p => p.CreateProduct(null)).Returns(true);
            ProductService productService = new ProductService(mock.Object);

            bool result = productService.CreateProduct(null);

            Assert.False(result);
        }

        [Fact]
        public void CreateProduct_WithInvalidProduct_ReturnsFalse()
        {
            Product product = new Product();
            var mock = new Mock<IProductRepository>();
            mock.Setup(p => p.CreateProduct(product)).Returns(true);
            ProductService productService = new ProductService(mock.Object);

            bool result = productService.CreateProduct(product);

            Assert.False(result);
        }

        [Fact]
        public void UpdateProduct_WithValidProduct_ReturnsTrue()
        {
            Product product = new Product
            {
                ID = 2,
                Name = "Test2",
                Price = 32.47M,
                Description = "Test2",
            };
            var mock = new Mock<IProductRepository>();
            mock.Setup(p => p.UpdateProduct(2, product)).Returns(true);
            mock.Setup(p => p.GetProductBy(2)).Returns(product);
            ProductService productService = new ProductService(mock.Object);

            bool result = productService.UpdateProduct(2, product);

            Assert.True(result);
        }

        [Fact]
        public void UpdateProduct_Null_ReturnsFalse()
        {
            var mock = new Mock<IProductRepository>();
            mock.Setup(p => p.UpdateProduct(2, null)).Returns(true);
            ProductService productService = new ProductService(mock.Object);
            bool result = productService.UpdateProduct(2, null);

            Assert.False(result);
        }

        [Fact]
        public void UpdateProduct_WithInvalidProduct_ReturnsFalse()
        {
            Product product = new Product();

            var mock = new Mock<IProductRepository>();
            mock.Setup(p => p.UpdateProduct(2, product)).Returns(true);
            ProductService productService = new ProductService(mock.Object);

            bool result = productService.UpdateProduct(2, product);

            Assert.False(result);
        }

        [Fact]
        public void UpdateProduct_InvalidId_ReturnsFalse()
        {
            Product product = new Product
            {
                ID = 2,
                Name = "Test2",
                Price = 32.47M,
                Description = "Test2",
            };
            var mock = new Mock<IProductRepository>();
            mock.Setup(p => p.UpdateProduct(2, product)).Returns(true);
            mock.Setup(p => p.GetProductBy(2)).Returns(null as Product);
            ProductService productService = new ProductService(mock.Object);

            bool result = productService.UpdateProduct(2, product);

            Assert.False(result);
        }

        [Fact]
        public void DeleteProduct_ValidId_ReturnsTrue()
        {
            Product product = new Product
            {
                ID = 2,
                Name = "Test2",
                Price = 32.47M,
                Description = "Test2",
            };
            var mock = new Mock<IProductRepository>();
            mock.Setup(p => p.GetProductBy(2)).Returns(product);
            mock.Setup(p => p.DeleteProduct(2)).Returns(true);
            ProductService productService = new ProductService(mock.Object);

            bool result = productService.DeleteProduct(2);

            Assert.True(result);
        }

        [Fact]
        public void DeleteProduct_InvalidId_ReturnsFalse()
        {
            var mock = new Mock<IProductRepository>();
            mock.Setup(p => p.GetProductBy(2)).Returns(null as Product);
            mock.Setup(p => p.DeleteProduct(2)).Returns(true);
            ProductService productService = new ProductService(mock.Object);

            bool result = productService.DeleteProduct(2);

            Assert.False(result);
        }
    }
}