using AuthorizationService.DAL.Context;
using AuthorizationService.DAL.Entities;
using AuthorizationService.DAL.Interfaces.Repositories;
using AuthorizationService.DAL.Repositories;
using AuthorizationService.DAL.IntegrationTests.Entities;
using Microsoft.EntityFrameworkCore;
using Shouldly;
using Xunit;
using static AuthorizationService.DAL.IntegrationTests.Entities.TestUserEntity;

namespace AuthorizationService.DAL.IntegrationTests;

public class UserRepositoryIntegrationTests : IDisposable
{
    private readonly IUserRepository<UserEntity> _userRepository;
    private readonly DatabaseContext _context;

    public UserRepositoryIntegrationTests()
    {
        var options = new DbContextOptionsBuilder<DatabaseContext>()
            .UseInMemoryDatabase(databaseName: "test_user_dal_db")
        .Options;

        _context = new DatabaseContext(options);
        _userRepository = new UserRepository(_context);
    }

    public async void Dispose() => await _context.Database.EnsureDeletedAsync();

    [Theory]
    [MemberData(nameof(GetValidUserEntities), MemberType = typeof(TestUserEntity))]
    public async Task GetById_ValidId_ReturnsUserEntity(UserEntity user)
    {
        await AddAsync(_context, user);

        var createdUserEntity = await _context.Users
            .FirstOrDefaultAsync(u => u.Email == user.Email
                                      && u.Password == user.Password);

        createdUserEntity.ShouldNotBeNull();

        var actualUser = await _userRepository.GetById(createdUserEntity.Id, default);

        actualUser.ShouldNotBeNull();
        actualUser.Id.ShouldBe(createdUserEntity.Id);
    }

    [Theory]
    [MemberData(nameof(GetValidUserEntities), MemberType = typeof(TestUserEntity))]
    public async Task GetById_InvalidId_ReturnsNull(UserEntity user)
    {
        await AddAsync(_context, user);

        var actualUser = await _userRepository.GetById(user.Id + 1, default);

        actualUser.ShouldBeNull();
    }

    [Theory]
    [MemberData(nameof(GetValidUserEntities), MemberType = typeof(TestUserEntity))]
    public async Task GetByEmail_ValidEmail_ReturnsUserEntity(UserEntity user)
    {
        await AddAsync(_context, user);

        var createdUserEntity = await _context.Users
            .FirstOrDefaultAsync(u => u.Email == user.Email
                                      && u.Password == user.Password);

        createdUserEntity.ShouldNotBeNull();

        var actualUser = await _userRepository.GetByEmail(createdUserEntity.Email, default);

        actualUser.ShouldNotBeNull();
        actualUser.Id.ShouldBe(createdUserEntity.Id);
    }

    [Fact]
    public async Task GetAll_ReturnsUserEntities()
    {
        await AddAsync(_context, GetValidUserEntitiesWithId());
        var usersCount = _context.Users.Count();

        var actualUsers = await _userRepository.GetAll(default);
        
        actualUsers.Count().ShouldBe(usersCount);
    }

    [Theory]
    [MemberData(nameof(GetValidUserEntities), MemberType = typeof(TestUserEntity))]
    public async Task Create_ValidUserEntity_EntityIsCreated(
        UserEntity expectedValidUser)
    {
        var actualUser = await _userRepository.Create(expectedValidUser, default);

        actualUser.ShouldNotBeNull();
        actualUser.Email.ShouldBe(expectedValidUser.Email);
        actualUser.Password.ShouldBe(expectedValidUser.Password);
    }

    [Theory]
    [MemberData(nameof(GetValidUserEntities), MemberType = typeof(TestUserEntity))]
    public async Task Update_ValidUserEntity_EntityIsUpdated(
        UserEntity user)
    {
        await AddAsync(_context, user);

        var updatedUserEntity = user;
        updatedUserEntity.Email += ".com";

        var actualUser = await _userRepository.Update(updatedUserEntity, default);

        actualUser.ShouldNotBeNull();
        actualUser.Email.ShouldBe(updatedUserEntity.Email);        
    }

    [Theory]
    [MemberData(nameof(GetValidUserEntities), MemberType = typeof(TestUserEntity))]
    public async Task Update_InvalidUserEntity_ThrowsException(
        UserEntity user)
    {
        await AddAsync(_context, user);

        var updatedUserEntity = user;
        updatedUserEntity.Id += 1;

        await Should.ThrowAsync<InvalidOperationException>
            (async () => await _userRepository.Update(updatedUserEntity, default));
    }

    [Theory]
    [MemberData(nameof(GetValidUserEntities), MemberType = typeof(TestUserEntity))]
    public async Task Delete_ValidId_EntityIsDeleted(UserEntity user)
    {
        await AddAsync(_context, user);

        await Should.NotThrowAsync(
            async () => await _userRepository.Delete(user, default));

        var deletedUser = await _context.Users.FirstOrDefaultAsync(u => u.Id == user.Id);

        deletedUser.ShouldBeNull();
    }

    [Theory]
    [MemberData(nameof(GetValidUserEntities), MemberType = typeof(TestUserEntity))]
    public async Task Delete_InvalidId_ThrowsException(UserEntity user)
    {
        await Should.ThrowAsync<DbUpdateConcurrencyException>(
            async () => await _userRepository.Delete(user, default));
    }

    private static async Task AddAsync(DatabaseContext context, UserEntity userEntity)
    {
        await context.Users.AddAsync(userEntity, default);
        await context.SaveChangesAsync();
    }

    private static async Task AddAsync(DatabaseContext context, IEnumerable<UserEntity> userEntities)
    {
        await context.Users.AddRangeAsync(userEntities, default);
        await context.SaveChangesAsync();
    }
}