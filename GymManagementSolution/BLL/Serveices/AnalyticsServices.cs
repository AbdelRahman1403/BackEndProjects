using BLL.Interfaces;
using BLL.ViewModels;
using DAL.Models;
using DAL.Reposatories.InterfacesRepos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Serveices
{
    public class AnalyticsServices : IAnalyticsService
    {
        private readonly IUnitOfWork unitOfWork;

        public AnalyticsServices(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public AnalyitcsData GetAnalyticsData()
        {
            var sessions = unitOfWork.GetRepository<Session>().GetAll();
            return new AnalyitcsData
            {
                TotalActiveMembers = unitOfWork.GetRepository<MemberShip>().GetAll(act => act.Status == "Active").Count(),
                TotalMembers = unitOfWork.GetRepository<Member>().GetAll().Count(),
                TotalTrainers = unitOfWork.GetRepository<Trainer>().GetAll().Count(),
                OnGoingSessions = sessions.Count(s => s.StartTime >= DateTime.Now && s.EndTime <= DateTime.Now),
                UpcomingSessions = sessions.Count(s => s.StartTime > DateTime.Now),
                CompletedSessions = sessions.Count(s => s.EndTime < DateTime.Now),

            };
        }
    }
}
