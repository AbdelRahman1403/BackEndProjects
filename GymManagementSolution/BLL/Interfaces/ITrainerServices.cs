using BLL.ViewModels.TrainerViewModels;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface ITrainerServices
    {
        public IEnumerable<TrainerViewMoel> GetAllTrainers();
        public bool CreateTrainer(TrainerToCreateViewModel trainerViewModel);

        public TrainerViewMoel? GetDetailsOfTrainer(int trainerId);

        public TrainerToUpdateViewModel? GetTrainerForUpdate(int TrainerId);

        public bool UpdateTrainer(int trainerId, TrainerToUpdateViewModel trainerViewModel);

        public bool DeleteTrainer(int trainerId);
    }
}
