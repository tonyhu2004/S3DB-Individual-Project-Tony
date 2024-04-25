using Core.Interfaces;
using Core.Models;

namespace Core.Services;

public class ProductService
{
    private readonly IProductRepository _repository;

    public ProductService(IProductRepository productRepository)
    {
        _repository = productRepository;
    }

    public IEnumerable<Product> GetProductsBy(string userId)
    {
        return _repository.GetProductsBy(userId);
    }

    public IEnumerable<Product> GetPageProducts(int currentPage, int amount)
    {
        var lastProduct = (currentPage - 1) * amount;
        return _repository.GetPageProducts(lastProduct, amount);
    }

    public int GetProductCount()
    {
        return _repository.GetProductCount();
    }

    public Product? GetProductBy(int id)
    {
        var existingProduct = _repository.GetProductBy(id);
        if (existingProduct == null) throw new ArgumentException("Product doesn't exist");
        return existingProduct;
    }
    public Product? GetProductWithReviewsBy(int id)
    {
        var existingProduct = _repository.GetProductWithReviewsBy(id);
        if (existingProduct == null) throw new ArgumentException("Product doesn't exist");
        if (existingProduct.Reviews != null && existingProduct.Reviews.Count != 0) existingProduct.AverageRating = CalculateAverageRating(existingProduct.Reviews);
        return existingProduct;
    }    
    private static decimal CalculateAverageRating(List<Review> reviews)
    {
        var averageRating = reviews.Count != 0 ? reviews.Average(r => r.Rating) : 0 ;
        
        averageRating = Math.Round(averageRating, 2);
        var decimalAsString = averageRating.ToString();
        var trimmedDecimal = decimalAsString.TrimEnd('0');
        averageRating = decimal.Parse(trimmedDecimal);
        
        return averageRating;
    }

    public bool CreateProduct(Product product)
    {
        if (!IsProductComplete(product)) throw new InvalidOperationException("Product isn't complete");
        return _repository.CreateProduct(product);
    }

    public bool UpdateProduct(int id, Product product)
    {
        if (!IsProductComplete(product)) throw new InvalidOperationException("Product isn't complete");
        var existingProduct = _repository.GetProductBy(id);
        if (existingProduct == null) throw new ArgumentException("Product doesn't exist");
        if (existingProduct.UserId != product.UserId) throw new UnauthorizedAccessException("Product does not belong to user");
        return _repository.UpdateProduct(id, product);
    }

    public bool DeleteProduct(int id, string userId)
    {
        var existingProduct = _repository.GetProductBy(id);
        if (existingProduct == null) throw new ArgumentException("Product doesn't exist");
        if (existingProduct.UserId != userId) throw new UnauthorizedAccessException("Product does not belong to user");

        return _repository.DeleteProduct(id);
    }

    private static bool IsProductComplete(Product product)
    {
        if (string.IsNullOrWhiteSpace(product.Name) || product.Price <= 0 ||
            string.IsNullOrWhiteSpace(product.Description) || string.IsNullOrWhiteSpace(product.UserId)) return false;
        return true;
    }
}