using AutoMapper;
using BLL.ViewModels.MemberShipViewModels;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Profiles
{
    public class MemberShipProfile : Profile
    {
        public MemberShipProfile()
        {
            CreateMap<MemberShip , MemberShipViewModel>()
                .ForMember(dest => dest.MemberId , opt => opt.MapFrom(src => src.MemberId))
                .ForMember(dest => dest.Member , opt => opt.MapFrom(src => src.Member.Name))
                .ForMember(dest => dest.Plan, opt => opt.MapFrom(src => src.Plan.PlanName))
                .ForMember(dest => dest.StartDate , opt => opt.MapFrom(src => src.CreatedAt))
                .ForMember(dest => dest.EndDate , opt => opt.MapFrom(src => src.EndDate));

            CreateMap<MemberShipToCreateViewModel, MemberShip>()
                .ForMember(dest => dest.MemberId , opt => opt.MapFrom(src => src.MemberId))
                .ForMember(dest => dest.PlanId , opt => opt.MapFrom(src => src.PlanId))
                .ForMember(dest => dest.EndDate , opt => opt.MapFrom(src => src.EndDate));

            CreateMap<Member, GetMembersDropDown>().ReverseMap();
            CreateMap<Plan , GetPlansDropDown>()
                .ForMember(dest => dest.Name , opt => opt.MapFrom(src => src.PlanName))
                .ReverseMap();
        }
    }
}
