using Core.Models;
using Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using S3DB_Individual_Project_Tony.CustomFilter;
using S3DB_Individual_Project_Tony.ViewModels;
using System.Security.Claims;
using S3DB_Individual_Project_Tony.RequestModels;

namespace S3DB_Individual_Project_Tony.Controllers;

[Authorize(Roles = "Admin, Seller")]
[ServiceFilter(typeof(CustomExceptionFilter))]
[ApiController]
[Route("[controller]")]
public class ProductController : ControllerBase
{
    private readonly ProductService _service;

    public ProductController(ProductService productService)
    {
        _service = productService;
    }

    [HttpGet("")]
    public ActionResult Get()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        var products = (List<Product>)_service.GetProductsBy(userId);
        var productsViewModel = new List<ProductViewModel>();
        foreach (var product in products)
            productsViewModel.Add(new ProductViewModel
            {
                Id = product.ID,
                Name = product.Name,
                Price = product.Price,
                Description = product.Description
            });
        return Ok(productsViewModel);
    }

    [AllowAnonymous]
    [HttpGet("paged")]
    public ActionResult Get(int currentPage, int amount)
    {
        var products = (List<Product>)_service.GetPageProducts(currentPage, amount);
        var productsViewModel = new List<ProductViewModel>();
        var productCount = _service.GetProductCount();
        foreach (var product in products)
            productsViewModel.Add(new ProductViewModel
            {
                Id = product.ID,
                Name = product.Name,
                Price = product.Price,
                Description = product.Description
            });
        var response = new
        {
            Products = productsViewModel,
            ProductCount = productCount
        };
        return Ok(response);
    }

    [HttpGet("{id:int}")]
    public ActionResult Get(int id)
    {
        var product = _service.GetProductBy(id);
        var productViewModel = new ProductViewModel
        {
            Id = product.ID,
            Name = product.Name,
            Price = product.Price,
            Description = product.Description
        };
        return Ok(productViewModel);
    }

    [AllowAnonymous]
    [HttpGet("{id:int}/details")]
    public ActionResult GetDetails(int id)
    {
        var product = _service.GetProductWithReviewsBy(id);
        var productWithReviewsViewModel = new ProductWithReviewsViewModel
        {
            Id = product.ID,
            Name = product.Name,
            Price = product.Price,
            Description = product.Description,
            AverageRating = product.AverageRating,
            Reviews = product.Reviews.Select(r => new ReviewViewModel
            {
                Rating = r.Rating,
                Comment = r.Comment,
                ProductId = r.ProductId,
            }).ToList(),
        };
        return Ok(productWithReviewsViewModel);
    }

    [HttpPost("")]
    public ActionResult Post([FromBody] ProductRequest productRequest)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        var product = new Product
        {
            Name = productRequest.Name,
            Price = productRequest.Price,
            Description = productRequest.Description,
            AccountId = userId,
        };
        return Ok(_service.CreateProduct(product));
    }

    [HttpPut("{id:int}")]
    public ActionResult Put(int id, [FromBody] ProductRequest productRequest)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        var product = new Product
        {
            Name = productRequest.Name,
            Price = productRequest.Price,
            Description = productRequest.Description,
            AccountId = userId,
        };
        return Ok(_service.UpdateProduct(id, product));
    }

    [HttpDelete("{id:int}")]
    public ActionResult Delete(int id)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        return Ok(_service.DeleteProduct(id, userId));
    }
}