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
            .ForMember(sp => sp.PageInfo, opt =>
                opt.MapFrom(qsp => new PageInfo(qsp.PageNumber, qsp.PageSize)));
        CreateMap<PagedList<Drawing>, PagedList<DrawingViewModel>>().ReverseMap();
    }
}