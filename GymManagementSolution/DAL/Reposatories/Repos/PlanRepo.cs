using DAL.Data;
using DAL.Models;
using DAL.Reposatories.InterfacesRepos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Reposatories.Repos
{
    public class PlanRepo : IPlan
    {
        private readonly GymDbContext dbContext;

        public PlanRepo(GymDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public IEnumerable<Plan> GetAll() => dbContext.Set<Plan>().ToList();

        public Plan? GetById(int id)
        {
            var entity = dbContext.Set<Plan>().FirstOrDefault(e => e.Id == id);
            return entity;
        }

        public void Update(Plan entity)
        {
            dbContext.Set<Plan>().Update(entity);
            dbContext.SaveChanges();
        }
    }
}
