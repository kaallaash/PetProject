using DG.DAL.Context;
using DG.DAL.Entities;
using DG.DAL.Interfaces.Repositories;
using DG.DAL.Repositories;
using DG.DAL.IntegrationTests.Entities;
using Microsoft.EntityFrameworkCore;
using Shouldly;
using Xunit;
using static DG.DAL.IntegrationTests.Entities.TestDrawingEntity;
using DG.DAL.Models;

namespace DG.DAL.IntegrationTests;

public class DrawingRepositoryIntegrationTests : IDisposable
{
    private readonly IDrawingRepository<DrawingEntity> _drawingRepository;
    private readonly DatabaseContext _context;

    public DrawingRepositoryIntegrationTests()
    {
        var options = new DbContextOptionsBuilder<DatabaseContext>()
            .UseInMemoryDatabase(databaseName: "test_dal_db")
            .Options;

        _context = new DatabaseContext(options);
        _drawingRepository = new DrawingRepository(_context);
    }

    public async void Dispose() => await _context.Database.EnsureDeletedAsync();

    [Theory]
    [MemberData(nameof(GetValidDrawingEntities), MemberType = typeof(TestDrawingEntity))]
    public async Task GetById_ValidId_ReturnsDrawingEntity(DrawingEntity drawing)
    {
        await AddAsync(_context, drawing);

        var createdDrawingEntity = await _context.Drawings
            .FirstOrDefaultAsync(d => d.DrawingPhotoLink == drawing.DrawingPhotoLink);

        createdDrawingEntity.ShouldNotBeNull();

        var actualDrawing = await _drawingRepository.GetById(createdDrawingEntity.Id, default);

        actualDrawing.ShouldNotBeNull();
        actualDrawing.Id.ShouldBe(createdDrawingEntity.Id);
    }

    [Theory]
    [MemberData(nameof(GetValidDrawingEntities), MemberType = typeof(TestDrawingEntity))]
    public async Task GetById_InvalidId_ReturnsNull(DrawingEntity drawing)
    {
        await AddAsync(_context, drawing);

        var actualDrawing = await _drawingRepository.GetById(drawing.Id + 1, default);

        actualDrawing.ShouldBeNull();
    }

    [Theory]
    [MemberData(nameof(GetValidPageParameters), MemberType = typeof(TestDrawingEntity))]
    public async Task GetByParameters_ValidParameters_ReturnsDrawingEntities(SearchParameters pageParameters)
    {
        await AddAsync(_context, GetValidDrawingEntitiesWithId());

        var createdDrawingEntities = await _context.Drawings
            .AsNoTracking()
            .Include(d => d.Description)
            .Skip(pageParameters.Skip)
            .Take(pageParameters.Take)
            .ToListAsync(default);

        var drawingCount = await _context.Drawings
            .AsNoTracking().CountAsync(default);

        var totalPages = drawingCount / pageParameters.Take;

        if (!IsRemainderEqualsZero(drawingCount, pageParameters.Take))
        {
            totalPages += 1;
        }

        var actualPagedList =
            await _drawingRepository.GetByParameters(pageParameters, default);

        var actualDrawingEntitiesArray = actualPagedList.Collection?.ToArray();
        var createdDrawingEntitiesArray = createdDrawingEntities.ToArray();

        actualPagedList.TotalPages.ShouldBe(totalPages);
        actualDrawingEntitiesArray.ShouldNotBeNull();
        actualDrawingEntitiesArray.Length.ShouldBe(createdDrawingEntities.Count);
        actualDrawingEntitiesArray.Length.ShouldBe(createdDrawingEntities.Count);

        for (var i = 0; i < createdDrawingEntitiesArray.Length; i++)
        {
            actualDrawingEntitiesArray[i].Id.ShouldBe(createdDrawingEntitiesArray[i].Id);
            actualDrawingEntitiesArray[i].DrawingPhotoLink.ShouldBe(createdDrawingEntitiesArray[i].DrawingPhotoLink);
            actualDrawingEntitiesArray[i].Description?.DescriptionPhotoLink.ShouldBe(createdDrawingEntitiesArray[i]?.Description?.DescriptionPhotoLink);
            actualDrawingEntitiesArray[i].Description?.Points.ShouldBe(createdDrawingEntitiesArray[i]?.Description?.Points);
            actualDrawingEntitiesArray[i].Description?.Text.ShouldBe(createdDrawingEntitiesArray[i]?.Description?.Text);
        }
    }

