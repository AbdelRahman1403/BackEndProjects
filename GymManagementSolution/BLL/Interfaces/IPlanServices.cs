using BLL.ViewModels.PlanViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IPlanServices
    {
        bool CreatePlan(CreatePlanViewModel createPlanViewModel);
        IEnumerable<PlanViewModel> GetAllPlans();
        
        UpdateToPlanViewModel? GetPlanToUpdate(int planId);

        bool UpdatePlan(int planId, UpdateToPlanViewModel updatePlanViewModel);

        bool TogglePlanStatus(int planId);
    }
}
