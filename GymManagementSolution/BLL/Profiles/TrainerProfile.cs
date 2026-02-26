using AutoMapper;
using BLL.ViewModels.TrainerViewModels;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Profiles
{
    public class TrainerProfile : Profile
    {
        public TrainerProfile()
        {
            CreateMap<Trainer, TrainerViewMoel>()
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => $"{src.Address.BuildingNumber} - {src.Address.Street} - {src.Address.City}"))
                .ForMember(dest => dest.Gender , opt => opt.MapFrom(src => src.gender.ToString()))
                .ForMember(dest => dest.DateOfBirth , opt => opt.MapFrom(src => src.BirthDate.ToShortDateString()))
                .ForMember(dest => dest.Specialties , opt => opt.MapFrom(src => src.specialties.ToString()));

            CreateMap<TrainerToCreateViewModel, Trainer>()
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => new Address
                {
                    BuildingNumber = src.BuildingNumber.ToString(),
                    Street = src.Street,
                    City = src.City
                }));

            CreateMap<Trainer , TrainerToUpdateViewModel>()
                .ForMember(dest => dest.BuildingNumber, opt => opt.MapFrom(src => int.Parse(src.Address.BuildingNumber)))
                .ForMember(dest => dest.Street, opt => opt.MapFrom(src => src.Address.Street))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.Address.City));
           
            CreateMap<TrainerToUpdateViewModel, Trainer>()
                .ForMember(dest => dest.Name , opt => opt.Ignore())
                .ForMember(dest => dest.Phone , opt => opt.Ignore())
                .AfterMap((src , dest) =>
                {
                    dest.Address.BuildingNumber = src.BuildingNumber.ToString();
                    dest.Address.Street = src.Street;
                    dest.Address.City = src.City;
                    dest.UpdatedAt = DateTime.Now;
                });
        }
    }
}
