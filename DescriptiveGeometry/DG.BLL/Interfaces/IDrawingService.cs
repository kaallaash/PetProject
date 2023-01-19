using DG.BLL.Models;
using DG.Core.Models;
using DG.Core.Services;

namespace DG.BLL.Interfaces;

public interface IDrawingService<T1, in T2> : IBaseCrudService<T1, T2>
{
    Task<PagedList<Drawing>> GetByParameters(
        SearchParameters parameters,
        CancellationToken cancellationToken);
}
