using DAL.Data;
using DAL.Models;
using DAL.Reposatories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Reposatories.Repos
{
    public class GenericRepo<Entity> : IGenericRepo<Entity> where Entity : BaseEntity
    {
        private readonly GymDbContext dbContext;

        public GenericRepo(GymDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public void Add(Entity entity)
        {
            dbContext.Set<Entity>().Add(entity);
        }

        public void Delete(int id)
        {
            var chickEntity = dbContext.Set<Entity>().Find(id);
            if (chickEntity is null)
                return;
            dbContext.Set<Entity>().Remove(chickEntity);
        }

        public IEnumerable<Entity> GetAll(Func<Entity, bool>? condition = null)
        {
            if (condition is null) return dbContext.Set<Entity>().AsNoTracking().ToList();
            else return dbContext.Set<Entity>().AsNoTracking().Where(condition).ToList();
        }

        public Entity? GetById(int id)
        {
            var entity = dbContext.Set<Entity>().FirstOrDefault(e => e.Id == id);
            return entity;
        }

        public void Update(Entity entity)
        {
            dbContext.Set<Entity>().Update(entity);
        }
    }
}
