using DAL.Models;
using DAL.Reposatories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Reposatories.InterfacesRepos
{
    public interface ISessionRepo : IGenericRepo<Session>
    {
        IEnumerable<Session> GetSessionsWithTrainerAndCategory();

        int GetReservedSlotsCount(int sessionId);

        Session? GetSessionWithTrainerAndCategory(int sessionId);
    }
}
