using AuthorizationService.DAL.Entities;

namespace AuthorizationService.DAL.Interfaces.Repositories;

public interface IUserRepository
{
    Task<IEnumerable<UserEntity>> GetAll(CancellationToken cancellationToken);
    Task<UserEntity?> GetById(int id, CancellationToken cancellationToken);
    Task<UserEntity?> GetByEmail(string email, CancellationToken cancellationToken);
    Task<UserEntity> Create(UserEntity user, CancellationToken cancellationToken);
    Task<UserEntity> Update(UserEntity user, CancellationToken cancellationToken);
    Task Delete(UserEntity drawing, CancellationToken cancellationToken);
}