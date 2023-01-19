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

    internal static IEnumerable<SearchParameters> GetSearchParameters()
    {
        return new List<SearchParameters>()
        {
            new SearchParameters()
            {
                Skip = 0,
                Take = 2
            },
            new SearchParameters()
            {
                Skip = 2,
                Take = 3
            },
            new SearchParameters()
            {
                Skip = 3,
                Take = 2
            },
            new SearchParameters()
            {
                Skip = 1,
                Take = 2
            },
            new SearchParameters()
            {
                Skip = 3,
                Take = 5
            }
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
        foreach (var pageParameter in GetSearchParameters())
        {
            yield return new object[] { pageParameter };
        }
    }
}
