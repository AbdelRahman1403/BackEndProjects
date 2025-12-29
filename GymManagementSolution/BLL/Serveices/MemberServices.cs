using BLL.Interfaces;
using BLL.ViewModels.MemberViewModels;
using DAL.Models;
using DAL.Reposatories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Serveices
{
    public class MemberServices : IMemberServices
    {
        private readonly IGenericRepo<Member> memberRepo;

        public MemberServices(IGenericRepo<Member> MemberRepo)
        {
            memberRepo = MemberRepo;
        }
        public IEnumerable<MemberViewModel> GetAllMembers()
        {
            var members = memberRepo.GetAll();

            if(members is null || !members.Any())
            {
                return Enumerable.Empty<MemberViewModel>();
            }

            var memberViewModels = members.Select(m => new MemberViewModel
            {
                Id = m.Id,
                Name = m.Name,
                Email = m.Email,
                PhoneNumber = m.Phone,
                Photo = m.Photo
            });

            return memberViewModels;
        }
    }
}
