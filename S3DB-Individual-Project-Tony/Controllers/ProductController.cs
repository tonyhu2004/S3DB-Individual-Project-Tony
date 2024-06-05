using System.Security.Claims;
using Core.Models;
using Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using S3DB_Individual_Project_Tony.CustomFilter;
using S3DB_Individual_Project_Tony.RequestModels;
using S3DB_Individual_Project_Tony.ViewModels;

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

        var products = (List<Product>)_service.GetProductsBy(userId!);
        var productsViewModel = new List<ProductViewModel>();
        foreach (var product in products)
            productsViewModel.Add(new ProductViewModel
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Description = product.Description,
                ImageUrl = product.ImageUrl!
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
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Description = product.Description,
                ImageUrl = product.ImageUrl!
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
            Id = product!.Id,
            Name = product.Name,
            Price = product.Price,
            Description = product.Description,
            ImageUrl = product.ImageUrl!
        };
        return Ok(productViewModel);
    }

    [AllowAnonymous]
    [HttpGet("{id:int}/details")]
    public ActionResult GetDetails(int id)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var product = _service.GetProductWithReviewsBy(id);

        var productWithReviewsViewModel = new ProductWithReviewsViewModel
        {
            Id = product.Id,
            Name = product.Name,
            Price = product.Price,
            Description = product.Description,
            AverageRating = product.AverageRating,
            CurrentUserId = userId ?? "",
            UserId = product.UserId,
            Username = product.User?.Email?.Substring(0, product.User.Email.IndexOf('@')) ?? "",
            ImageUrl = product.ImageUrl!,
            Reviews = product.Reviews?.Select(r => new ReviewViewModel
            {
                Id = r.Id,
                Rating = r.Rating,
                Comment = r.Comment,
                UserId = r.User!.Id,
                Username = r.User.Email!.Substring(0, r.User.Email.IndexOf('@'))
            }).ToList()
        };
        return Ok(productWithReviewsViewModel);
    }

    [HttpPost("")]
    public ActionResult Post([FromForm] ProductRequest productRequest)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        var product = new Product
        {
            Name = productRequest.Name,
            Price = productRequest.Price,
            Description = productRequest.Description,
            UserId = userId!,
            FormFile = productRequest.FormFile
        };
        return Ok(_service.CreateProduct(product));
    }

    [HttpPut("{id:int}")]
    public ActionResult Put(int id, [FromForm] ProductRequest productRequest)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        var product = new Product
        {
            Name = productRequest.Name,
            Price = productRequest.Price,
            Description = productRequest.Description,
            UserId = userId!,
            FormFile = productRequest.FormFile
        };
        return Ok(_service.UpdateProduct(id, product));
    }

    [HttpDelete("{id:int}")]
    public ActionResult Delete(int id)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        return Ok(_service.DeleteProduct(id, userId!));
    }
}