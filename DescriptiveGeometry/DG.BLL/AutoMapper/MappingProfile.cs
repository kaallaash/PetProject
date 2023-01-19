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
            .ForMember(dalSp => dalSp.Skip, opt =>
        opt.MapFrom(sp => (sp.PageInfo.PageNumber - 1) * sp.PageInfo.PageSize))
        .ForMember(dalSp => dalSp.Take, opt =>
            opt.MapFrom(sp => sp.PageInfo.PageSize));
        CreateMap<PagedList<DrawingEntity>, PagedList<Drawing>>().ReverseMap();
    }
}
