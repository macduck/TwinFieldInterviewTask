using System;
using System.Collections.Generic;
using System.Linq;

namespace TwinfieldTask.ApiVariant1
{
    public class PointOfSaleTerminal
    {
        List<string> _scannedProducts = new List<string>();
        private ProductsPricingService _pricingService;

        public double CalculateTotal()
        {
            var orderedProducts = _scannedProducts.GroupBy(c => c).ToDictionary(i => i.Key, i => i.Count());
            return (double) orderedProducts.Sum(i => _pricingService.GetOrderItemTotal(i.Key, i.Value));
        }

        public void Scan(string productCode)
        {
            _scannedProducts.Add(productCode);
        }

        public void SetPricing(ProductsPricingService productsPricingService)
        {
            _pricingService = productsPricingService;
        }
    }
}
