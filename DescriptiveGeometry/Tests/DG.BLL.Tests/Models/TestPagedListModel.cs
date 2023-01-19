using DG.BLL.Models;
using DG.BLL.Tests.Helpers;
using DG.Core.Models;
using DG.DAL.Entities;

namespace DG.BLL.Tests.Models;

public static class TestPagedListModel
{
    public static PagedList<Drawing> GetValidDrawingPagedListModel => PagedListHelper.CreateDrawingPagedList(1);
    public static PagedList<DrawingEntity> GetValidDrawingEntityPagedListModel => PagedListHelper.CreateDrawingEntityPagedList(1);

    public static IEnumerable<PagedList<Drawing>> GetValidDrawingPagedListModels => new List<PagedList<Drawing>>()
    {
        PagedListHelper.CreateDrawingPagedList(1),
        PagedListHelper.CreateDrawingPagedList(1),
        PagedListHelper.CreateDrawingPagedList(2),
        PagedListHelper.CreateDrawingPagedList(3),
        PagedListHelper.CreateDrawingPagedList(4),
        PagedListHelper.CreateDrawingPagedList(5)
    };

    public static IEnumerable<PagedList<DrawingEntity>> GetValidDrawingEntityPagedListModels => new List<PagedList<DrawingEntity>>()
    {
        PagedListHelper.CreateDrawingEntityPagedList(1),
        PagedListHelper.CreateDrawingEntityPagedList(1),
        PagedListHelper.CreateDrawingEntityPagedList(2),
        PagedListHelper.CreateDrawingEntityPagedList(3),
        PagedListHelper.CreateDrawingEntityPagedList(4),
        PagedListHelper.CreateDrawingEntityPagedList(5)
    };
}