using DomainLayer.ContractsRepoInterfaces;
using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Spceifications
{
    public abstract class BasicSpecification<TEntity, TKey> : ISpecification<TEntity, TKey>
        where TEntity : BaseEntity<TKey>
    {
        public Expression<Func<TEntity, bool>> Criteria { get; private set; }

        public List<Expression<Func<TEntity, object>>> Includes { get; } = [];

        public Expression<Func<TEntity, object>>? OrderBy { get; private set; }

        public Expression<Func<TEntity, object>>? OrderByDescending { get; private set; }

        public int Take { get; private set; }

        public int Skip { get; private set; }

        public bool IsPagingEnabled { get; set; }

        protected BasicSpecification(Expression<Func<TEntity, bool>> _Criteria)
        {
            Criteria = _Criteria;
        }
        protected void AddInclude(Expression<Func<TEntity, object>> includeExpression)
        {
            Includes.Add(includeExpression);
        }

        protected void AddOrderBy(Expression<Func<TEntity, object>> orderByExpression)
        {
                OrderBy = orderByExpression;
        }
        protected void AddOrderByDescending(Expression<Func<TEntity, object>> orderByDescendingExpression)
        {
                OrderByDescending = orderByDescendingExpression;
        }

        protected void ApplyPaginaging(int PageIndex , int PageSize)
        {
            IsPagingEnabled = true;
            Take = PageSize;
            Skip = (PageIndex - 1) * PageSize;
        }
    }
}
