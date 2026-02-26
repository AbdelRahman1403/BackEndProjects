using BLL.Interfaces;
using BLL.Serveices;
using BLL.ViewModels.PlanViewModels;
using BLL.ViewModels.TrainerViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementPL.Controllers
{
    [Authorize]
    public class PlanController : Controller
    {
        private readonly IPlanServices _planServices;

        public PlanController(IPlanServices planServices)
        {
            _planServices = planServices;
        }
        public IActionResult Index()
        {
            var GetAllPlans = _planServices.GetAllPlans();
            return View(GetAllPlans);
        }
        [HttpGet]
        public IActionResult Details(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Invalid Plan Id";
                return RedirectToAction(nameof(Index));
            }
            var planDetails = _planServices.GetPlanById(id);
            if (planDetails == null)
            {
                TempData["ErrorMessage"] = "Plan Not Found";
                return RedirectToAction(nameof(Index));
            }

            return View(planDetails);
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Invalid Plan Id";
                return RedirectToAction(nameof(Index));
            }
            var planToUpdate = _planServices.GetPlanToUpdate(id);
            if (planToUpdate == null)
            {
                TempData["ErrorMessage"] = "Plan Not Found";
                return RedirectToAction(nameof(Index));
            }
            return View(planToUpdate);
        }
        [HttpPost]
        public IActionResult Edit([FromRoute]int id , UpdateToPlanViewModel planViewModel)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Invalid Trainer Id";
                return RedirectToAction(nameof(Index));
            }

            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "Please correct the errors and try again.");
                return View(nameof(Edit), planViewModel);
            }
            var ChickUpdatePlan = _planServices.UpdatePlan(id, planViewModel);
            if (ChickUpdatePlan)
            {
                TempData["SuccessMessage"] = "Plan Updated Successfully";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Please correct the errors and try again.");
                return View(nameof(Edit), planViewModel);
            }
        }


        [HttpPost]
        public IActionResult ToggleStatus(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Invalid Plan Id";
                return RedirectToAction(nameof(Index));
            }
            var toggleStatusResult = _planServices.TogglePlanStatus(id);
            if (toggleStatusResult)
            {
                TempData["SuccessMessage"] = "Plan status toggled successfully.";
            }
            else
            {
                TempData["ErrorMessage"] = "Failed to toggle plan status.";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
