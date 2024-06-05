using Core.Interfaces;
using Core.Models;
using DataAccess.Data;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly ApplicationDbContext _dbContext;

    public ProductRepository(DbContextOptions<ApplicationDbContext> options)
    {
        _dbContext = new ApplicationDbContext(options);
    }

    public IEnumerable<Product> GetProductsBy(string userId)
    {
        var products = _dbContext.Products
            .Where(p => p.UserId == userId)
            .ToList();
        return products;
    }

    public IEnumerable<Product> GetPageProducts(int lastProduct, int amount)
    {
        var products = _dbContext.Products
            .OrderBy(p => p.Id)
            .Skip(lastProduct)
            .Take(amount)
            .ToList();
        return products;
    }

    public int GetProductCount()
    {
        return _dbContext.Products
            .Count();
    }

    public Product? GetProductBy(int id)
    {
        var product = _dbContext.Products.FirstOrDefault(p => p.Id == id);
        return product;
    }

    public Product? GetProductWithReviewsBy(int id)
    {
        var product = _dbContext.Products
            .Include(p => p.Reviews)
            .Include(p => p.User)
            .FirstOrDefault(p => p.Id == id);

        if (product?.Reviews != null && product.Reviews.Count != 0)
            _dbContext.Entry(product).Collection(p => p.Reviews!).Query().Include(r => r.User).Load();

        return product;
    }

    public int CreateProduct(Product product)
    {
        _dbContext.Products.Add(product);
        _dbContext.SaveChanges();
        return product.Id;
    }

    public bool UpdateProduct(int id, Product product)
    {
        var entity = _dbContext.Products.FirstOrDefault(p => p.Id == id);
        if (entity == null) return false;

        entity.Name = product.Name;
        entity.Price = product.Price;
        entity.Description = product.Description;
        if (product.ProductInformation != null) entity.ProductInformation = product.ProductInformation;
        if (product.Reviews != null) entity.Reviews = product.Reviews;

        _dbContext.SaveChanges();
        return true;
    }

    public bool DeleteProduct(int id)
    {
        var entity = _dbContext.Products.FirstOrDefault(p => p.Id == id);
        if (entity == null) return false;

        _dbContext.Products.Remove(entity);
        _dbContext.SaveChanges();
        return true;
    }
}