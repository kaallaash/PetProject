using AuthorizationService.DAL.Entities;
using DG.Core.Repositories;

namespace AuthorizationService.DAL.Interfaces.Repositories;

public interface IUserRepository<T1> : IBaseCrudRepository<T1>
{
    Task<T1?> GetByEmail(string email, CancellationToken cancellationToken);
}