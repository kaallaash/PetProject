using AuthorizationService.BLL.Models;

namespace AuthorizationService.BLL.Interfaces;

public interface IUserService
{
    Task<User> Get(int id,
        CancellationToken cancellationToken);
    
    Task<List<User>> GetAll(
        CancellationToken cancellationToken);

    Task<User> Create(User user,
        CancellationToken cancellationToken);

    Task<User> Update(User drawing,
        CancellationToken cancellationToken);

    Task Delete(int id,
        CancellationToken cancellationToken);
}
