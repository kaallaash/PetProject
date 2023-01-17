using DG.DAL.IntegrationTests.Helpers;
using DG.DAL.Entities;
using DG.DAL.Models;

namespace DG.DAL.IntegrationTests.Entities;

public static class TestDrawingEntity
{
    internal static IEnumerable<DrawingEntity> GetValidDrawingEntitiesWithId() => new List<DrawingEntity>()
    {
        DrawingEntityHelper.CreateValidEntity(1),
        DrawingEntityHelper.CreateValidEntity(2),
        DrawingEntityHelper.CreateValidEntity(3),
        DrawingEntityHelper.CreateValidEntity(4),
        DrawingEntityHelper.CreateValidEntity(5)
    };

    internal static IEnumerable<DrawingEntity> GetValidCreatedDrawingEntities() => new List<DrawingEntity>()
    {
        DrawingEntityHelper.CreateValidEntityWithoutId(),
        DrawingEntityHelper.CreateValidEntityWithoutId(),
        DrawingEntityHelper.CreateValidEntityWithoutId(),
        DrawingEntityHelper.CreateValidEntityWithoutId(),
        DrawingEntityHelper.CreateValidEntityWithoutId()
    };

    internal static IEnumerable<PageParameters> GetPageParameters()
    {
        return new List<PageParameters>()
        {
            new PageParameters(1,2),
            new PageParameters(2,3),
            new PageParameters(3,1),
            new PageParameters(2,1),
            new PageParameters(4,1),
            new PageParameters(1,4)
        };
    }

    public static IEnumerable<object[]> GetValidDrawingEntities()
    {
        foreach (var validCreatedDrawingEntity in GetValidCreatedDrawingEntities())
        {
            yield return new object[] { validCreatedDrawingEntity };
        }
    }

    public static IEnumerable<object[]> GetValidPageParameters()
    {
        foreach (var pageParameter in GetPageParameters())
        {
            yield return new object[] { pageParameter };
        }
    }
}
