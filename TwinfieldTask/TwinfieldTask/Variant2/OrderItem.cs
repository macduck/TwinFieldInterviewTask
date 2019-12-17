using System;
using System.Collections.Generic;
using System.Text;

namespace TwinfieldTask.ApiVariant2
{
    public class OrderItem
    {
        public string ProductCode { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public decimal SubTotal => UnitPrice * Quantity;
        public decimal Discount { get; set; }
        public decimal Total => SubTotal - Discount;
    }
}
