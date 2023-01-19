using AutoMapper;
using DG.API.ViewModels;
using DG.API.Models;
using DG.BLL.Interfaces;
using DG.BLL.Models;
using DG.Core.Models;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DG.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]

public class DrawingController : Controller
{
    private readonly IDrawingService<Drawing, int> _drawingService;
    private readonly IMapper _mapper;
    private readonly IValidator<ChangeDrawingViewModel> _changeDrawingViewModelValidator;

    public DrawingController(
        IDrawingService<Drawing, int> drawingService,
        IMapper mapper,
        IValidator<ChangeDrawingViewModel> changeDrawingViewModelValidator)
    {
        _drawingService = drawingService;
        _mapper = mapper;
        _changeDrawingViewModelValidator = changeDrawingViewModelValidator;
    }

    [HttpGet("{id}")]
    public async Task<DrawingViewModel> Get(
        int id,
        CancellationToken cancellationToken)
    {
        var drawing = await _drawingService
            .GetById(id, cancellationToken);

        return _mapper.Map<DrawingViewModel>(drawing);
    }

    [HttpGet("page")]
    public async Task<PagedList<DrawingViewModel>> GetByParameters(
        [FromQuery] QuerySearchParameters querySearchParameters,
        CancellationToken cancellationToken)
    {
        var searchParameters = _mapper.Map<SearchParameters>(querySearchParameters);
        var pagedList = await _drawingService.GetByParameters(searchParameters, cancellationToken);

        return _mapper.Map<PagedList<DrawingViewModel>>(pagedList);
    }

    [HttpGet]
    public async Task<IEnumerable<DrawingViewModel>> GetAll(
        CancellationToken cancellationToken)
    {
        var drawings = await _drawingService
            .GetAll(cancellationToken);

        return _mapper.Map<IEnumerable<DrawingViewModel>>(drawings);
    }

    [HttpPost]
    public async Task<DrawingViewModel> Create(
        [FromBody] ChangeDrawingViewModel changeDrawingViewModel,
        CancellationToken cancellationToken)
    {
        await _changeDrawingViewModelValidator
            .ValidateAndThrowAsync(changeDrawingViewModel, cancellationToken);

        var drawingModel = _mapper.Map<Drawing>(changeDrawingViewModel);

        var drawing = await _drawingService
            .Create(drawingModel, cancellationToken);

        return _mapper.Map<DrawingViewModel>(drawing);
    }

    [HttpPut("{id}")]
    public async Task<DrawingViewModel> Update(
        int id,
        [FromBody] ChangeDrawingViewModel changeDrawingViewModel,
      CancellationToken cancellationToken)
    {
        await _changeDrawingViewModelValidator
            .ValidateAndThrowAsync(changeDrawingViewModel, cancellationToken);

        var drawingModel = _mapper.Map<Drawing>(changeDrawingViewModel);
        drawingModel.Id = id;

        var result = await _drawingService
            .Update(drawingModel, cancellationToken);

        return _mapper.Map<DrawingViewModel>(result);
    }

    [HttpDelete("{id}")]
    public Task Delete(
        int id,
        CancellationToken cancellationToken)
    {
        return _drawingService.Delete(id, cancellationToken);
    }
}