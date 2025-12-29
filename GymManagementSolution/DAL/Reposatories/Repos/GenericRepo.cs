using DAL.Data;
using DAL.Models;
using DAL.Reposatories.Interfaces;
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
            dbContext.SaveChanges();
        }

        public void Delete(int id)
        {
            var chickEntity = dbContext.Set<Entity>().Find(id);
            if (chickEntity is null)
                return;
            dbContext.Set<Entity>().Remove(chickEntity);
            dbContext.SaveChanges();
        }

        public IEnumerable<Entity> GetAll() => dbContext.Set<Entity>().ToList();
                        

        public Entity? GetById(int id)
        {
            var entity = dbContext.Set<Entity>().FirstOrDefault(e => e.Id == id);
            return entity;
        }

        public void Update(Entity entity)
        {
            dbContext.Set<Entity>().Update(entity);
            dbContext.SaveChanges();
        }
    }
}
