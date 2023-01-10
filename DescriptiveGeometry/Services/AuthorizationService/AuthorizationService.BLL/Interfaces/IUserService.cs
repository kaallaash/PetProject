using AuthorizationService.BLL.Models;
using DG.Core.Services;

namespace AuthorizationService.BLL.Interfaces;

public interface IUserService<T1, in T2> : IBaseCrudService<T1, T2>
{
    //Task<User> GetById(int id,
    //    CancellationToken cancellationToken);

    Task<T1> GetByEmail(string email,
        CancellationToken cancellationToken);

    //Task<List<User>> GetAll(
    //    CancellationToken cancellationToken);

    //Task<User> Create(User user,
    //    CancellationToken cancellationToken);

    //Task<User> Update(User drawing,
    //    CancellationToken cancellationToken);

    //Task Delete(int id,
    //    CancellationToken cancellationToken);
}
