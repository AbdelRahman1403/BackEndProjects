using DAL.Data;
using DAL.Models;
using DAL.Reposatories.Interfaces;
using DAL.Reposatories.InterfacesRepos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Reposatories.Repos
{
    public class UnitOfWork(GymDbContext _dbContext) : IUnitOfWork
    {
        private readonly GymDbContext dbContext = _dbContext;

        private readonly Dictionary<Type,object> repositories = new();

        public IGenericRepo<TEntity> GetRepository<TEntity>() where TEntity : BaseEntity, new()
        {
            var entityRepo = typeof(TEntity);

            if (repositories.TryGetValue(entityRepo , out var repo))
            {
                return (IGenericRepo<TEntity>)repo;
            }

            var newRepo = new GenericRepo<TEntity>(dbContext);

            repositories[entityRepo] = newRepo;
            return newRepo;
        }

        public int SaveChanges() => dbContext.SaveChanges();
    }
}
