using DG.BLL.Models;
using DG.Core.Models;
using DG.BLL.Tests.Models;
using DG.BLL.Tests.Entities;
using DG.DAL.Entities;

namespace DG.BLL.Tests.Helpers;

public static class PagedListHelper
{
    public static PagedList<Drawing> CreateDrawingPagedList(int randomValue)
    {
        var collection = TestDrawingModel.GetValidDrawingModels;
        var totalPages = collection is null ? 0 : collection.Count() * (randomValue + 1);

        return new PagedList<Drawing>(collection, totalPages);
    }

    public static PagedList<DrawingEntity> CreateDrawingEntityPagedList(int randomValue)
    {
        var collection = TestDrawingEntity.GetValidDrawingEntities;
        var totalPages = collection is null ? 0 : collection.Count() * (randomValue + 1);

        return new PagedList<DrawingEntity>(collection, totalPages);
    }
}