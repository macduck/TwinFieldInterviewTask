using System;
using System.Collections.Generic;
using System.Text;

namespace TwinfieldTask
{
    public interface IProductVolumeDiscountRulesRepository
    {
        ProductVolumeDiscountRule FindByProductCode(string productCode);
    }
}
