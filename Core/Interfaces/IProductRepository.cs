using Core.Models;

namespace Core.Interfaces;

public interface IProductRepository
{
    IEnumerable<Product> GetProductsBy(string userId);
    IEnumerable<Product> GetPageProducts(int lastProduct, int amount);
    Product? GetProductBy(int id);
    int GetProductCount();
    bool CreateProduct(Product product);
    bool UpdateProduct(int id, Product product);
    bool DeleteProduct(int id);
}