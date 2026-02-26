using BLL.Interfaces;
using BLL.ViewModels.MemberViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementPL.Controllers
{
    [Authorize(Roles = "SuperAdmin")]
    public class MemberController : Controller
    {
        private readonly IMemberServices memberServices;

        public MemberController(IMemberServices memberServices)
        {
            this.memberServices = memberServices;
        }
        public IActionResult Index()
        {
            var AllMembers = memberServices.GetAllMembers();
            return View(AllMembers);
        }
        public IActionResult MemberDetails(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Invalid Member Id";
                return RedirectToAction(nameof(Index));
            }
            var memberDetails = memberServices.DetailsOfMember(id);
            if (memberDetails == null)
            {
                TempData["ErrorMessage"] = "Member Not Found";
                return RedirectToAction(nameof(Index));
            }
            return View(memberDetails);
        }
        public IActionResult MemberHealthRecordDetails(int? id)
        {
            if (id <= 0 || id is null)
            {
                TempData["ErrorMessage"] = "Invalid Health Record member Id";
                return RedirectToAction(nameof(Index));
            }
            var memberHealthRecordDetails = memberServices.GetHealthRecordByMemberId(id.Value);
            if (memberHealthRecordDetails is null)
            {
                TempData["ErrorMessage"] = "Health record of member is not found";
                return RedirectToAction(nameof(Index));
            }
            return View(memberHealthRecordDetails);
        }
        [HttpGet]
        public IActionResult MemberCreate()
        {
            return View();
        }
        [HttpPost]
        public IActionResult MemberCreate(CreateMemberViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "Please correct the errors and try again.");
                return View(nameof(MemberCreate), viewModel);
            }
            var ChickCreateMember = memberServices.CreateMember(viewModel);
            if (ChickCreateMember)
            {
                TempData["SuccessMessage"] = "Member Created Successfully";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Please correct the errors and try again.");
                return View(nameof(MemberCreate), viewModel);
            }
        }

        [HttpGet]
        public IActionResult MemberUpdate(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Invalid Member Id";
                return RedirectToAction(nameof(Index));
            }
            var memberDetails = memberServices.GetMemberForUpdate(id);
            if (memberDetails == null)
            {
                TempData["ErrorMessage"] = "Member Not Found";
                return RedirectToAction(nameof(Index));
            }
            return View(memberDetails);
        }

        [HttpPost]
        public IActionResult MemberUpdate([FromRoute]int id,MemberToUpdateViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "Please correct the errors and try again.");
                return View(nameof(MemberUpdate), viewModel);
            }
            var ChickUpdateMember = memberServices.UpdateMember(id , viewModel);
            if (ChickUpdateMember)
            {
                TempData["SuccessMessage"] = "Member Updated Successfully";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Please correct the errors and try again.");
                return View(nameof(MemberCreate), viewModel);
            }
        }
    }
}
