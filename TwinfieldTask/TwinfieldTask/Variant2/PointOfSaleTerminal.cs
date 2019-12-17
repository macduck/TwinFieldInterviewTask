using System;

namespace TwinfieldTask.ApiVariant2
{
    public class PointOfSaleTerminal
    {
        private readonly Func<OrderingSession> _orderingSessionFactory;

        public PointOfSaleTerminal(Func<OrderingSession> orderingSessionFactory)
        {
            this._orderingSessionFactory = orderingSessionFactory;
        }

        public OrderingSession CurrentSession { get; private set; } 

        public void Scan(string productCode)
        {
            CurrentSession.AddProduct(productCode, quantity: 1);
        }

        public OrderingSession OpenOrderingSession()
        {
            CurrentSession = _orderingSessionFactory();
            return CurrentSession;
        }

        public decimal CalculateTotal()
        {
            return CurrentSession.Total;
        }
    }
}
