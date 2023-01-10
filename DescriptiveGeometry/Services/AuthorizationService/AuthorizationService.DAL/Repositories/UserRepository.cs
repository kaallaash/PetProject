using AuthorizationService.DAL.Entities;
using AuthorizationService.DAL.Interfaces.Repositories;
using AuthorizationService.DAL.Context;
using Microsoft.EntityFrameworkCore;

namespace AuthorizationService.DAL.Repositories;

public class UserRepository : IUserRepository<UserEntity>
{
    private readonly DatabaseContext _db;

    public UserRepository(DatabaseContext db)
    {
        _db = db;
    }
    public async Task<UserEntity> Create(UserEntity user, CancellationToken cancellationToken)
    {
        var drawingEntity = await _db.Users.AddAsync(user, cancellationToken);
        await _db.SaveChangesAsync(cancellationToken);

        return drawingEntity.Entity;
    }

    public async Task Delete(UserEntity user, CancellationToken cancellationToken)
    {
        _db.Users.Remove(user);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task<UserEntity?> GetById(int id, CancellationToken cancellationToken)
    {
        return await _db.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Id == id, cancellationToken);
    }

    public async Task<UserEntity?> GetByEmail(string email, CancellationToken cancellationToken)
    {
        return await _db.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Email == email, cancellationToken);
    }

    public async Task<IEnumerable<UserEntity>> GetAll(CancellationToken cancellationToken)
    {
        return await _db.Users
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<UserEntity> Update(UserEntity user, CancellationToken cancellationToken)
    {
        _db.Entry(user).State = EntityState.Modified;
        await _db.SaveChangesAsync(cancellationToken);

        return user;
    }
}