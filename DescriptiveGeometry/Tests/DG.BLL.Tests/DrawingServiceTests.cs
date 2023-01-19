using AutoMapper;
using Moq;
using Xunit;
using DG.BLL.Models;
using DG.DAL.Interfaces.Repositories;
using DG.DAL.Entities;
using DG.BLL.Services;
using DG.Core.Models;
using static DG.BLL.Tests.Models.TestDrawingModel;
using static DG.BLL.Tests.Models.TestBllSearchParametersModel;
using static DG.BLL.Tests.Models.TestDalSearchParametersModel;
using static DG.BLL.Tests.Entities.TestDrawingEntity;
using static DG.BLL.Tests.Models.TestPagedListModel;

namespace DG.BLL.Tests;

public class DrawingServiceTests
{
    private readonly DrawingService _drawingService;
    private readonly Mock<IDrawingRepository<DrawingEntity>> _drawingRepository;
    private readonly Mock<IMapper> _mapper;

    public DrawingServiceTests()
    {
        _drawingRepository = new Mock<IDrawingRepository<DrawingEntity>>();
        _mapper = new Mock<IMapper>();
        _drawingService = new DrawingService(_drawingRepository.Object, _mapper.Object);
    }

    [Fact]
    public async Task GetById_ValidId_ReturnsDrawingModel()
    {
        var validDrawingEntity = GetValidDrawingEntity;
        var validDrawingModel = GetValidDrawingModel;
        _drawingRepository
                .Setup(dr => dr.GetById(validDrawingModel.Id, default))
                .ReturnsAsync(validDrawingEntity);
        _mapper
            .Setup(m => m.Map<Drawing>(validDrawingEntity))
            .Returns(validDrawingModel);

        var result = await _drawingService.GetById(validDrawingModel.Id, default);

        Assert.Equal(result?.Id, validDrawingModel.Id);
        Assert.Equal(result?.DrawingPhotoLink, validDrawingModel.DrawingPhotoLink);
    }

    [Fact]
    public async Task GetByParameters_ValidParameters_ReturnsPagedList()
    {
        var validDalSearchParameters = GetValidDalSearchParametersModel;
        var validBllSearchParameters = GetValidBllSearchParametersModel;
        var validDrawingEntityPagedList = GetValidDrawingEntityPagedListModel;
        var validDrawingPagedList = GetValidDrawingPagedListModel;

        _mapper
            .Setup(m => m.Map<DAL.Models.SearchParameters>(validBllSearchParameters))
            .Returns(validDalSearchParameters);
        _drawingRepository
            .Setup(dr => dr.GetByParameters(validDalSearchParameters, default))
            .ReturnsAsync(validDrawingEntityPagedList);
        _mapper
            .Setup(m => m.Map<PagedList<Drawing>>(validDrawingEntityPagedList))
            .Returns(validDrawingPagedList);

        var result = await _drawingService.GetByParameters(validBllSearchParameters, default);

        Assert.Equal(result?.Collection?.Count(), validDrawingPagedList?.Collection?.Count());
        Assert.Equal(result?.TotalPages, validDrawingPagedList?.TotalPages);
    }

    [Fact]
    public async Task GetAll_ReturnsDrawingModelList()
    {
        var validDrawingEntities = GetValidDrawingEntities;
        _drawingRepository
            .Setup(dr => dr.GetAll(default))
            .ReturnsAsync(validDrawingEntities);
        _mapper
            .Setup(m =>m.Map<IEnumerable<Drawing>>(validDrawingEntities))
            .Returns(GetValidDrawingModels);

        var result = await _drawingService.GetAll(default);
        
        Assert.Equal(result.Count(), validDrawingEntities.Count());
    }

    [Fact]
    public async Task Create_ValidDrawingModel_ReturnsDrawingModel()
    {
        var validDrawingEntity = GetValidDrawingEntity;
        var validDrawingModel = GetValidDrawingModel;
        _drawingRepository
            .Setup(dr => dr.Create(validDrawingEntity, default))
            .ReturnsAsync(validDrawingEntity);
        _mapper
            .Setup(m => m.Map<Drawing>(validDrawingEntity))
            .Returns(validDrawingModel);
        _mapper
            .Setup(m => m.Map<DrawingEntity>(validDrawingModel))
            .Returns(validDrawingEntity);

        var result = await _drawingService.Create(validDrawingModel, default);

        Assert.Equal(result.Id, validDrawingModel.Id);
        Assert.Equal(result.DrawingPhotoLink, validDrawingModel.DrawingPhotoLink);
    }

    [Fact]
    public async Task Update_ValidDrawingModel_ReturnsDrawingModel()
    {
        var validDrawingEntity = GetValidDrawingEntity;
        var validDrawingModel = GetValidDrawingModel;
        _drawingRepository
            .Setup(dr => dr.Update(validDrawingEntity, default))
            .ReturnsAsync(validDrawingEntity);
        _drawingRepository
            .Setup(dr => dr.GetById(validDrawingEntity.Id, default))
            .ReturnsAsync(validDrawingEntity);
        _mapper
            .Setup(m => m.Map<Drawing>(validDrawingEntity))
            .Returns(validDrawingModel);
        _mapper
            .Setup(m => m.Map<DrawingEntity>(validDrawingModel))
            .Returns(validDrawingEntity);

        var result = await _drawingService.Update(validDrawingModel, default);

        Assert.Equal(result.Id, validDrawingModel.Id);
        Assert.Equal(result.DrawingPhotoLink, validDrawingModel.DrawingPhotoLink);
    }
}
