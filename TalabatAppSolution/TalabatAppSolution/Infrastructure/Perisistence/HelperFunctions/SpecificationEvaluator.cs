using DomainLayer.ContractsRepoInterfaces;
using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Perisistence.HelperFunctions
{
    public static class SpecificationEvaluator
    {
        public static IQueryable<TEntity> CreateQuery<TEntity,Tkey>(IQueryable<TEntity> BaseQuery, ISpecification<TEntity,Tkey> specification)
               where TEntity : BaseEntity<Tkey>
        {
            var Query = BaseQuery;

            if (specification.Criteria is not null)
            {
                Query = BaseQuery.Where(specification.Criteria);
            }
            if(specification.OrderBy is not null)
            {
                Query = Query.OrderBy(specification.OrderBy);
            }
            if(specification.OrderByDescending is not null)
            {
                Query = Query.OrderByDescending(specification.OrderByDescending);
            }
            if (specification.IsPagingEnabled)
            {
                Query = Query.Skip(specification.Skip).Take(specification.Take);
            }
            if(specification.Includes is not null && specification.Includes.Any())
            {
                Query = specification.Includes.Aggregate(Query, (current, include) => current.Include(include));
            }

            return Query;
        }
    }
}
