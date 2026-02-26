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
    public class SessionRepo : GenericRepo<Session>, ISessionRepo
    {
        private readonly GymDbContext dbContext;

        public SessionRepo(GymDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }
        public int GetReservedSlotsCount(int sessionId)
        {
            return dbContext.MemberSessions.Count(ms => ms.SessionId == sessionId);
        }

        public IEnumerable<Session> GetSessionsWithTrainerAndCategory()
        {
            return dbContext.Sessions.Include(s => s.TrainerSession)
                                     .Include(s => s.Category)
                                     .ToList();
        }

        public Session? GetSessionWithTrainerAndCategory(int sessionId)
        {
            return dbContext.Sessions
                            .Include(s => s.TrainerSession)
                            .Include(s => s.Category)
                            .FirstOrDefault(s => s.Id == sessionId);
        }
    }
}
