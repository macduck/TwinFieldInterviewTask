using System;
using System.Collections.Generic;
using System.Text;

namespace TwinfieldTask
{
    public class ProductVolumeDiscountRule
    {
        public string ProductCode { get; set; }
        
        public /*see assumtions*/ int VolumeQuantity { get; set; }
        public decimal VolumePrice { get; set; }
    }
}
