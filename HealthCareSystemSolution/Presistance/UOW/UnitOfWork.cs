using ApplicationLayer.Entities;
using ApplicationLayer.Interfaces.Repositories;
using ApplicationLayer.Interfaces.UOW;
using Persistence.Context;
using Persistence.Reposetories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.UOW
{
    public class UnitOfWork(HealthCareDbContext context) : IUnitOfWork
    {
        private readonly HealthCareDbContext _context = context;

        private readonly Dictionary<string, object> repos = [];

        public IGenericRepository<TEntity> GetRepo<TEntity>() where TEntity : BaseEntity
        {
            var TypeName = typeof(TEntity).Name;

            if (repos.ContainsKey(TypeName))
            {
                return (IGenericRepository<TEntity>)repos[TypeName];
            }
            else
            {
                var repo = new GenericReposetory<TEntity>(_context);
                repos.Add(TypeName, repo);
                return repo;
            }
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
