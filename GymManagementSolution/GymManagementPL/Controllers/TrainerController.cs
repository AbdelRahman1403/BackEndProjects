using BLL.Interfaces;
using BLL.Serveices;
using BLL.ViewModels.TrainerViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace GymManagementPL.Controllers
{
    [Authorize(Roles ="SuperAdmin")]
    public class TrainerController : Controller
    {
        private readonly ITrainerServices trainerServices;

        public TrainerController(ITrainerServices trainerServices)
        {
            this.trainerServices = trainerServices;
        }
        public IActionResult Index()
        {
            var GetTrainers = trainerServices.GetAllTrainers();
            return View(GetTrainers);
        }
        [HttpGet]
        public IActionResult CreateTrainer()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateTrainer(TrainerToCreateViewModel createViewModel)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "Please correct the errors and try again.");
                return View(nameof(CreateTrainer), createViewModel);
            }
            var ChickCreateTrainer = trainerServices.CreateTrainer(createViewModel);
            if (ChickCreateTrainer)
            {
                TempData["SuccessMessage"] = "Member Created Successfully";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Please correct the errors and try again.");
                return View(nameof(CreateTrainer), createViewModel);
            }
        }

        [HttpGet]
        public IActionResult TrainerDetails(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Invalid Trainer Id";
                return RedirectToAction(nameof(Index));
            }
            var trainerDetails = trainerServices.GetDetailsOfTrainer(id);
            if (trainerDetails == null)
            {
                TempData["ErrorMessage"] = "Trainer Not Found";
                return RedirectToAction(nameof(Index));
            }
            return View(trainerDetails);
        }
        [HttpGet]
        public IActionResult TrainerUpdate(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Invalid Trainer Id";
                return RedirectToAction(nameof(Index));
            }
            var trainerDetails = trainerServices.GetTrainerForUpdate(id);
            if (trainerDetails is null)
            {
                TempData["ErrorMessage"] = "Trainer Not Found";
                return RedirectToAction(nameof(Index));
            }
            return View(trainerDetails);
        }
        [HttpPost]
        public IActionResult TrainerUpdate([FromRoute] int id, TrainerToUpdateViewModel trainerViewMoel)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Invalid Trainer Id";
                return RedirectToAction(nameof(Index));
            }

            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "Please correct the errors and try again.");
                return View(nameof(TrainerUpdate), trainerViewMoel);
            }
            var ChickUpdateTrainer = trainerServices.UpdateTrainer(id, trainerViewMoel);
            if (ChickUpdateTrainer)
            {
                TempData["SuccessMessage"] = "Trainer Updated Successfully";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Please correct the errors and try again.");
                return View(nameof(TrainerUpdate), trainerViewMoel);
            }
        }
        [HttpGet]
        public IActionResult TrainerDelete(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Invalid Trainer Id";
                return RedirectToAction(nameof(Index));
            }
            var ChickTrainer = trainerServices.GetDetailsOfTrainer(id);
            if(ChickTrainer is null)
            {
                TempData["ErrorMessage"] = "Trainer Not Found";
                return RedirectToAction(nameof(Index));
            }

            ViewBag.TrainerId = ChickTrainer.Id;
            return View();
        }
        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {

            var ChickDeleteTrainer = trainerServices.DeleteTrainer(id);
            if (ChickDeleteTrainer)
            {
                TempData["SuccessMessage"] = "Trainer Deleted Successfully";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData["ErrorMessage"] = "Error in Deleting Trainer";
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
