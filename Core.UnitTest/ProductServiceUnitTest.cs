using Core.Interfaces;
using Core.Models;
using Core.Services;
using Moq;

namespace Core.UnitTest;

public class ProductServiceUnitTest
{
    [Fact]
    public void GetProducts_ReturnsAllProducts()
    {
        var mock = new Mock<IProductRepository>();
        mock.Setup(p => p.GetProductsBy("a1")).Returns(new List<Product>
        {
            new()
            {
                Name = "Test1",
                Price = 12.34M,
                Description = "Test1",
                UserId = "a1"
            },
            new()
            {
                Name = "Test2",
                Price = 32.47M,
                Description = "Test2",
                UserId = "a1"
            }
        });
        var expected = new List<Product>
        {
            new()
            {
                Name = "Test1",
                Price = 12.34M,
                Description = "Test1",
                UserId = "a1"
            },
            new()
            {
                Name = "Test2",
                Price = 32.47M,
                Description = "Test2",
                UserId = "a1"
            }
        };
        var productService = new ProductService(mock.Object);

        var actual = (List<Product>)productService.GetProductsBy("a1");

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
                UserId = "a1"
            });
        var expected = new Product
        {
            ID = 2,
            Name = "Test2",
            Price = 32.47M,
            Description = "Test2",
            UserId = "a1"
        };
        var productService = new ProductService(mock.Object);

        var actual = productService.GetProductBy(2);

        Assert.Equal(expected.Name, actual.Name);
        Assert.Equal(expected.Price, actual.Price);
        Assert.Equal(expected.Description, actual.Description);
    }

    [Fact]
    public void GetProductBy_InvalidId_ThrowsArgumentException()
    {
        var mock = new Mock<IProductRepository>();
        mock.Setup(p => p.GetProductBy(2)).Returns(null as Product);
        var productService = new ProductService(mock.Object);

        void GetProductBy()
        {
            productService.GetProductBy(2);
        }

        Assert.Throws<ArgumentException>(GetProductBy);
    }

    [Fact]
    public void CreateProduct_WithValidProduct_ReturnsTrue()
    {
        var product = new Product
        {
            ID = 2,
            Name = "Test2",
            Price = 32.47M,
            Description = "Test2",
            UserId = "a1"
        };
        var mock = new Mock<IProductRepository>();
        mock.Setup(p => p.CreateProduct(product)).Returns(true);
        var productService = new ProductService(mock.Object);

        var result = productService.CreateProduct(product);

        Assert.True(result);
    }

    [Fact]
    public void CreateProduct_WithInvalidProduct_ThrowsInvalidOperationException()
    {
        var product = new Product();
        var mock = new Mock<IProductRepository>();
        mock.Setup(p => p.CreateProduct(product)).Returns(true);
        var productService = new ProductService(mock.Object);

        void CreateProduct()
        {
            productService.CreateProduct(product);
        }

        Assert.Throws<InvalidOperationException>(CreateProduct);
    }

    [Fact]
    public void UpdateProduct_WithValidProduct_ReturnsTrue()
    {
        var product = new Product
        {
            ID = 2,
            Name = "Test2",
            Price = 32.47M,
            Description = "Test2",
            UserId = "a1"
        };
        var mock = new Mock<IProductRepository>();
        mock.Setup(p => p.UpdateProduct(2, product)).Returns(true);
        mock.Setup(p => p.GetProductBy(2)).Returns(product);
        var productService = new ProductService(mock.Object);

        var result = productService.UpdateProduct(2, product);

        Assert.True(result);
    }

    [Fact]
    public void UpdateProduct_WithInvalidProduct_ThrowsInvalidOperationException()
    {
        var product = new Product();

        var mock = new Mock<IProductRepository>();
        mock.Setup(p => p.UpdateProduct(2, product)).Returns(true);
        var productService = new ProductService(mock.Object);

        void UpdateProduct()
        {
            productService.UpdateProduct(2, product);
        }

        Assert.Throws<InvalidOperationException>(UpdateProduct);
    }

    [Fact]
    public void UpdateProduct_InvalidId_ThrowsArgumentException()
    {
        var product = new Product
        {
            ID = 2,
            Name = "Test2",
            Price = 32.47M,
            Description = "Test2",
            UserId = "a1"
        };
        var mock = new Mock<IProductRepository>();
        mock.Setup(p => p.GetProductBy(2)).Returns(null as Product);
        var productService = new ProductService(mock.Object);

        void UpdateProduct()
        {
            productService.UpdateProduct(2, product);
        }

        Assert.Throws<ArgumentException>(UpdateProduct);
    }
    
    [Fact]
    public void UpdateProduct_DifferentUserId_ThrowsUnauthorizedAccessException()
    {
        var existingProduct = new Product
        {
            ID = 2,
            Name = "Test2",
            Price = 32.47M,
            Description = "Test2",
            UserId = "a2"
        };
        var product = new Product
        {
            ID = 2,
            Name = "Test3",
            Price = 32.47M,
            Description = "Test2",
            UserId = "a1"
        };
        var mock = new Mock<IProductRepository>();
        mock.Setup(p => p.GetProductBy(2)).Returns(existingProduct);
        var productService = new ProductService(mock.Object);

        void UpdateProduct()
        {
            productService.UpdateProduct(2, product);
        }

        Assert.Throws<UnauthorizedAccessException>(UpdateProduct);
    }

    [Fact]
    public void DeleteProduct_ValidId_ReturnsTrue()
    {
        var product = new Product
        {
            ID = 2,
            Name = "Test2",
            Price = 32.47M,
            Description = "Test2",
            UserId = "a1"
        };
        var mock = new Mock<IProductRepository>();
        mock.Setup(p => p.GetProductBy(2)).Returns(product);
        mock.Setup(p => p.DeleteProduct(2)).Returns(true);
        var productService = new ProductService(mock.Object);

        var result = productService.DeleteProduct(2, "a1");

        Assert.True(result);
    }

    [Fact]
    public void DeleteProduct_InvalidId_ThrowsArgumentException()
    {
        var mock = new Mock<IProductRepository>();
        mock.Setup(p => p.GetProductBy(2)).Returns(null as Product);
        var productService = new ProductService(mock.Object);

        void DeleteProduct()
        {
            productService.DeleteProduct(2, "a1");
        }

        Assert.Throws<ArgumentException>(DeleteProduct);
    }
    
    [Fact]
    public void DeleteProduct_DifferentUserId_ThrowsUnauthorizedAccessException()
    {
        var product = new Product
        {
            ID = 2,
            Name = "Test2",
            Price = 32.47M,
            Description = "Test2",
            UserId = "a1"
        };
        var mock = new Mock<IProductRepository>();
        mock.Setup(p => p.GetProductBy(2)).Returns(product);
        var productService = new ProductService(mock.Object);

        void DeleteProduct()
        {
            productService.DeleteProduct(2, "a2");
        }

        Assert.Throws<UnauthorizedAccessException>(DeleteProduct);
    }
}