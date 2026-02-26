using AutoMapper;
using BLL.ViewModels.MemberViewModels;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Profiles
{
    public class MemberProfile : Profile
    {
        public MemberProfile()
        {
            CreateMap<Member, MemberViewModel>()
                .ForMember(dest => dest.PhoneNumber , opt => opt.MapFrom(src=> src.Phone))
                .ForMember(dest => dest.Gender , opt => opt.MapFrom(src => src.gender.ToString()));

            CreateMap<CreateMemberViewModel, Member>()
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.PhoneNumber))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => new Address
                {
                    Street = src.Street,
                    City = src.City,
                    BuildingNumber = src.BuildingNumber
                }))
                .ForMember(dest => dest.HealthRecord , opt => opt.MapFrom(src => src.HealthRecord));

            CreateMap<Member, MemberViewModel>()
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.Phone))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => $"{src.Address.Street}, {src.Address.City}, {src.Address.BuildingNumber}"))
                .ForMember(dest => dest.BirthOfDate , opt => opt.MapFrom(src => src.BirthDate.ToShortDateString()));
        
            CreateMap<HealthRecordViewModel, HealthRecord>().ReverseMap();

            CreateMap<Member , MemberToUpdateViewModel>()
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.Phone))
                .ForMember(dest => dest.Street, opt => opt.MapFrom(src => src.Address.Street))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.Address.City))
                .ForMember(dest => dest.BuildingNumber, opt => opt.MapFrom(src => src.Address.BuildingNumber));

            CreateMap<MemberToUpdateViewModel, Member>()
                .ForMember(dest => dest.Name, opt => opt.Ignore())
                .ForMember(dest => dest.Photo, opt => opt.Ignore())
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.PhoneNumber))
                .AfterMap((src , dest) =>
                {
                    dest.Address.Street = src.Street;
                    dest.Address.City = src.City;
                    dest.Address.BuildingNumber = src.BuildingNumber;
                    dest.UpdatedAt = DateTime.Now;
                });
        }
    }
}
