using System;

namespace TwinfieldTask.ApiVariant2
{
    public class VolumeDiscountCalculationStrategy : IOrderItemDiscountCaculationStrategy
    {
        private readonly IProductVolumeDiscountRulesRepository _rulesRepository;

        public VolumeDiscountCalculationStrategy(IProductVolumeDiscountRulesRepository rulesRepository)
        {
            _rulesRepository = rulesRepository;
        }

        public decimal CalculateDiscount(string productCode, decimal unitPrice, int quantity)
        {
            var rule = _rulesRepository.FindByProductCode(productCode);
            if (rule == null)
            {
                return 0M;
            }

            var discountedQuantity = quantity - quantity % rule.VolumeQuantity;
            var volumes = discountedQuantity / rule.VolumeQuantity;

            return discountedQuantity * unitPrice - volumes * rule.VolumePrice;
        }
    }
}