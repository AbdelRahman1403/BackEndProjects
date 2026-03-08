using DAL.Models;
using DAL.Reposatories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Reposatories.InterfacesRepos
{
    public interface IMemberShipRepo : IGenericRepo<MemberShip>
    {
        public IEnumerable<MemberShip> GetMemberShipsWithMemberAndPlan();
        public MemberShip? GetMemberShipsWithMemberAndPlanWithMemberId(int MemberId); 

        void Delete(int MemberId, int PlanId);
    }
}
