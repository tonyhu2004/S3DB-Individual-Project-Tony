using System.Runtime.InteropServices;
using Core.Interfaces;
using Core.Models;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;

namespace Core.Services;

public class ProductService
{
    private readonly IProductRepository _productRepository;
    private readonly ICloudinaryRepository _cloudinaryRepository;

    public ProductService(IProductRepository productRepository, ICloudinaryRepository cloudinaryRepository)
    {
        _productRepository = productRepository;
        _cloudinaryRepository = cloudinaryRepository;
    }

    public IEnumerable<Product> GetProductsBy(string userId)
    {
        var products = _productRepository.GetProductsBy(userId).ToList();
        products.ForEach(p => p.ImageUrl = _cloudinaryRepository.GetImageUrl(p.Name + p.Id));
        return products;
    }

    public IEnumerable<Product> GetPageProducts(int currentPage, int amount)
    {
        var lastProduct = (currentPage - 1) * amount;
        var products = _productRepository.GetPageProducts(lastProduct, amount).ToList();
        products.ForEach(p => p.ImageUrl = _cloudinaryRepository.GetImageUrl(p.Name + p.Id));
        return products;
    }

    public int GetProductCount()
    {
        return _productRepository.GetProductCount();
    }

    public Product GetProductBy(int id)
    {
        var existingProduct = _productRepository.GetProductBy(id);
        if (existingProduct == null) throw new ArgumentException("Product doesn't exist");
        existingProduct.ImageUrl = _cloudinaryRepository.GetImageUrl(existingProduct.Name + existingProduct.Id);
        return existingProduct;
    }
    public Product GetProductWithReviewsBy(int id)
    {
        var existingProduct = _productRepository.GetProductWithReviewsBy(id);
        if (existingProduct == null) throw new ArgumentException("Product doesn't exist");
        if (existingProduct.Reviews != null && existingProduct.Reviews.Count != 0) existingProduct.AverageRating = CalculateAverageRating(existingProduct.Reviews);
        existingProduct.ImageUrl = _cloudinaryRepository.GetImageUrl(existingProduct.Name + existingProduct.Id);
        return existingProduct;
    }    
    
    public bool CreateProduct(Product product)
    {
        if (!IsProductComplete(product)) throw new InvalidOperationException("Product isn't complete");
        product.Id = _productRepository.CreateProduct(product);
        var result = _cloudinaryRepository.UploadImage(product.FormFile!, product.Name + product.Id);
        if (result.Error != null)
        {
            throw new ExternalException("Couldn't add image using cloudinary");
        }
        return true;
    }

    public bool UpdateProduct(int id, Product product)
    {
        if (!IsProductComplete(product)) throw new InvalidOperationException("Product isn't complete");
        var existingProduct = _productRepository.GetProductBy(id);
        if (existingProduct == null) throw new ArgumentException("Product doesn't exist");
        if (existingProduct.UserId != product.UserId) throw new UnauthorizedAccessException("Product does not belong to user");
        var result = _cloudinaryRepository.UpdateImage(product.FormFile!, product.Name + id);
        if (result.Error != null)
        {
            throw new ExternalException("Couldn't edit image using cloudinary");
        }
        return _productRepository.UpdateProduct(id, product);
    }

    public bool DeleteProduct(int id, string userId)
    {
        var existingProduct = _productRepository.GetProductBy(id);
        if (existingProduct == null) throw new ArgumentException("Product doesn't exist");
        if (existingProduct.UserId != userId) throw new UnauthorizedAccessException("Product does not belong to user");
        var deleteResult =  _productRepository.DeleteProduct(id);
        var result = _cloudinaryRepository.DeleteImage(existingProduct.Name + id);
        if (result.Error != null)
        {
            throw new ExternalException("Couldn't delete image using cloudinary");
        }
        return deleteResult;
    }

    private static bool IsProductComplete(Product product)
    {
        if (string.IsNullOrWhiteSpace(product.Name) || product.Price <= 0 ||
            string.IsNullOrWhiteSpace(product.Description) || string.IsNullOrWhiteSpace(product.UserId) || product.FormFile == null) return false;
        return true;
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
}