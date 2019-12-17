using System;
using System.Collections.Generic;
using System.Text;
using TwinfieldTask.Data;

namespace TwinfieldTask.ApiVariant1
{
    public class ProductsPricingService
    {
        private readonly ProductsRepository _productsRepository;
        private readonly ProductVolumeDiscountRulesRepository _volumeDiscountsRulesRepository;

        public ProductsPricingService()
        {
            _productsRepository = new ProductsRepository();
            _volumeDiscountsRulesRepository = new ProductVolumeDiscountRulesRepository();
        }

        private decimal GetProductUnitPrice(string productCode)
        {
            return _productsRepository.GetByProductCode(productCode).Price;
        }

        public decimal GetOrderItemTotal(string productCode, int quantity)
        {
            var rule = _volumeDiscountsRulesRepository.FindByProductCode(productCode);
            if (rule == null)
            {
                return GetProductUnitPrice(productCode) * quantity;
            }


            var unitPriceQuantity = quantity % rule.VolumeQuantity;
            var volumePriceQuantity = quantity - unitPriceQuantity;
            return unitPriceQuantity * GetProductUnitPrice(productCode) 
                + (volumePriceQuantity / rule.VolumeQuantity) * rule.VolumePrice;

        }
    }
}
