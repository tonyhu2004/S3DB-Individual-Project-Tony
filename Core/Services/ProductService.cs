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

    public IEnumerable<Product> GetProducts()
    {
        return _repository.GetProducts();
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
        return _repository.UpdateProduct(id, product);
    }

    public bool DeleteProduct(int id)
    {
        var existingProduct = _repository.GetProductBy(id);
        if (existingProduct == null) throw new ArgumentException("Product doesn't exist");
        return _repository.DeleteProduct(id);
    }

    private static bool IsProductComplete(Product product)
    {
        if (string.IsNullOrEmpty(product.Name) || product.Price < 0 ||
            string.IsNullOrEmpty(product.Description)) return false;
        return true;
    }
}