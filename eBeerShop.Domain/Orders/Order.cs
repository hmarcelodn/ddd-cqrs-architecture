using eBeerShop.Domain.Beers;
using eBeerShop.Domain.Customers;
using eBeerShop.Domain.Shared;
using System;
using System.Collections.Generic;

namespace eBeerShop.Domain.Orders
{
    public class Order : 
        EntityBase, 
        IAggregateRoot
    {
        public bool CanBeSaved => throw new NotImplementedException();

        protected Order()
        { }

        public Order(Customer customer)
        {
            Buyer = customer.TakeSnapshot();
            State = OrderStateType.PENDING;
            Items = new List<OrderItem>();
            Date = DateTime.Now.Date;
            Total = 0;
        }

        public CustomerSnapshot Buyer { get; private set; }

        public OrderStateType State { get; private set; }

        public DateTime Date { get; private set; }

        public ICollection<OrderItem> Items { get; private set; }

        public decimal Total { get; private set; }

        public void AddOrderLine(Beer beer, int liters)
        {
            Items.Add(new OrderItem(liters, beer, this));
        }

        public void Cancel()
        {
            State = OrderStateType.CANCELLED;
        }

        public void Archive()
        {
            State = OrderStateType.ARCHIVED;
        }

        public bool HasShipped()
        {
            return (State == OrderStateType.SHIPPED);
        }
    }
}
