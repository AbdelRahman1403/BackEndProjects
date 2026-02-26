using AutoMapper;
using BLL.Interfaces;
using BLL.ViewModels.TrainerViewModels;
using DAL.Models;
using DAL.Reposatories.InterfacesRepos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Serveices
{
    public class TrainerServices : ITrainerServices
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public TrainerServices(IUnitOfWork unitOfWork , IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public IEnumerable<TrainerViewMoel> GetAllTrainers()
        {
            var trainers = unitOfWork.GetRepository<Trainer>().GetAll();

            if(trainers is null || !trainers.Any())
            {
                return Enumerable.Empty<TrainerViewMoel>();
            }

            return mapper.Map<IEnumerable<TrainerViewMoel>>(trainers);
        }
        public bool CreateTrainer(TrainerToCreateViewModel trainerViewModel)
        {
            var ChickEmailAndPhone = unitOfWork.GetRepository<Trainer>().GetAll(
                t => t.Email == trainerViewModel.Email || t.Phone == trainerViewModel.Phone).Any();
            if (ChickEmailAndPhone) return false;
            try
            {
                var TrainerMapper = mapper.Map<Trainer>(trainerViewModel);
                unitOfWork.GetRepository<Trainer>().Add(TrainerMapper);
                TrainerMapper.CreatedAt = DateTime.Now;
                return unitOfWork.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                return false;

            }
        }
        public TrainerViewMoel? GetDetailsOfTrainer(int trainerId)
        {
            var GetTrainer = unitOfWork.GetRepository<Trainer>().GetById(trainerId);
            if (GetTrainer is null) return null;

            return mapper.Map<TrainerViewMoel>(GetTrainer);
        }
        public bool DeleteTrainer(int trainerId)
        {
            var GetTrainer = unitOfWork.GetRepository<Trainer>().GetById(trainerId);
            if (GetTrainer is null) return false;

            var ChickActiveSessions = unitOfWork.GetRepository<Session>().GetAll(s => s.TrainerId == trainerId
                                                                                  &&  s.StartTime > DateTime.Now).Any();
            if (ChickActiveSessions) return false;
            try
            {
                unitOfWork.GetRepository<Trainer>().Delete(trainerId);
                return unitOfWork.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public TrainerToUpdateViewModel? GetTrainerForUpdate(int TrainerId)
        {
            var GetTrainer = unitOfWork.GetRepository<Trainer>().GetById(TrainerId);
            if (GetTrainer is null) return null;

            return mapper.Map<TrainerToUpdateViewModel>(GetTrainer);
        }
        public bool UpdateTrainer(int trainerId, TrainerToUpdateViewModel trainerViewModel)
        {
            var ChickEmailAndPhone = unitOfWork.GetRepository<Trainer>().GetAll(
                t => (t.Email == trainerViewModel.Email || t.Phone == trainerViewModel.Phone) && t.Id != trainerId).Any();
            if (ChickEmailAndPhone) return false;

            try
            {
                var GetTrainer = unitOfWork.GetRepository<Trainer>().GetById(trainerId);
                if (GetTrainer is null) return false;
                var TrainerMapper = mapper.Map(trainerViewModel , GetTrainer);
                unitOfWork.GetRepository<Trainer>().Update(TrainerMapper);
                return unitOfWork.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
