using DG.Core.Models;
using DG.Core.Repositories;
using DG.DAL.Entities;
using DG.DAL.Models;

namespace DG.DAL.Interfaces.Repositories;
public interface IDrawingRepository<T1> : IBaseCrudRepository<T1>
{
    Task<PagedList<T1>> GetByParameters(
        SearchParameters parameters,
        CancellationToken cancellationToken);
}