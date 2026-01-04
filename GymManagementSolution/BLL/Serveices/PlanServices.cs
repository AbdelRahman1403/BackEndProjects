using BLL.Interfaces;
using BLL.ViewModels.MemberViewModels;
using BLL.ViewModels.PlanViewModels;
using DAL.Models;
using DAL.Reposatories.InterfacesRepos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Serveices
{
    public class PlanServices(IUnitOfWork unitOfWork) : IPlanServices
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public bool CreatePlan(CreatePlanViewModel createPlanViewModel)
        {
            if(createPlanViewModel == null)
            {
                return false;
            }

            var plan = new Plan
            {
                PlanName = createPlanViewModel.PlanName,
                PlanDescription = createPlanViewModel.PlanDescription,
                Price = createPlanViewModel.Price,
                DurationInDays = createPlanViewModel.DurationInDays,
                IsActive = createPlanViewModel.IsActive
            };

            _unitOfWork.GetRepository<Plan>().Add(plan);

            return _unitOfWork.SaveChanges() > 0;
        }

        public IEnumerable<PlanViewModel> GetAllPlans()
        {
            var Plans = _unitOfWork.GetRepository<Plan>().GetAll();
            if (Plans == null || !Plans.Any())
            {
                return Enumerable.Empty<PlanViewModel>();
            }

            return Plans.Select(plan => new PlanViewModel
            {
                Id= plan.Id,
                PlanName = plan.PlanName,
                PlanDescription = plan.PlanDescription,
                Price = plan.Price,
                DurationInDays = plan.DurationInDays,
                IsActive = plan.IsActive
            });
        }

        public UpdateToPlanViewModel? GetPlanToUpdate(int planId)
        {
            var PlanChick = _unitOfWork.GetRepository<Plan>().GetById(planId);
            if (PlanChick == null)
            {
                return null;
            }

            return new UpdateToPlanViewModel
            {
                
                PlanName = PlanChick.PlanName,
                PlanDescription = PlanChick.PlanDescription,
                Price = PlanChick.Price,
                DurationInDays = PlanChick.DurationInDays
            };
        }
        public bool UpdatePlan(int planId, UpdateToPlanViewModel updatePlanViewModel)
        {
            var planToUpdate = _unitOfWork.GetRepository<Plan>().GetById(planId);
            if(planToUpdate == null || planToUpdate.IsActive == false || ChickHasActiveMemeberShips(planId) == true)
            {
                return false;
            }

            planToUpdate.PlanName = updatePlanViewModel.PlanName;
            planToUpdate.PlanDescription = updatePlanViewModel.PlanDescription;
            planToUpdate.Price = updatePlanViewModel.Price;
            planToUpdate.DurationInDays = updatePlanViewModel.DurationInDays;
            planToUpdate.UpdatedAt = DateTime.Now;

            _unitOfWork.GetRepository<Plan>().Update(planToUpdate);
            return _unitOfWork.SaveChanges() > 0;
        }
        public bool TogglePlanStatus(int planId)
        {
            var planToUpdate = _unitOfWork.GetRepository<Plan>().GetById(planId);
            if (planToUpdate == null || ChickHasActiveMemeberShips(planId) == true)
            {
                return false;
            }

            planToUpdate.IsActive = !planToUpdate.IsActive;
            planToUpdate.UpdatedAt = DateTime.Now;
            _unitOfWork.GetRepository<Plan>().Update(planToUpdate);
            return _unitOfWork.SaveChanges() > 0;
        }  

        private bool ChickHasActiveMemeberShips(int PlanId)
        {
            return _unitOfWork.GetRepository<MemberShip>()
                              .GetAll(ms => ms.PlanId == PlanId 
                                   && ms.Status == "Active").Any();
        }
    }
}
