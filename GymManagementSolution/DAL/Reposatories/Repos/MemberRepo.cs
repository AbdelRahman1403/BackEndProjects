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
    public class MemberRepo : GenericRepo<Member> , IMember
    {
        private readonly GymDbContext dbContext;

        public MemberRepo(Data.GymDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }
    }
}
