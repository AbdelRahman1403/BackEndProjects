using DAL.Data;
using DAL.Models;
using DAL.Reposatories.Interfaces;
using DAL.Reposatories.InterfacesRepos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Reposatories.Repos
{
    public class MemberShipRepo : GenericRepo<MemberShip>, IMemberShipRepo
    {
        private readonly GymDbContext _context;

        public MemberShipRepo(GymDbContext context) : base(context)
        {
            _context = context;
        }

        public void Delete(int MemberId, int PlanId)
        {
            var chickEntity = _context.MemberShips.Find(MemberId , PlanId);
            if (chickEntity is null)
                return;
            _context.Remove(chickEntity);
        }

        public IEnumerable<MemberShip> GetMemberShipsWithMemberAndPlan()
        {
            return _context.MemberShips
                           .Include(m => m.Member)
                           .Include(p => p.Plan)
                           .ToList();
        }

        public MemberShip? GetMemberShipsWithMemberAndPlanWithMemberId(int memberId)
        {
            return _context.MemberShips
                           .Include(m => m.Member)
                           .Include(p => p.Plan)
                           .FirstOrDefault(s => s.MemberId == memberId);
        }
    }
}
