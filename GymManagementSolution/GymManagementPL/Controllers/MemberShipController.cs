using BLL.Interfaces;
using BLL.Serveices;
using BLL.ViewModels.MemberShipViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GymManagementPL.Controllers
{
    public class MemberShipController : Controller
    {
        private readonly IMemberShipServices _memberShipServices;

        public MemberShipController(IMemberShipServices memberShipServices)
        {
            _memberShipServices = memberShipServices;
        }
        public IActionResult Index()
        {
            var memberShips = _memberShipServices.GetAllMemberShips();
            return View(memberShips);
        }
        
        public IActionResult Create()
        {
            LoadMemberDropDownList();
            LoadPlanDropDownList();
            return View();
        }
        [HttpPost]
        public IActionResult Create(MemberShipToCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Please , correct the errors ";
                return RedirectToAction(nameof(Index));
            }
            
            var memberShipCreated = _memberShipServices.CreateMemberShip(model);
            if (memberShipCreated)
            {
                TempData["SuccessMessage"] = "MemberShip created successfully";
                 return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData["ErrorMessage"] = "Failed to create MemberShip";
                return View(nameof(Create), model);
            }
        }
        [HttpGet]
        public IActionResult Delete(int memberId, int planId)
        {
            if (memberId <= 0 && planId <= 0)
            {
                TempData["ErrorMessage"] = "Invalid Session Id";
                return RedirectToAction(nameof(Index));
            }
            var memberShip = _memberShipServices.GetMemberShipByMemberId(memberId);
            if (memberShip == null)
            {
                TempData["ErrorMessage"] = "membership Not Found";
                return RedirectToAction(nameof(Index));
            }
            ViewBag.MemberId = memberId;
            ViewBag.PlanId = planId;
            return View();
        }
        [HttpPost]
        public IActionResult DeleteConfirmed(int memberId, int planId)
        {
            if (memberId <= 0 && planId <= 0)
            {
                TempData["ErrorMessage"] = "Invalid Session Id";
                return RedirectToAction(nameof(Index));
            }
            var isDeleted = _memberShipServices.DeleteMemberShip(memberId , planId);
            if (isDeleted)
            {
                TempData["SuccessMessage"] = "Session Deleted Successfully";
            }
            else
            {
                TempData["ErrorMessage"] = "Failed to Delete MemberShip";
            }
            return RedirectToAction(nameof(Index));
        }



        private void LoadMemberDropDownList()
        {
            var members = _memberShipServices.GetAllMembersForDropDown();
            ViewBag.Members = new SelectList(members, "Id", "Name");
        }
        private void LoadPlanDropDownList()
        {
            var plans = _memberShipServices.GetAllPlansForDropDown();
            ViewBag.Plans = new SelectList(plans, "Id", "Name");
        }
    }
}
