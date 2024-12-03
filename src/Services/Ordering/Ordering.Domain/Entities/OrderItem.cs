﻿using Ordering.Domain.Abstractions;
using Ordering.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Domain.Entities
{
    public class OrderItem : Entity<OrderItemId>
    {
        internal OrderItem(OrderId orderId,ProductId productId,int quantity,decimal price)
        {
            Id = OrderItemId.Of(Guid.NewGuid());
            OrderId= orderId;
            ProductId= productId;
            Quantity= quantity;
            Price= price;
        }
        public OrderId OrderId { get; private set; }
        public ProductId ProductId { get; private set; }
        public int Quantity { get;private  set; }
        public decimal Price { get; private set; }
    }
}