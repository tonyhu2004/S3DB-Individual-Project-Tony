using Core.Interfaces;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
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

        public Product GetProductBy(int id)
        {
            return _repository.GetProductBy(id);
        }

        public bool CreateProduct(Product product)
        {
            _repository.CreateProduct(product);
            return true;
        }

        public bool UpdateProduct(int id, Product product)
        {
            return _repository.UpdateProduct(id, product);
        }

        public bool DeleteProduct(int id)
        {
            return _repository.DeleteProduct(id);
        }
    }
}
