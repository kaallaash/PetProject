using AutoMapper;
using DG.BLL.Exceptions;
using DG.BLL.Interfaces;
using DG.BLL.Models;
using DG.Core.Models;
using DG.DAL.Entities;
using DG.DAL.Interfaces.Repositories;

namespace DG.BLL.Services;

public class DrawingService : IDrawingService<Drawing, int>
{
    private readonly IDrawingRepository<DrawingEntity> _drawingRepository;
    private readonly IMapper _mapper;

    public DrawingService(
        IDrawingRepository<DrawingEntity> drawingRepository,
        IMapper mapper)
    {
        _drawingRepository = drawingRepository;
        _mapper = mapper;
    }

    public async Task<Drawing> Create(Drawing drawing, CancellationToken cancellationToken)
    {
        var drawingEntities = await _drawingRepository.GetAll(cancellationToken);

        if (drawingEntities.Any(d =>
                d.Description?.Text == drawing.Description?.Text))
        {
            throw new ArgumentException("Description already exists");
        }

        var drawingEntity = _mapper.Map<DrawingEntity>(drawing);
        var createdDrawing = await _drawingRepository.Create(drawingEntity, cancellationToken);

        return _mapper.Map<Drawing>(createdDrawing);
    }

    public async Task Delete(int id, CancellationToken cancellationToken)
    {
        var drawing = await _drawingRepository.GetById(id, cancellationToken);

        if (drawing is null)
        {
            throw new NotFoundException("The drawing is not found");
        }

        await _drawingRepository.Delete(drawing, cancellationToken);
    }

    public async Task<Drawing?> GetById(int id, CancellationToken cancellationToken)
    {
        var drawing = await _drawingRepository.GetById(id, cancellationToken);

        return _mapper.Map<Drawing>(drawing);
    }

    public async Task<PagedList<Drawing>> GetByParameters(
        SearchParameters parameters,
        CancellationToken cancellationToken)
    {
        var dalSearchParameters = _mapper.Map<DAL.Models.SearchParameters>(parameters);
        var dalPagedList = await _drawingRepository.GetByParameters(dalSearchParameters, cancellationToken);

        return _mapper.Map<PagedList<Drawing>>(dalPagedList);
    }

    public async Task<IEnumerable<Drawing>> GetAll(CancellationToken cancellationToken)
    {
        var drawings = await _drawingRepository.GetAll(cancellationToken);

        return _mapper.Map<List<Drawing>>(drawings); 
    }

    public async Task<Drawing> Update(Drawing drawing, CancellationToken cancellationToken)
    {
        var drawingEntity = await _drawingRepository.GetById(drawing.Id, cancellationToken);

        if (drawingEntity is null)
        {
            throw new NotFoundException("The drawing is not found");
        }

        var drawingEntities = await _drawingRepository.GetAll(cancellationToken);

        var result = drawingEntities.Any(d =>
            d.Description?.Text == drawing.Description?.Text
            && d.Id != drawing.Id);

        if (result)
        {
            throw new ArgumentException("The same description already exists");
        }

        var drawingAfterMap = _mapper.Map<DrawingEntity>(drawing);
        await _drawingRepository.Update(drawingAfterMap, cancellationToken);

        return drawing;
    }
}
