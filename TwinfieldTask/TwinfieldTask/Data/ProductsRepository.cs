using System;
using System.Collections.Generic;
using System.Text;

namespace TwinfieldTask.Data
{
    public class ProductsRepository : IProductsRepository
    {
        IDictionary<string, Product> _productsRepository = new Dictionary<string, Product>
        {
            {"A", new Product { Code = "A", Price = 1.25M } },
            {"B", new Product { Code = "B", Price = 4.25M } },
            {"C", new Product { Code = "C", Price = 1.0M } },
            {"D", new Product { Code = "D", Price = 0.75M } }
        };

        public Product GetByProductCode(string productCode)
        {
            return _productsRepository[productCode];
        }
    }
}
