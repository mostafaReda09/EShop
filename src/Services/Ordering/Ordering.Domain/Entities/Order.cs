using Ordering.Domain.Abstractions;
using Ordering.Domain.Enums;
using Ordering.Domain.Events;
using Ordering.Domain.ValueObjects;

namespace Ordering.Domain.Entities
{
    public class Order : Aggregate<OrderId>
    {
        private readonly List<OrderItem> _items = [];
        public IReadOnlyList<OrderItem> Items=>_items.AsReadOnly();
        public CustomerId CustomerId { get; private set; } = default!;
        public Address ShippingAddress { get; private set; } = default!;
        public Address BillingAddress { get; private set; } = default!;
        public Payement Payement { get; private set; } = default!;
        public OrderStatus OrderStatus { get; private set; } = OrderStatus.Pending;
        public decimal TotalPrice => _items.Sum(x => x.Price * x.Quantity);

        public static Order Create(OrderId Id,CustomerId customerId,Address shippingAddress,Address billingAddress,Payement payement)
        {

            var order= new Order
            {
                Id = Id,
                Payement = payement,
                CustomerId = customerId,
                ShippingAddress = shippingAddress,
                BillingAddress = billingAddress,
                OrderStatus = OrderStatus.Pending,

            };

            order.AddDomainEvent(new OrderCreatedEvent(order));
            return order;
        }
        public void Update( Address shippingAddress, Address billingAddress, Payement payement,OrderStatus orderStatus)
        {
           ShippingAddress=shippingAddress;
            BillingAddress=billingAddress;
            Payement=payement;
            OrderStatus=orderStatus;
            AddDomainEvent(new OrderUpdatedEvent(this));

        }
        public void Add(ProductId productId,int quantity,decimal price)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(quantity);
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(price);
            var item = new OrderItem(Id,productId,quantity,price);
            _items.Add(item);
        }

        public void Remove(ProductId productId)
        {
          
            var item = _items.Where(x=>x.ProductId==productId).FirstOrDefault();
            if (item is not null)
            {
                _items.Remove(item);

            }
        }
    }
}
