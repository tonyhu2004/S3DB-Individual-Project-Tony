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
        var mockProduct = new Mock<IProductRepository>();
        var mockCloudinary = new Mock<ICloudinaryRepository>();
        mockProduct.Setup(p => p.GetProductsBy("a1")).Returns(new List<Product>
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
        var productService = new ProductService(mockProduct.Object, mockCloudinary.Object);

        var actual = (List<Product>)productService.GetProductsBy("a1");

        Assert.Equal(expected[0].Name, actual[0].Name);
        Assert.Equal(expected[0].Price, actual[0].Price);
        Assert.Equal(expected[0].Description, actual[0].Description);
        Assert.Equal(expected[1].Name, actual[1].Name);
        Assert.Equal(expected[1].Price, actual[1].Price);
        Assert.Equal(expected[1].Description, actual[1].Description);
    }

    [Fact]
    public void GetPageProducts_ForPage1_ReturnsAllProducts()
    {
        var mockProduct = new Mock<IProductRepository>();
        var mockCloudinary = new Mock<ICloudinaryRepository>();
        mockProduct.Setup(p => p.GetPageProducts(0,1)).Returns(new List<Product>
        {
            new()
            {
                Name = "Test1",
                Price = 12.34M,
                Description = "Test1",
                UserId = "a1"
            },
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
        };        
        var productService = new ProductService(mockProduct.Object, mockCloudinary.Object);

        var actual = productService.GetPageProducts(1,1).ToList();

        Assert.Equal(expected[0].ID, actual[0].ID);
        Assert.Equal(expected[0].Name, actual[0].Name);
        Assert.Equal(expected[0].Price, actual[0].Price);
        Assert.Equal(expected[0].Description, actual[0].Description);
    }
    
    [Fact]
    public void GetPageProducts_ForPage2_ReturnsAllProducts()
    {
        var mockProduct = new Mock<IProductRepository>();
        var mockCloudinary = new Mock<ICloudinaryRepository>();
        mockProduct.Setup(p => p.GetPageProducts(1,1)).Returns(new List<Product>
        {
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
                Name = "Test2",
                Price = 32.47M,
                Description = "Test2",
                UserId = "a1"
            }
        };
        var productService = new ProductService(mockProduct.Object, mockCloudinary.Object);

        var actual = productService.GetPageProducts(2,1).ToList();

        Assert.Equal(expected[0].ID, actual[0].ID);
        Assert.Equal(expected[0].Name, actual[0].Name);
        Assert.Equal(expected[0].Price, actual[0].Price);
        Assert.Equal(expected[0].Description, actual[0].Description);
    }

    [Fact]
    public void GetProductCount_ReturnsAmountOfProducts()
    {
        var mockProduct = new Mock<IProductRepository>();
        var mockCloudinary = new Mock<ICloudinaryRepository>();
        mockProduct.Setup(p => p.GetProductCount()).Returns(10);
        const int expected = 10;
        var productService = new ProductService(mockProduct.Object, mockCloudinary.Object);

        var actual = productService.GetProductCount();

        Assert.Equal(expected, actual);
    }
    
    [Fact]
    public void GetProductBy_ValidId_ReturnsProduct()
    {
        var mockProduct = new Mock<IProductRepository>();
        var mockCloudinary = new Mock<ICloudinaryRepository>();
        mockProduct.Setup(p => p.GetProductBy(2)).Returns(
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
        var productService = new ProductService(mockProduct.Object, mockCloudinary.Object);

        var actual = productService.GetProductBy(2);

        Assert.Equal(expected.Name, actual.Name);
        Assert.Equal(expected.Price, actual.Price);
        Assert.Equal(expected.Description, actual.Description);
    }

    [Fact]
    public void GetProductBy_InvalidId_ThrowsArgumentException()
    {
        var mockProduct = new Mock<IProductRepository>();
        var mockCloudinary = new Mock<ICloudinaryRepository>();
        mockProduct.Setup(p => p.GetProductBy(2)).Returns(null as Product);
        var productService = new ProductService(mockProduct.Object, mockCloudinary.Object);

        Assert.Throws<ArgumentException>(() => productService.GetProductBy(2));
    }
    
    [Fact]
    public void GetProductWithReviewsBy_ValidId_ReturnsProduct()
    {
        var mockProduct = new Mock<IProductRepository>();
        var mockCloudinary = new Mock<ICloudinaryRepository>();
        var expected = new Product
        {
            ID = 2,
            Name = "Test2",
            Price = 32.47M,
            Description = "Test2",
            UserId = "a1",
            Reviews = new List<Review>
            {
                new()
                {
                    ID = 1,
                    Rating = 3,
                    Comment = "Pretty mid",
                    ProductId = 2,
                    UserId = "a3",
                    PublishedDate = DateTime.Now,
                },
                new()
                {
                    ID = 2,
                    Rating = 5,
                    Comment = "Very good",
                    ProductId = 2,
                    UserId = "a2",
                    PublishedDate = DateTime.Now,
                }
            }
        };
        mockProduct.Setup(p => p.GetProductWithReviewsBy(2)).Returns(expected);
        var productService = new ProductService(mockProduct.Object, mockCloudinary.Object);

        var actual = productService.GetProductWithReviewsBy(2);

        Assert.Equal(expected.ID, actual.ID);
        Assert.Equal(expected.Name, actual.Name);
        Assert.Equal(expected.Price, actual.Price);
        Assert.Equal(expected.Description, actual.Description);
        Assert.Equal(expected.UserId, actual.UserId);
        Assert.Equal(expected.Reviews.Count, actual.Reviews.Count);
        Assert.Equal(4, actual.AverageRating);

        Assert.Equal(expected.Reviews[0].ID, actual.Reviews[0].ID);
        Assert.Equal(expected.Reviews[0].Rating, actual.Reviews[0].Rating);
        Assert.Equal(expected.Reviews[0].Comment, actual.Reviews[0].Comment);
        Assert.Equal(expected.Reviews[0].ProductId, actual.Reviews[0].ProductId);
        Assert.Equal(expected.Reviews[0].UserId, actual.Reviews[0].UserId);
        Assert.Equal(expected.Reviews[0].PublishedDate, actual.Reviews[0].PublishedDate);
        
        Assert.Equal(expected.Reviews[1].ID, actual.Reviews[1].ID);
        Assert.Equal(expected.Reviews[1].Rating, actual.Reviews[1].Rating);
        Assert.Equal(expected.Reviews[1].Comment, actual.Reviews[1].Comment);
        Assert.Equal(expected.Reviews[1].ProductId, actual.Reviews[1].ProductId);
        Assert.Equal(expected.Reviews[1].UserId, actual.Reviews[1].UserId);
        Assert.Equal(expected.Reviews[1].PublishedDate, actual.Reviews[1].PublishedDate);
    }

    [Fact]
    public void GetProductWithReviewsBy_InvalidId_ThrowsArgumentException()
    {
        var mockProduct = new Mock<IProductRepository>();
        var mockCloudinary = new Mock<ICloudinaryRepository>();
        mockProduct.Setup(p => p.GetProductWithReviewsBy(2)).Returns(null as Product);
        var productService = new ProductService(mockProduct.Object, mockCloudinary.Object);

        Assert.Throws<ArgumentException>(() => productService.GetProductWithReviewsBy(2));
    }

    // [Fact]
    // public void CreateProduct_WithValidProduct_ReturnsTrue()
    // {
    //     var product = new Product
    //     {
    //         ID = 2,
    //         Name = "Test2",
    //         Price = 32.47M,
    //         Description = "Test2",
    //         UserId = "a1"
    //     };
    //     var mockProduct = new Mock<IProductRepository>();
    //     var mockCloudinary = new Mock<ICloudinaryRepository>();
    //     mockProduct.Setup(p => p.CreateProduct(product)).Returns(true);
    //     var productService = new ProductService(mockProduct.Object, mockCloudinary.Object);
    //
    //     var result = productService.CreateProduct(product);
    //
    //     Assert.True(result);
    // }
    //
    // [Fact]
    // public void CreateProduct_WithInvalidProduct_ThrowsInvalidOperationException()
    // {
    //     var product = new Product();
    //     var mockProduct = new Mock<IProductRepository>();
    //     var mockCloudinary = new Mock<ICloudinaryRepository>();
    //     mockProduct.Setup(p => p.CreateProduct(product)).Returns(true);
    //     var productService = new ProductService(mockProduct.Object, mockCloudinary.Object);
    //
    //     Assert.Throws<InvalidOperationException>(() => productService.CreateProduct(product));
    // }

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
        var mockProduct = new Mock<IProductRepository>();
        var mockCloudinary = new Mock<ICloudinaryRepository>();
        mockProduct.Setup(p => p.UpdateProduct(2, product)).Returns(true);
        mockProduct.Setup(p => p.GetProductBy(2)).Returns(product);
        var productService = new ProductService(mockProduct.Object, mockCloudinary.Object);

        var result = productService.UpdateProduct(2, product);

        Assert.True(result);
    }

    [Fact]
    public void UpdateProduct_WithInvalidProduct_ThrowsInvalidOperationException()
    {
        var product = new Product();

        var mockProduct = new Mock<IProductRepository>();
        var mockCloudinary = new Mock<ICloudinaryRepository>();
        mockProduct.Setup(p => p.UpdateProduct(2, product)).Returns(true);
        var productService = new ProductService(mockProduct.Object, mockCloudinary.Object);
        
        Assert.Throws<InvalidOperationException>(() => productService.UpdateProduct(2, product));
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
        var mockProduct = new Mock<IProductRepository>();
        var mockCloudinary = new Mock<ICloudinaryRepository>();
        mockProduct.Setup(p => p.GetProductBy(2)).Returns(null as Product);
        var productService = new ProductService(mockProduct.Object, mockCloudinary.Object);

        Assert.Throws<ArgumentException>(() => productService.UpdateProduct(2, product));
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
        var mockProduct = new Mock<IProductRepository>();
        var mockCloudinary = new Mock<ICloudinaryRepository>();
        mockProduct.Setup(p => p.GetProductBy(2)).Returns(existingProduct);
        var productService = new ProductService(mockProduct.Object, mockCloudinary.Object);

        Assert.Throws<UnauthorizedAccessException>(() => productService.UpdateProduct(2, product));
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
        var mockProduct = new Mock<IProductRepository>();
        var mockCloudinary = new Mock<ICloudinaryRepository>();
        mockProduct.Setup(p => p.GetProductBy(2)).Returns(product);
        mockProduct.Setup(p => p.DeleteProduct(2)).Returns(true);
        var productService = new ProductService(mockProduct.Object, mockCloudinary.Object);

        var result = productService.DeleteProduct(2, "a1");

        Assert.True(result);
    }

    [Fact]
    public void DeleteProduct_InvalidId_ThrowsArgumentException()
    {
        var mockProduct = new Mock<IProductRepository>();
        var mockCloudinary = new Mock<ICloudinaryRepository>();
        mockProduct.Setup(p => p.GetProductBy(2)).Returns(null as Product);
        var productService = new ProductService(mockProduct.Object, mockCloudinary.Object);
        
        Assert.Throws<ArgumentException>(() => productService.DeleteProduct(2, "a1"));
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
        var mockProduct = new Mock<IProductRepository>();
        var mockCloudinary = new Mock<ICloudinaryRepository>();
        mockProduct.Setup(p => p.GetProductBy(2)).Returns(product);
        var productService = new ProductService(mockProduct.Object, mockCloudinary.Object);

        Assert.Throws<UnauthorizedAccessException>(() => productService.DeleteProduct(2, "a2"));
    }
}