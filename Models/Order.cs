using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using EsapApi.Enums;

namespace EsapApi.Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }

        public int UserId { get; set; }
        public User User { get; set; } // Navigation property

        public DateTime OrderDate { get; set; } = DateTime.Now;

        public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

        [Required]
        public OrderStatus Status { get; set; } // Enum for order status

        // Payment details
        public string PaymentMethod { get; set; } // e.g., Credit Card, PayPal
        public string PaymentTransactionId { get; set; } // Unique identifier from the payment gateway
        public DateTime? PaymentDate { get; set; } // Nullable in case the payment is pending

        // Shipping details (optional)
        public string ShippingAddress { get; set; }
        public DateTime? EstimatedDeliveryDate { get; set; }
        public string TrackingNumber { get; set; }
    }

  
}
