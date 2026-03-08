using AutoMapper;
using BLL.Interfaces;
using BLL.ViewModels.MemberShipViewModels;
using DAL.Models;
using DAL.Reposatories.InterfacesRepos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Serveices
{
    public class MemberShipServices : IMemberShipServices
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public MemberShipServices(IMapper mapper , IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public IEnumerable<MemberShipViewModel> GetAllMemberShips()
        {
            var memberships = _unitOfWork.memberShipRepo.GetMemberShipsWithMemberAndPlan();
            if(memberships is null || !memberships.Any())
            {
                return Enumerable.Empty<MemberShipViewModel>();
            }

            return _mapper.Map<IEnumerable<MemberShipViewModel>>(memberships);
        }
        public bool CreateMemberShip(MemberShipToCreateViewModel membershipToCreateViewModel)
        {
            var Chick = _unitOfWork.GetRepository<MemberShip>()
                                   .GetAll(m => m.Member.Id == membershipToCreateViewModel.MemberId).Any();
            if (Chick)
            {
                return false;
            }

            try
            {
                var MemberShipMapper = _mapper.Map<MemberShip>(membershipToCreateViewModel);
                MemberShipMapper.CreatedAt = DateTime.Now;
                _unitOfWork.GetRepository<MemberShip>().Add(MemberShipMapper);
                return _unitOfWork.SaveChanges() > 0;

            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public bool DeleteMemberShip(int MemberId, int PlanId)
        {
            var chick = _unitOfWork.memberShipRepo.GetMemberShipsWithMemberAndPlanWithMemberId(MemberId);

            if (chick is null)
            {
                return false;
            }

            try
            {
                _unitOfWork.memberShipRepo.Delete(MemberId , PlanId);
                return _unitOfWork.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public IEnumerable<GetMembersDropDown> GetAllMembersForDropDown()
        {
            var members = _unitOfWork.GetRepository<Member>().GetAll();
            if (!members.Any())
            {
                return Enumerable.Empty<GetMembersDropDown>();
            }

            return _mapper.Map<IEnumerable<GetMembersDropDown>>(members);
        }

        public IEnumerable<GetPlansDropDown> GetAllPlansForDropDown()
        {
            var plans = _unitOfWork.GetRepository<Plan>().GetAll();
            if (!plans.Any())
            {
                return Enumerable.Empty<GetPlansDropDown>();
            }

            return _mapper.Map<IEnumerable<GetPlansDropDown>>(plans);
        }

        public MemberShipViewModel? GetMemberShipByMemberId(int MemberId)
        {
            var membership = _unitOfWork.memberShipRepo.GetMemberShipsWithMemberAndPlanWithMemberId(MemberId);
            if(membership is null)
            {
                return null;
            }

            return _mapper.Map<MemberShipViewModel>(membership);
        }
    }
}
