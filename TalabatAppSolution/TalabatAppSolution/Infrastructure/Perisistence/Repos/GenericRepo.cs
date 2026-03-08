using DomainLayer.ContractsRepoInterfaces;
using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;
using Perisistence.HelperFunctions;
using Perisistence.Store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Perisistence.Repos
{
    public class GenericRepo<TEntity, TKey>(StoreDbContext context) : IGenericRepo<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        public async Task<IEnumerable<TEntity>> GetAllAsync() => await context.Set<TEntity>().ToListAsync();

        public async Task<TEntity> GetByIdAsync(TKey id) => await context.Set<TEntity>().FindAsync(id);
        public void Add(TEntity entity) => context.Set<TEntity>().Add(entity);
        
        public void Update(TEntity entity) => context.Set<TEntity>().Update(entity);
        public void Delete(TEntity entity) => context.Set<TEntity>().Remove(entity);

        public async Task<TEntity> GetByIdSpecificationsAsync(ISpecification<TEntity, TKey> specification)
        {
            return await SpecificationEvaluator.CreateQuery(context.Set<TEntity>(), specification).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAllWithSpecificationsAsync(ISpecification<TEntity, TKey> specification)
        {
            return await SpecificationEvaluator.CreateQuery(context.Set<TEntity>(), specification).ToListAsync();
        }

        public async Task<int> GetCountWithSpecificationsAsync(ISpecification<TEntity, TKey> specification)
        {
            return await SpecificationEvaluator.CreateQuery(context.Set<TEntity>(), specification).CountAsync();
        }
    }
}
