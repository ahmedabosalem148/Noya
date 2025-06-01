using System;
using System.Collections.Generic;

namespace Noya.Models
{
    public class CheckoutRequest
    {
        public int UserId { get; set; }
        public string ShippingAddress { get; set; }
        public List<CheckoutItem> Items { get; set; }
    }

    public class CheckoutItem
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
} 