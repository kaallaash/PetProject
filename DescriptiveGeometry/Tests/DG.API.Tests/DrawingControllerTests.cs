using System.Net;
using System.Net.Http.Json;
using System.Text;
using DG.API.ViewModels;
using DG.Core.Models;
using DG.DAL.Context;
using Newtonsoft.Json;
using Xunit;
using Microsoft.Extensions.DependencyInjection;
using static DG.API.Tests.Entities.TestDrawingEntity;
using static DG.API.Tests.ViewModels.TestDrawingViewModel;
using static DG.API.Tests.Models.TestSearchParametersModel;
using static DG.API.Tests.Constants.ApiTestsConstants;
using Shouldly;

namespace DG.API.Tests;

public class DrawingControllerTests
{
    [Fact]
    public async Task Get_ReturnDrawingViewModel()
    {
        await using var application = new DrawingApi();
        var client = await CreateClient(application);

        foreach (var validDrawingEntity in ValidDrawingEntities )
        {
            var responseDrawingViewModel =
                await client.GetFromJsonAsync<DrawingViewModel>($"{DrawingPath}/{validDrawingEntity.Id}");

            Assert.Equal(validDrawingEntity.Id, responseDrawingViewModel?.Id);
        }
    }

    [Fact]
    public async Task GetAll_ReturnsDrawingViewModels()
    {
        await using var application = new DrawingApi();
        var client = await CreateClient(application);

        var responseDrawingViewModels = await client.GetFromJsonAsync<IEnumerable<DrawingViewModel>>(DrawingPath);

        Assert.Equal(ValidDrawingEntities.Count(), responseDrawingViewModels?.Count());
    }

    [Fact]
    public async Task GetByParameters_ValidSearchParameters_ReturnsPagedList()
    {
        await using var application = new DrawingApi();
        var client = await CreateClient(application);

        var validSearchParameters = GetValidSearchParametersModel;

        var actualPagedList = await client.GetFromJsonAsync<PagedList<DrawingViewModel>>(
            $"{DrawingPath}/page?PageNumber={validSearchParameters.PageNumber}" +
            $"&PageSize={validSearchParameters.PageSize}" +
            $"&SearchPhrase={validSearchParameters.SearchPhrase}");

        actualPagedList?.Collection.ShouldNotBeNull();
        actualPagedList?.Collection?.Count().ShouldBeLessThanOrEqualTo(validSearchParameters.PageSize);
    }

    [Fact]
    public async Task Create_ReturnsDrawingViewModel()
    {
        await using var application = new DrawingApi();
        var client = await CreateClient(application);

        var jsonDrawingViewModel = JsonConvert.SerializeObject(ValidCreateDrawingViewModel);
        var contentDrawingViewModel = new StringContent(jsonDrawingViewModel, Encoding.UTF8, "application/json");

        var response = await client.PostAsync(DrawingPath, contentDrawingViewModel);
        var responseDrawingViewModel = await response.Content.ReadAsAsync<DrawingViewModel>();

        Assert.Equal( HttpStatusCode.OK, response.StatusCode);
        Assert.Equal(ValidCreateDrawingViewModel.DrawingPhotoLink, responseDrawingViewModel?.DrawingPhotoLink);
    }

    [Fact]
    public async Task Update_ReturnsDrawingViewModel()
    {
        await using var application = new DrawingApi();
        var client = await CreateClient(application);

        var updateDrawingViewModel = ValidChangeDrawingViewModel;
        updateDrawingViewModel.DrawingPhotoLink += "Update";

        var jsonDrawingViewModel = JsonConvert.SerializeObject(updateDrawingViewModel);
        var contentDrawingViewModel = new StringContent(jsonDrawingViewModel, Encoding.UTF8, "application/json");

        var response = await client.PutAsync($"{DrawingPath}/{ValidDrawingViewModel.Id}", contentDrawingViewModel);
        var responseDrawingViewModel = await response.Content.ReadAsAsync<DrawingViewModel>();

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Equal(updateDrawingViewModel.DrawingPhotoLink, responseDrawingViewModel?.DrawingPhotoLink);
    }

    [Fact]
    public async Task Delete_ReturnsDrawingsCount()
    {
        await using var application = new DrawingApi();
        var client = await CreateClient(application);

        var responseDelete = await client.DeleteAsync($"{DrawingPath}/{ValidDrawingViewModel.Id}");
        var responseDrawingViewModels = await client.GetFromJsonAsync<IEnumerable<DrawingViewModel>>(DrawingPath);

        Assert.Equal(HttpStatusCode.OK, responseDelete.StatusCode);
        Assert.Equal(ValidDrawingEntities.Count() - 1, responseDrawingViewModels?.Count());
    }

    private static async Task<HttpClient> CreateClient(DrawingApi application)
    {
        using (var scope = application.Services.CreateScope())
        {
            var provider = scope.ServiceProvider;
            await using var dbContext = provider.GetRequiredService<DatabaseContext>();
            await dbContext.Database.EnsureCreatedAsync();

            await dbContext.Drawings.AddRangeAsync(ValidDrawingEntities);
            await dbContext.SaveChangesAsync();
        }

        return application.CreateClient();
    }
}