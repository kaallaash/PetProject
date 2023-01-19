using DG.BLL.Models;
using DG.BLL.Tests.Helpers;

namespace DG.BLL.Tests.Models;

public static class TestDrawingModel
{
    public static Drawing GetValidDrawingModel => DrawingModelHelper.Create(1);

    public static IEnumerable<Drawing> GetValidDrawingModels => new List<Drawing>()
    {
        DrawingModelHelper.Create(1),
        DrawingModelHelper.Create(2),
        DrawingModelHelper.Create(3),
        DrawingModelHelper.Create(4),
        DrawingModelHelper.Create(5)
    };
}