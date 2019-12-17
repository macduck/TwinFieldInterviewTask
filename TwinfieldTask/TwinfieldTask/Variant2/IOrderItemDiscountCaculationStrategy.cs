using System;
using System.Collections.Generic;
using System.Text;

namespace TwinfieldTask.ApiVariant2
{
    public interface IOrderItemDiscountCaculationStrategy
    {
        decimal CalculateDiscount(string productCode, decimal unitPrice, int quantity);
    }
}
