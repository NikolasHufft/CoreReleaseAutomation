using AutoMapper;
using CoreReleaseAutomation.Models;
using CoreReleaseAutomation.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreReleaseAutomation.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, LoginViewModel>().ReverseMap(); ;
            CreateMap<LoginViewModel, User>().ReverseMap(); ;
            CreateMap<HomeViewModel, Release>().ReverseMap(); ;
            CreateMap<ReleaseViewModel, Release>().ReverseMap();

            CreateMap<Release, HomeViewModel>().ReverseMap();
        }
    }
}
