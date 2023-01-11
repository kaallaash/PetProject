using AutoMapper;
using AuthorizationService.BLL.Models;
using AuthorizationService.DAL.Entities;

namespace AuthorizationService.BLL.AutoMapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<UserEntity, User>().ReverseMap();
    }
}
