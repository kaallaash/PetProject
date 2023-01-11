using AutoMapper;
using AuthorizationService.API.ViewModels;
using AuthorizationService.BLL.Models;

namespace AuthorizationService.API.AutoMapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<UserViewModel, User>().ReverseMap();
        CreateMap<ChangeUserViewModel, User>().ReverseMap();
        CreateMap<LoginViewModel, User>().ReverseMap();
    }
}