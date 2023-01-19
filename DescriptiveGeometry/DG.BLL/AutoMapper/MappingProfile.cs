using AutoMapper;
using DG.BLL.Models;
using DG.Core.Models;
using DG.DAL.Entities;

namespace DG.BLL.AutoMapper;

public class MappingProfile : Profile
{
    public MappingProfile() 
    {
        CreateMap<DrawingEntity, Drawing>().ReverseMap();
        CreateMap<DrawingDescriptionEntity, DrawingDescription>().ReverseMap();
        CreateMap<SearchParameters, DG.DAL.Models.SearchParameters>()
        .ForMember("Skip", opt =>
        opt.MapFrom(s => (s.PageInfo.PageNumber - 1) * s.PageInfo.PageSize))
        .ForMember("Take", opt =>
            opt.MapFrom(s => s.PageInfo.PageSize));
        CreateMap<PagedList<DrawingEntity>, PagedList<Drawing>>().ReverseMap();
    }
}
