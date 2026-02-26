using BLL.Interfaces;
using BLL.ViewModels.SessionViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GymManagementPL.Controllers
{
    [Authorize]
    public class SessionController : Controller
    {
        private readonly ISessionServices _sessionServices;

        public SessionController(ISessionServices sessionServices)
        {
            _sessionServices = sessionServices;
        }
        public IActionResult Index()
        {
            var sessions = _sessionServices.GetAllSessions();
            return View(sessions);
        }
        [HttpGet]
        public IActionResult Create()
        {
            TrainerLoadDropDown();
            CategoryLoadDropDown();
            return View();
        }
        [HttpPost]
        public IActionResult Create(CreateSessionViewModel viewModel)
        {
            if(!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Please correct the errors and try again.";
                return View(nameof(Create), viewModel);
            }
            TrainerLoadDropDown();
            CategoryLoadDropDown();

            var ChickCreateSession = _sessionServices.CreateSession(viewModel);
            if(ChickCreateSession)
            {
                TempData["SuccessMessage"] = "Session created successfully.";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["ErrorMessage"] = "Failed to create session. Please try again.";
                return View(nameof(Create), viewModel);
            }
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Invalid Session Id";
                return RedirectToAction(nameof(Index));
            }
            var sessionDetails = _sessionServices.GetSessionById(id);
            if (sessionDetails == null)
            {
                TempData["ErrorMessage"] = "Session Not Found";
                return RedirectToAction(nameof(Index));
            }
            return View(sessionDetails);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Invalid Session Id";
                return RedirectToAction(nameof(Index));
            }
            var sessionDetails = _sessionServices.GetSessionById(id);
            if (sessionDetails == null)
            {
                TempData["ErrorMessage"] = "Session Not Found";
                return RedirectToAction(nameof(Index));
            }
            ViewBag.SessionId = sessionDetails.Id;
            return View();
        }
        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Invalid Session Id";
                return RedirectToAction(nameof(Index));
            }
            var isDeleted = _sessionServices.DeleteSession(id);
            if (isDeleted)
            {
                TempData["SuccessMessage"] = "Session Deleted Successfully";
            }
            else
            {
                TempData["ErrorMessage"] = "Failed to Delete Session";
            }
            return RedirectToAction(nameof(Index));
        }
        private void TrainerLoadDropDown()
        {
            var trainers = _sessionServices.getTrainerForDropDown();
            ViewBag.Trainers = new SelectList(items : trainers , dataValueField : "Id" , dataTextField : "Name");
        }
        private void CategoryLoadDropDown()
        {
            var categories = _sessionServices.getCategoryForDropDown();
            ViewBag.Categories = new SelectList(items: categories, dataValueField: "Id", dataTextField: "CategoryName");
        }
    }
}
