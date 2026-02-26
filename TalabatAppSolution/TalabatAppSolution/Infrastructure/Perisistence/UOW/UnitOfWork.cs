using DomainLayer.ContractsRepoInterfaces;
using DomainLayer.Models;
using DomainLayer.UOW;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Perisistence.Repos;
using Perisistence.Store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Perisistence.UOW
{
    public class UnitOfWork(StoreDbContext context) : IUnitOfWork
    {
        private readonly StoreDbContext _context = context;

        private readonly Dictionary<string, object> repos = [];
        public IGenericRepo<TEntity, TKey> GetRepo<TEntity, TKey>() where TEntity : BaseEntity<TKey> 
        {
            var TypeName = typeof(TEntity).Name;

            if(repos.ContainsKey(TypeName))
            {
                return (IGenericRepo<TEntity , TKey>) repos[TypeName];
            }
            else
            {
                var repo = new GenericRepo<TEntity, TKey>(_context);
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
