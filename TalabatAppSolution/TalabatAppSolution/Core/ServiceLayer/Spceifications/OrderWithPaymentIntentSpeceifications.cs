using DomainLayer.Models.Orders;
using Stripe.Climate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.Models.Orders;
using Order = DomainLayer.Models.Orders.Order;
namespace ServiceLayer.Spceifications
{
    internal class OrderWithPaymentIntentSpeceifications : BasicSpecification<Order, Guid>
    {
        public OrderWithPaymentIntentSpeceifications(string IntentId) : base(O => O.PaymentIntentId == IntentId)
        {
        }
    }
}
