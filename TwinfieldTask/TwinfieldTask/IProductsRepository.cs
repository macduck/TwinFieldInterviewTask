using System;
using System.Collections.Generic;
using System.Text;

namespace TwinfieldTask
{
    public interface IProductsRepository
    {
        Product GetByProductCode(string productCode);
    }
}
