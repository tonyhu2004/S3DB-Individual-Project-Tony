﻿using Core.Models;
using Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using S3DB_Individual_Project_Tony.CustomFilter;
using S3DB_Individual_Project_Tony.ViewModels;
using System.Data.SqlClient;
using System.IO;

namespace S3DB_Individual_Project_Tony.Controllers
{
    [Authorize]
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
            List<Product> products = (List<Product>)_service.GetProducts();
            List<ProductViewModel> productsViewModel = new List<ProductViewModel>();
            foreach (Product product in products)
            {
                productsViewModel.Add(new ProductViewModel
                {
                    Id = product.ID,
                    Name = product.Name,
                    Price = product.Price,
                    Description = product.Description,
                });
            }
            return Ok(productsViewModel);
        }

        [AllowAnonymous]
        [HttpGet("paged")] 
        public ActionResult Get(int currentPage, int amount)
        {
            List<Product> products = (List<Product>)_service.GetPageProducts(currentPage, amount);
            List<ProductViewModel> productsViewModel = new List<ProductViewModel>();
            int productCount = _service.GetProductCount();
            foreach (Product product in products)
            {
                productsViewModel.Add(new ProductViewModel
                {
                    Id = product.ID,
                    Name = product.Name,
                    Price = product.Price,
                    Description = product.Description,
                });
            }
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
            Product? product = _service.GetProductBy(id);
            ProductViewModel productViewModel = new ProductViewModel
            {
                Id = product.ID,
                Name = product.Name,
                Price = product.Price,
                Description = product.Description,
            };
            return Ok(productViewModel);
        }


        [HttpPost("")]
        public ActionResult Post([FromBody] ProductViewModel productViewModel)
        {
            Product product = new Product
            {
                Name = productViewModel.Name,
                Price = productViewModel.Price,
                Description = productViewModel.Description,
            };
            return Ok(_service.CreateProduct(product));
        }

        [HttpPut("{id:int}")]
        public ActionResult Put(int id, [FromBody] ProductViewModel productViewModel)
        {
            Product product = new Product
            {
                Name = productViewModel.Name,
                Price = productViewModel.Price,
                Description = productViewModel.Description,
            };
            return Ok(_service.UpdateProduct(id, product));
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            return Ok(_service.DeleteProduct(id));
        }
    }
}
