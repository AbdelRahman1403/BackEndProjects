using DAL.Data;
using DAL.Models;
using DAL.Reposatories.InterfacesRepos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Reposatories.Repos
{
    public class TrainerRepo : GenericRepo<Trainer>, ITrainer
    {
        private readonly GymDbContext dbContext;

        public TrainerRepo(GymDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }
    }
}
