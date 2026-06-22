using ApplicationLayer.Entities;
using ApplicationLayer.Exceptions.AuthenticationExceptions;
using ApplicationLayer.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Reposetories
{
    internal class GenericReposetory<TEntity>(HealthCareDbContext context) : IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        public async Task<IEnumerable<TEntity>> GetAllAsync() => await context.Set<TEntity>().ToListAsync();

        public async Task<TEntity> GetByIdAsync(int? id)
        {
            var entity = await context.Set<TEntity>().FindAsync(id.Value);
            return entity is null ? throw new UserNotFoundException($"User with id {id} not found.") : entity;
        }


        public async Task AddAsync(TEntity entity) => await context.Set<TEntity>().AddAsync(entity);

        public void Delete(int? id)
        {
            var chick = context.Set<TEntity>().Find(id);
            if(chick is null)
            {
                throw new UserNotFoundException($"User with id {id} not found.");
            }
            context.Set<TEntity>().Remove(chick);
        }

        

        public void Update(TEntity entity)
        {
            var chick = context.Set<TEntity>().Find(entity.Id);
            if(chick is null)
            {
                throw new UserNotFoundException($"User with id {entity.Id} not found.");
            }
            context.Set<TEntity>().Update(entity);
        }
    }
}
