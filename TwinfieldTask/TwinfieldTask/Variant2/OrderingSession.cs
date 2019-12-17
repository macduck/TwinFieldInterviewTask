using System;
using System.Collections.Generic;
using System.Linq;
using TwinfieldTask.Data;

namespace TwinfieldTask.ApiVariant2
{
    public class OrderingSession
    {
        public OrderingSession(ProductsRepository productsRepository, IOrderItemDiscountCaculationStrategy orderItemDisountStrategy)
        {
            _productsRepo = productsRepository;
            _orderItemDisountStrategy = orderItemDisountStrategy;
        }

        readonly IProductsRepository _productsRepo;
        private readonly IOrderItemDiscountCaculationStrategy _orderItemDisountStrategy;
        readonly List<OrderItem> _orderItems = new List<OrderItem>();

        public decimal Total { get; internal set; }

        public void AddProduct(string productCode, int quantity = 1)
        {
            var orderItem = GetOrAddOrderItem(productCode);
            orderItem.Quantity += quantity;

            RecalculateOrderItemDisount(orderItem);
            RecalculateOrderTotal();
        }

        private void RecalculateOrderTotal()
        {
            Total = _orderItems.Sum(i => i.Total);
        }

        private void RecalculateOrderItemDisount(OrderItem orderItem)
        {
            var discount = _orderItemDisountStrategy.CalculateDiscount(orderItem.ProductCode, orderItem.UnitPrice, orderItem.Quantity);
            orderItem.Discount = discount;
        }

        private OrderItem GetOrAddOrderItem(string productCode)
        {
            var orderItem = _orderItems.SingleOrDefault(i => i.ProductCode == productCode);
            if (orderItem == null)
            {
                var product = _productsRepo.GetByProductCode(productCode);
                orderItem = new OrderItem { ProductCode = productCode, UnitPrice = product.Price };
                _orderItems.Add(orderItem);
            }

            return orderItem;
        }
    }
}