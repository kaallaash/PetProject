using DG.Core.Models;
using DG.DAL.Context;
using DG.DAL.Entities;
using DG.DAL.Interfaces.Repositories;
using DG.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DG.DAL.Repositories;

public class DrawingRepository : IDrawingRepository<DrawingEntity>
{
    private readonly DatabaseContext _db;

    public DrawingRepository(DatabaseContext db)
    {
        _db = db;
    }
    public async Task<DrawingEntity> Create(DrawingEntity drawing, CancellationToken cancellationToken)
    {
        var drawingEntity = await _db.Drawings.AddAsync(drawing, cancellationToken);
        await _db.SaveChangesAsync(cancellationToken);

        return drawingEntity.Entity;
    }

    public async Task Delete(DrawingEntity drawing, CancellationToken cancellationToken)
    {
        _db.Drawings.Remove(drawing);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task<DrawingEntity?> GetById(int id, CancellationToken cancellationToken)
    {
        return await _db.Drawings
            .AsNoTracking()
            .Include(d => d.Description)
            .FirstOrDefaultAsync(d => d.Id == id, cancellationToken);
    }
        
    public async Task<PagedList<DrawingEntity>> GetByParameters(
        SearchParameters parameters,
        CancellationToken cancellationToken)
    {
        var collection = await _db.Drawings
            .AsNoTracking()
            .Include(d => d.Description)
            .Skip(parameters.Skip)
            .Take(parameters.Take)
            .ToListAsync(cancellationToken);

        var drawingCount = await _db.Drawings
            .AsNoTracking().CountAsync(cancellationToken);

        var totalPages = drawingCount / parameters.Take;

        if (!IsRemainderEqualsZero(drawingCount, parameters.Take))
        {
            totalPages += 1;
        }

        return new PagedList<DrawingEntity>(collection, totalPages);
    }

    public async Task<IEnumerable<DrawingEntity>> GetAll(CancellationToken cancellationToken)
    {
        return await _db.Drawings
            .AsNoTracking()
            .Include(d => d.Description)
            .ToListAsync(cancellationToken);
    }

    public async Task<DrawingEntity> Update(DrawingEntity drawing, CancellationToken cancellationToken)
    {
        _db.Entry(drawing).State = EntityState.Modified;

        var drawingById = await GetById(drawing.Id, cancellationToken);

        if (drawing.Description is not null && drawingById?.Description is not null)
        {
            drawing.Description.DrawingId = drawing.Id;
            drawing.Description.Id = drawingById.Description.Id;
            _db.Entry(drawing.Description).State = EntityState.Modified;
        }
        
        await _db.SaveChangesAsync(cancellationToken);

        return drawing;
    }

    private static bool IsRemainderEqualsZero(int divisible, int divider)
    {
        return divisible % divider == 0;
    }
}
