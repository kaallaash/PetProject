using DG.API.ViewModels;
using DG.API.Tests.Helpers;

namespace DG.API.Tests.ViewModels;

public static class TestDrawingViewModel
{
    public static ChangeDrawingViewModel GetValidChangeDrawingViewModel =>
        DrawingModelHelper.CreateChangeDrawingViewModel(1);
    public static DrawingViewModel GetValidDrawingViewModel => 
        DrawingModelHelper.CreateDrawingViewModel(1);
    public static DrawingViewModel GetValidCreateDrawingViewModel =>
        DrawingModelHelper.CreateDrawingViewModel(6);

    public static IEnumerable<ChangeDrawingViewModel> GetValidChangeDrawingViewModels =>
        new List<ChangeDrawingViewModel>()
    {
        DrawingModelHelper.CreateChangeDrawingViewModel(1),
        DrawingModelHelper.CreateChangeDrawingViewModel(2),
        DrawingModelHelper.CreateChangeDrawingViewModel(3),
        DrawingModelHelper.CreateChangeDrawingViewModel(4),
        DrawingModelHelper.CreateChangeDrawingViewModel(5)
    };

    public static IEnumerable<DrawingViewModel> GetValidDrawingViewModels =>
        new List<DrawingViewModel>()
    {
        DrawingModelHelper.CreateDrawingViewModel(1),
        DrawingModelHelper.CreateDrawingViewModel(2),
        DrawingModelHelper.CreateDrawingViewModel(3),
        DrawingModelHelper.CreateDrawingViewModel(4),
        DrawingModelHelper.CreateDrawingViewModel(5)
    };
}