using AutoMapper;
using BLL.ViewModels.SessionViewModel;
using DAL.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Profiles
{
    public class SessionProfile : Profile
    {
        public SessionProfile()
        {
            //         TStource , TDestination
            // I need to catch the Tsource and put it in TDestination
            CreateMap<Session, SessionViewModel>()
                     .ForMember(dest => dest.TrainerName, options => options.MapFrom(src => src.TrainerSession.Name))
                     .ForMember(dest => dest.CategoryName, options => options.MapFrom(src => src.Category.CategoryName));
            
            CreateMap<CreateSessionViewModel, Session>()
                     .ForMember(dest => dest.StartTime , options => options.MapFrom(src => src.StartSession))
                     .ForMember(dest => dest.EndTime , options => options.MapFrom(src => src.EndSession));
            CreateMap<SessionToUpdateViewModel, Session>().ReverseMap();

            CreateMap<Trainer, GetTrainerForDropDown>();
            CreateMap<Category, GetCategoryForDropDown>();
        }
    }
}
