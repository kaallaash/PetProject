using AutoMapper;
using DG.API.ViewModels;
using DG.API.Models;
using DG.BLL.Models;
using DG.Core.Models;

namespace DG.API.AutoMapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<DrawingViewModel, Drawing>().ReverseMap();
        CreateMap<ChangeDrawingViewModel, Drawing>().ReverseMap();
        CreateMap<DrawingDescriptionViewModel, DrawingDescription>().ReverseMap();
        CreateMap<QuerySearchParameters, SearchParameters>()
            .ForMember("PageInfo", opt =>
                opt.MapFrom(p => new PageInfo(p.PageNumber, p.PageSize)));
        CreateMap<PagedList<Drawing>, PagedList<DrawingViewModel>>().ReverseMap();
    }
}