    [Fact]
    public async Task GetAll_ReturnsDrawingEntities()
    {
        await AddAsync(_context, GetValidDrawingEntitiesWithId());
        var drawingsCount = _context.Drawings.Count();

        var actualDrawings = await _drawingRepository.GetAll(default);

        actualDrawings.ShouldNotBeNull();
        actualDrawings.Count().ShouldBe(drawingsCount);
    }

    [Theory]
    [MemberData(nameof(GetValidDrawingEntities), MemberType = typeof(TestDrawingEntity))]
    public async Task Create_ValidDrawingEntity_EntityIsCreated(
        DrawingEntity expectedValidDrawing)
    {
        var actualDrawing = await _drawingRepository.Create(expectedValidDrawing, default);

        actualDrawing.ShouldNotBeNull();
        actualDrawing.Id.ShouldBe(expectedValidDrawing.Id);
        actualDrawing.Description.ShouldBe(expectedValidDrawing.Description);
    }

    [Theory]
    [MemberData(nameof(GetValidDrawingEntities), MemberType = typeof(TestDrawingEntity))]
    public async Task Update_ValidDrawingEntity_EntityIsUpdated(
        DrawingEntity drawing)
    {
        await AddAsync(_context, drawing);

        var updatedDrawingEntity = drawing;
        updatedDrawingEntity.DownloadsCount += 1;

        var actualDrawing = await _drawingRepository.Update(updatedDrawingEntity, default);

        actualDrawing.ShouldNotBeNull();
        actualDrawing.DownloadsCount.ShouldBe(updatedDrawingEntity.DownloadsCount);
    }

    [Theory]
    [MemberData(nameof(GetValidDrawingEntities), MemberType = typeof(TestDrawingEntity))]
    public async Task Update_InvalidDrawingEntity_ThrowsException(
        DrawingEntity drawing)
    {
        await AddAsync(_context, drawing);

        var updatedDrawingEntity = drawing;
        updatedDrawingEntity.Id += 1;

        await Should.ThrowAsync<InvalidOperationException>
            (async () => await _drawingRepository.Update(updatedDrawingEntity, default));
    }

    [Theory]
    [MemberData(nameof(GetValidDrawingEntities), MemberType = typeof(TestDrawingEntity))]
    public async Task Delete_ValidId_EntityIsDeleted(DrawingEntity drawing)
    {
        await AddAsync(_context, drawing);

        await Should.NotThrowAsync(
            async () => await _drawingRepository.Delete(drawing, default));

        var deletedDrawing = await _context.Drawings.FirstOrDefaultAsync(d => d.Id == drawing.Id);

        deletedDrawing.ShouldBeNull();
    }

    [Theory]
    [MemberData(nameof(GetValidDrawingEntities), MemberType = typeof(TestDrawingEntity))]
    public async Task Delete_InvalidId_ThrowsException(DrawingEntity drawing)
    {
        await Should.ThrowAsync<DbUpdateConcurrencyException>(
            async () => await _drawingRepository.Delete(drawing, default));
    }

    private static async Task AddAsync(DatabaseContext context, DrawingEntity drawingEntity)
    {
        await context.Drawings.AddAsync(drawingEntity, default);
        await context.SaveChangesAsync();
    }

    private static async Task AddAsync(DatabaseContext context, IEnumerable<DrawingEntity> drawingEntities)
    {
        await context.Drawings.AddRangeAsync(drawingEntities, default);
        await context.SaveChangesAsync();
    }

    private static bool IsRemainderEqualsZero(int divisible, int divider)
    {
        return divisible % divider == 0;
    }
}