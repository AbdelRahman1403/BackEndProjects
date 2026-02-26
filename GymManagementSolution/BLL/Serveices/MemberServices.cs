using AutoMapper;
using BLL.AttachmentServices;
using BLL.Interfaces;
using BLL.ViewModels.MemberViewModels;
using DAL.Models;
using DAL.Reposatories.Interfaces;
using DAL.Reposatories.InterfacesRepos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Serveices
{
    public class MemberServices : IMemberServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper mapper;
        private readonly IAttachmentServices _attachmentServices;

        public MemberServices(IUnitOfWork unitOfWork , IMapper mapper , IAttachmentServices attachmentServices)
        {
            _unitOfWork = unitOfWork;
            this.mapper = mapper;
            _attachmentServices = attachmentServices;
        }
        public IEnumerable<MemberViewModel> GetAllMembers()
        {
            var members = _unitOfWork.GetRepository<Member>().GetAll();
            if (members is null || !members.Any())
            {
                return Enumerable.Empty<MemberViewModel>();
            }
            
            return mapper.Map<IEnumerable<MemberViewModel>>(members);
        }
        public bool CreateMember(CreateMemberViewModel memberViewModel)
        {
            var ChickEmailAndPhone = _unitOfWork.GetRepository<Member>().GetAll(m => m.Email == memberViewModel.Email
                                                        && m.Phone == memberViewModel.PhoneNumber).Any();
            if (ChickEmailAndPhone) return false;

            var PhotoName = _attachmentServices.Upload("members", memberViewModel.PhotoProfile);

            var MappingMember = mapper.Map<Member>(memberViewModel);
            try
            {
                MappingMember.Photo = PhotoName;
                _unitOfWork.GetRepository<Member>().Add(MappingMember);

                var IsCreated = _unitOfWork.SaveChanges() > 0;
                if(!IsCreated)
                {
                    _attachmentServices.Delete("members", PhotoName);
                    return false;
                }
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public MemberViewModel DetailsOfMember(int MemberId)
        {
            var member = _unitOfWork.GetRepository<Member>().GetById(MemberId);

            if (member is null) return null;
            var memberMapping = mapper.Map<MemberViewModel>(member);
            var membership = _unitOfWork.GetRepository<MemberShip>().GetAll(ch => ch.MemberId == member.Id && ch.Status == "Active").FirstOrDefault();

            if (membership is not null)
            {
                memberMapping.MemberShipStartDate = membership.CreatedAt.ToShortDateString();
                memberMapping.MemberShipEndDate = membership.EndDate.ToShortDateString();
                var Plan = _unitOfWork.GetRepository<Plan>().GetById(membership.PlanId);
                memberMapping.PlanName = Plan.PlanName;
            }

            return memberMapping;
        }

        public HealthRecordViewModel GetHealthRecordByMemberId(int memberId)
        {
            var member = _unitOfWork.GetRepository<Member>().GetById(memberId);
            if (member is null)
            {
                return null;
            }

            return mapper.Map<HealthRecordViewModel>(member.HealthRecord);
        }

        public MemberToUpdateViewModel GetMemberForUpdate(int memberId)
        {
            var member = _unitOfWork.GetRepository<Member>().GetById(memberId);
            
            if (member is null)
            {
                return null;
            }

            return mapper.Map<MemberToUpdateViewModel>(member);
        }
        public bool UpdateMember(int memberId, MemberToUpdateViewModel memberViewModel)
        {
            try
            {
                var ChickEmailAndPhone = _unitOfWork.GetRepository<Member>().GetAll(m => m.Email == memberViewModel.Email
                                                            && m.Phone == memberViewModel.PhoneNumber
                                                            && m.Id != memberId).Any();

                if (ChickEmailAndPhone) return false;

                var member = _unitOfWork.GetRepository<Member>().GetById(memberId);
                if (member is null)
                {
                    return false;
                }
                var Mapping = mapper.Map(memberViewModel, member);

                _unitOfWork.GetRepository<Member>().Update(member);
                return _unitOfWork.SaveChanges() > 0;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public bool DeleteMember(int memberId)
        {
            var member = _unitOfWork.GetRepository<Member>().GetById(memberId);
            if (member is null)
            {
                return false;
            }

            var HasSessions = _unitOfWork.GetRepository<MemberShip>().GetAll(ms => memberId == ms.MemberId 
                                                    && ms.EndDate > DateTime.Now);
            if (HasSessions.Any()) return false;

            var MemberShips = _unitOfWork.GetRepository<MemberShip>().GetAll(ms => ms.MemberId == memberId);

            try
            {
                foreach (var membership in MemberShips)
                {
                    _unitOfWork.GetRepository<MemberShip>().Delete(membership.Id);
                }
                _unitOfWork.GetRepository<MemberShip>().Delete(memberId);
                var isDeleated = _unitOfWork.SaveChanges() > 0;
                if (isDeleated)
                {
                    _attachmentServices.Delete("members", member.Photo);
                }
                return isDeleated;
            }
            catch (Exception)
            {

                return false;
            }
        }
    }
}
