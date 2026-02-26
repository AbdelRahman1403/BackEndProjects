using AutoMapper;
using BLL.Interfaces;
using BLL.ViewModels.SessionViewModel;
using DAL.Models;
using DAL.Reposatories.InterfacesRepos;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace BLL.Serveices
{
    public class SessionServices : ISessionServices
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public SessionServices(IUnitOfWork unitOfWork , IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public bool CreateSession(CreateSessionViewModel createSession)
        {
            if(!IsCategoryExist(createSession.CategoryId) ||
               !IsTrainerExist(createSession.TrainerId)   ||
               !IsValidSessionTime(createSession.StartSession , createSession.EndSession))
            {
                return false;
            }

            var session = mapper.Map<Session>(createSession);
            unitOfWork.GetRepository<Session>().Add(session);
            return unitOfWork.SaveChanges() > 0;
        }

        public IEnumerable<SessionViewModel> GetAllSessions()
        {
            var Sessions = unitOfWork.sessionRepo.GetSessionsWithTrainerAndCategory();
            if (!Sessions.Any()) return [];

            //return Sessions.Select(session => new SessionViewModel
            //{
            //    Id = session.Id,
            //    Description = session.Description,
            //    Capacity = session.Capacity,
            //    StartSession = session.StartTime,
            //    EndSession = session.EndTime,

            //    CategoryName = session.Category.CategoryName,  // Lazy loading
            //    TrainerName = session.TrainerSession.Name,    // Lazy loading
            //    AvailableSlots = session.Capacity - unitOfWork.sessionRepo.GetReservedSlotsCount(session.Id)
            //});
            var mappeedSession = mapper.Map<IEnumerable<SessionViewModel>>(Sessions);
            foreach (var session in mappeedSession)
                session.AvailableSlots = session.Capacity - unitOfWork.sessionRepo.GetReservedSlotsCount(session.Id);
            return mappeedSession;
        }

        public SessionViewModel? GetSessionById(int sessionId)
        {
            var session = unitOfWork.sessionRepo.GetSessionWithTrainerAndCategory(sessionId);
            if (session is null) return null;

            var mappedSession = mapper.Map<SessionViewModel>(session);
            mappedSession.AvailableSlots = session.Capacity - unitOfWork.sessionRepo.GetReservedSlotsCount(session.Id);
            return mappedSession;


            //return new SessionViewModel()
            //{
            //    Id = session.Id,
            //    Description = session.Description,
            //    Capacity = session.Capacity,
            //    StartSession = session.StartTime,
            //    EndSession = session.EndTime,
            //    CategoryName = session.Category.CategoryName,
            //    TrainerName  = session.TrainerSession.Name,   
            //    AvailableSlots = session.Capacity - unitOfWork.sessionRepo.GetReservedSlotsCount(session.Id)
            //};
        }

        public SessionToUpdateViewModel? GetSessionToUpdate(int sessionId)
        {
            /**
             * 1. Chick if the session already exist
             * 2. Chick from trainer Id and time 
             * 3. Chick for membersession class
             */
            var session = unitOfWork.sessionRepo.GetSessionWithTrainerAndCategory(sessionId);

            if(IsSessionAvailableForUpdate(session))
            {
                return null;
            }

            return mapper.Map<SessionToUpdateViewModel>(session);
        }

        public bool UpdateSession(SessionToUpdateViewModel sessionVM, int sessionId)
        {
            var session = unitOfWork.GetRepository<Session>().GetById(sessionId);

            if (IsSessionAvailableForUpdate(session))
            {
                return false;
            }

            if (!IsCategoryExist(session.CategoryId) ||
               !IsTrainerExist(session.TrainerId) ||
               !IsValidSessionTime(session.StartTime, session.EndTime))
            {
                return false;
            }

            session.UpdatedAt = DateTime.Now;
            var mappedSession = mapper.Map(sessionVM, session);
            unitOfWork.GetRepository<Session>().Update(mappedSession);
            return unitOfWork.SaveChanges() > 0;
        }

        public bool DeleteSession(int sessionId)
        {
            var session = unitOfWork.sessionRepo.GetById(sessionId);

            if(!IsSessionAvailableForDelete(session))
            {
                return false;
            }
            unitOfWork.GetRepository<Session>().Delete(sessionId);
            return unitOfWork.SaveChanges() > 0;
        }
        public IEnumerable<GetTrainerForDropDown> getTrainerForDropDown()
        {
            var trainers = unitOfWork.GetRepository<Trainer>().GetAll();
            if (!trainers.Any()) return [];
            return mapper.Map<IEnumerable<GetTrainerForDropDown>>(trainers);
        }

        public IEnumerable<GetCategoryForDropDown> getCategoryForDropDown()
        {
            var categories = unitOfWork.GetRepository<Category>().GetAll();
            if (!categories.Any()) return [];
            return mapper.Map<IEnumerable<GetCategoryForDropDown>>(categories);
        }
        #region HelperFunctions 

        private bool IsCategoryExist(int id)
        {
            return unitOfWork.GetRepository<Category>().GetById(id) is not null;
        }
        private bool IsTrainerExist(int id) 
                     => unitOfWork.GetRepository<Trainer>().GetById(id) is not null;
        //private bool IsSessionExist(int id)
        //             => unitOfWork.GetRepository<Session>().GetById(id) is not null;
        private bool IsValidSessionTime(DateTime start , DateTime end)
                     => end > DateTime.Now && end > start;

       private bool IsSessionAvailableForUpdate(Session session)
        {
            if ( session.EndTime < DateTime.Now   ||
                 session.StartTime < DateTime.Now ||
                unitOfWork.sessionRepo.GetReservedSlotsCount(session.Id) > 0) return false;

            return true;
        }

        private bool IsSessionAvailableForDelete(Session session)
        {
            return session.EndTime < DateTime.Now &&
                   unitOfWork.sessionRepo.GetReservedSlotsCount(session.Id) == 0;
        }

       
        #endregion

    }
}
