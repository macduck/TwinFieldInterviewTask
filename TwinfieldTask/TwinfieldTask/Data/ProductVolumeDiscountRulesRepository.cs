using System;
using System.Collections.Generic;
using System.Text;

namespace TwinfieldTask.Data
{
    public class ProductVolumeDiscountRulesRepository: IProductVolumeDiscountRulesRepository
    {
        IDictionary<string, ProductVolumeDiscountRule> _volumeDiscountsRulesRepository = new Dictionary<string, ProductVolumeDiscountRule>
        {
            { "A", new ProductVolumeDiscountRule {ProductCode = "A", VolumeQuantity = 3, VolumePrice = 3M, } },
            { "C", new ProductVolumeDiscountRule {ProductCode = "C", VolumeQuantity = 6, VolumePrice = 5M, } },
        };

        public ProductVolumeDiscountRule FindByProductCode(string productCode)
        {
            return _volumeDiscountsRulesRepository.TryGetValue(productCode, out var rule) ? rule : null;

        }
    }
}
