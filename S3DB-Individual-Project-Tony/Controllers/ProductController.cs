using Core.Models;
using Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using S3DB_Individual_Project_Tony.CustomFilter;
using S3DB_Individual_Project_Tony.ViewModels;
using System.Security.Claims;

namespace S3DB_Individual_Project_Tony.Controllers;

[Authorize(Roles = "Admin")]
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

        var products = (List<Product>)_service.GetProducts();
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

    [AllowAnonymous]
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


    [HttpPost("")]
    public ActionResult Post([FromBody] ProductViewModel productViewModel)
    {
        var product = new Product
        {
            Name = productViewModel.Name,
            Price = productViewModel.Price,
            Description = productViewModel.Description
        };
        return Ok(_service.CreateProduct(product));
    }

    [HttpPut("{id:int}")]
    public ActionResult Put(int id, [FromBody] ProductViewModel productViewModel)
    {
        var product = new Product
        {
            Name = productViewModel.Name,
            Price = productViewModel.Price,
            Description = productViewModel.Description
        };
        return Ok(_service.UpdateProduct(id, product));
    }

    [HttpDelete("{id:int}")]
    public ActionResult Delete(int id)
    {
        return Ok(_service.DeleteProduct(id));
    }
}