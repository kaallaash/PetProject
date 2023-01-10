using AuthorizationService.DAL.Entities;
using DG.Core.Repositories;

namespace AuthorizationService.DAL.Interfaces.Repositories;

public interface IUserRepository<T1> : IBaseCrudRepository<T1>
{
    //Task<<UserEntity>> GetAll(CancellationToken cancellationToken);
    //Task<UserEntity?> GetById(int id, CancellationToken cancellationToken);
    Task<T1?> GetByEmail(string email, CancellationToken cancellationToken);
    //Task<UserEntity> Create(UserEntity user, CancellationToken cancellationToken);
    //Task<UserEntity> Update(UserEntity user, CancellationToken cancellationToken);
    //Task Delete(UserEntity drawing, CancellationToken cancellationToken);
}