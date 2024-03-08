using Core.Interfaces;
using Core.Models;
using DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ProductRepository(DbContextOptions<ApplicationDbContext> options)
        {
            _dbContext = new ApplicationDbContext(options);
        }

        public IEnumerable<Product> GetProducts()
        {
            var products = _dbContext.Products
                    .ToList();
            return products;
        }

        public Product? GetProductBy(int id)
        {
            var product = _dbContext.Products.FirstOrDefault(p => p.ID == id);
            return product;
        }

        public bool CreateProduct(Product product)
        {
            _dbContext.Products.Add(product);
            _dbContext.SaveChanges();
            return true;
        }

        public bool UpdateProduct(int id, Product product)
        {
            var entity = _dbContext.Products.FirstOrDefault(p => p.ID == id);
            if (entity == null)
            {
                return false;
            }

            entity.Name = product.Name;
            entity.Price = product.Price;
            entity.Description = product.Description;
            if (product.Productinformation != null)
            {
                entity.Productinformation = product.Productinformation;
            }
            if (product.Reviews != null)
            {
                entity.Reviews = product.Reviews;
            }

            _dbContext.SaveChanges();
            return true;
        }

        public bool DeleteProduct(int id)
        {
            var entity = _dbContext.Products.FirstOrDefault(p => p.ID == id);
            if (entity == null)
            {
                return false;
            }

            _dbContext.Products.Remove(entity);
            _dbContext.SaveChanges();
            return true;
        }
    }
}
