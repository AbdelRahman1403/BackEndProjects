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

        public MemberServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IEnumerable<MemberViewModel> GetAllMembers()
        {
            var members = _unitOfWork.GetRepository<Member>().GetAll();
            if (members is null || !members.Any())
            {
                return Enumerable.Empty<MemberViewModel>();
            }
            var memberViewModels = members.Select(m => new MemberViewModel
            {
                Id = m.Id,
                Name = m.Name,
                Email = m.Email,
                PhoneNumber = m.Phone,
                Photo = m.Photo,
                Gender = m.gender.ToString()
            });
            return memberViewModels;
        }
        public bool CreateMember(CreateMemberViewModel memberViewModel)
        {
            if (memberViewModel is null)
            {
                return false;
            }
            var member = new Member
            {
                Name = memberViewModel.Name,
                Email = memberViewModel.Email,
                Phone = memberViewModel.PhoneNumber,
                gender = memberViewModel.gender,
                BirthDate = memberViewModel.BirthDate,
                Address = new Address
                {
                    Street = memberViewModel.Street,
                    City = memberViewModel.City,
                    BuildingNumber = memberViewModel.BuildingNumber
                },
                HealthRecord = new HealthRecord
                {
                    Height = memberViewModel.HealthRecord.Height,
                    Weight = memberViewModel.HealthRecord.Weight,
                    BloodType = memberViewModel.HealthRecord.BloodType,
                    Notes = memberViewModel.HealthRecord.Notes
                }
            };
            try
            {
                _unitOfWork.GetRepository<Member>().Add(member);
                return _unitOfWork.SaveChanges() > 0;
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

            var MemberDetails = new MemberViewModel
            {
                Name = member.Name,
                Email = member.Email,
                PhoneNumber = member.Phone,
                Photo = member.Photo,
                Address = $"{member.Address.Street}, {member.Address.City}, {member.Address.BuildingNumber}",
                BirthOfDate = member.BirthDate.ToShortDateString()
            };
            var membership = _unitOfWork.GetRepository<MemberShip>().GetAll(ch => ch.MemberId == member.Id && ch.Status == "Active").FirstOrDefault();

            if (membership is not null)
            {
                MemberDetails.MemberShipStartDate = membership.CreatedAt.ToShortDateString();
                MemberDetails.MemberShipEndDate = membership.EndDate.ToShortDateString();
                var Plan = _unitOfWork.GetRepository<Plan>().GetById(membership.PlanId);
                MemberDetails.PlanName = Plan.PlanName;
            }

            return MemberDetails;
        }

        public HealthRecordViewModel GetHealthRecordByMemberId(int memberId)
        {
            var member = _unitOfWork.GetRepository<Member>().GetById(memberId);
            if (member is null)
            {
                return null;
            }

            var HealthRecord = new HealthRecordViewModel
            {
                Weight = member.HealthRecord.Weight,
                Height = member.HealthRecord.Height,
                BloodType = member.HealthRecord.BloodType,
                Notes = member.HealthRecord.Notes
            };

            return HealthRecord;
        }

        public MemberToUpdateViewModel GetMemberForUpdate(int memberId)
        {
            var member = _unitOfWork.GetRepository<Member>().GetById(memberId);
            
            if (member is null)
            {
                return null;
            }

            return new MemberToUpdateViewModel()
            {
                Name = member.Name,
                Email = member.Email,
                PhoneNumber = member.Phone,
                Photo = member.Photo,
                Street = member.Address.Street,
                City = member.Address.City,
                BuildingNumber = member.Address.BuildingNumber
            };
        }
        public bool UpdateMember(int memberId, MemberToUpdateViewModel memberViewModel)
        {
            try
            {
                var ChickEmailAndPhone = _unitOfWork.GetRepository<Member>().GetAll(m => m.Email == memberViewModel.Email
                                                            && m.Phone == memberViewModel.PhoneNumber).Any();

                if (ChickEmailAndPhone) return false;

                var member = _unitOfWork.GetRepository<Member>().GetById(memberId);
                if (member is null)
                {
                    return false;
                }
                member.Name = memberViewModel.Name;
                member.Email = memberViewModel.Email;
                member.Phone = memberViewModel.PhoneNumber;
                member.Photo = memberViewModel.Photo;
                member.Address.Street = memberViewModel.Street;
                member.Address.City = memberViewModel.City;
                member.Address.BuildingNumber = memberViewModel.BuildingNumber;

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
                return _unitOfWork.SaveChanges() > 0;
            }
            catch (Exception)
            {

                return false;
            }
        }
    }
}
