using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models.Orders
{
    public class Order : BaseEntity<Guid>
    {
        public Order()
        {
            
        }
        public Order(string userEmail, OrderAddress address, ICollection<OrderItem> items, DeliveryMethod deliveryMethod, decimal subTotal , string paymentIntentId , OrderStatus status)
        {
            UserEmail = userEmail;
            Address = address;
            Items = items;
            this.deliveryMethod = deliveryMethod;
            SubTotal = subTotal;
            PaymentIntentId = paymentIntentId;
            Status = status;
        }

        public string UserEmail { get; set; } = null!;

        public DateTimeOffset timeOffset { get; set; } = DateTimeOffset.Now;

        public OrderAddress Address { get; set; } = null!;

        public OrderStatus Status { get; set; }

        public ICollection<OrderItem> Items { get; set; } = [];

        public DeliveryMethod deliveryMethod { get; set; } = null!;
        [ForeignKey("deliveryMethod")]
        public int deliveryMethodId { get; set; }

        public decimal SubTotal { get; set; }

        public decimal GetTotal() => SubTotal + deliveryMethod.Price;

        public string PaymentIntentId { get; set; }
    }
}
