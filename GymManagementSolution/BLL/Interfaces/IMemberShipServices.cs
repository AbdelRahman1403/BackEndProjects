using BLL.ViewModels.MemberShipViewModels;
using BLL.ViewModels.TrainerViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IMemberShipServices
    {
        public IEnumerable<MemberShipViewModel> GetAllMemberShips();
        public MemberShipViewModel? GetMemberShipByMemberId(int MemberId);
        public bool CreateMemberShip(MemberShipToCreateViewModel membershipToCreateViewModel);
        public bool DeleteMemberShip(int MemberId , int PlanId);

        public IEnumerable<GetMembersDropDown> GetAllMembersForDropDown();
         public IEnumerable<GetPlansDropDown> GetAllPlansForDropDown();
    }
}
