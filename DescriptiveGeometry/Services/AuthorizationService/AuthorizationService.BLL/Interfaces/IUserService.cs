using AuthorizationService.BLL.Models;
using DG.Core.Services;

namespace AuthorizationService.BLL.Interfaces;

public interface IUserService<T1, in T2> : IBaseCrudService<T1, T2>
{
    Task<T1> GetByEmail(string email,
        CancellationToken cancellationToken);
}
