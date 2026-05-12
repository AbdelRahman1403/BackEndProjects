using DomainLayer.ContractsRepoInterfaces;
using DomainLayer.Models.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Spceifications
{
    public class OrderSpecification : BasicSpecification<Order, Guid>
    {
        public OrderSpecification(string Email):base(o => o.UserEmail == Email)
        {
            AddInclude(o => o.deliveryMethod);
            AddInclude(o => o.Items);

            AddOrderByDescending(o => o.timeOffset);
        }

        public OrderSpecification(Guid Id):base(o => o.Id == Id)
        {
            AddInclude(o => o.deliveryMethod);
            AddInclude(o => o.Items);
        }
    }
}
