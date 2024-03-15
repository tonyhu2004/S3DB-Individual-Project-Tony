using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IProductRepository
    {
        IEnumerable<Product>GetProducts ();
        IEnumerable<Product>GetPageProducts (int lastProduct, int amount);
        Product? GetProductBy(int id);
        int GetProductCount ();
        bool CreateProduct(Product product);
        bool UpdateProduct(int id, Product product);
        bool DeleteProduct(int id);
    }
}
