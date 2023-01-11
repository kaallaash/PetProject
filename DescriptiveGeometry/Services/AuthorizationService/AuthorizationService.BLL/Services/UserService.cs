using AuthorizationService.BLL.Exceptions;
using AuthorizationService.BLL.Interfaces;
using AuthorizationService.BLL.Models;
using AuthorizationService.BLL.Models.Enums;
using AuthorizationService.DAL.Entities;
using AuthorizationService.DAL.Interfaces.Repositories;
using AutoMapper;

namespace AuthorizationService.BLL.Services;

public class UserService : IUserService<User, int>
{
    private readonly IUserRepository<UserEntity> _userRepository;
    private readonly IMapper _mapper;

    public UserService(
        IUserRepository<UserEntity> userRepository,
        IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<User> Create(User user, CancellationToken cancellationToken)
    {
        var userEntities = await _userRepository.GetAll(cancellationToken);

        if (userEntities.Any(u =>
                u?.Email == user?.Email))
        {
            throw new ArgumentException("email already exists");
        }

        var userEntity = _mapper.Map<UserEntity>(user);
        var createdUser = await _userRepository.Create(userEntity, cancellationToken);

        return _mapper.Map<User>(createdUser);
    }

    public async Task Delete(int id, CancellationToken cancellationToken)
    {
        var userEntity = await _userRepository.GetById(id, cancellationToken);

        if (userEntity is null)
        {
            throw new NotFoundException("The user is not found");
        }

        await _userRepository.Delete(userEntity, cancellationToken);
    }

    public async Task<User?> GetById(int id, CancellationToken cancellationToken)
    {
        var userEntity = await _userRepository.GetById(id, cancellationToken);

        return _mapper.Map<User>(userEntity);
    }

    public async Task<User> GetByEmail(string email, CancellationToken cancellationToken)
    {
        var userEntity = await _userRepository.GetByEmail(email, cancellationToken);

        return _mapper.Map<User>(userEntity);
    }

    public async Task<IEnumerable<User>> GetAll(CancellationToken cancellationToken)
    {
        var userEntities = await _userRepository.GetAll(cancellationToken);
        
        return _mapper.Map<List<User>>(userEntities);
    }

    public async Task<User> Update(User user, CancellationToken cancellationToken)
    {
        var userEntity = await _userRepository.GetById(user.Id, cancellationToken);

        if (userEntity is null)
        {
            throw new NotFoundException("The user is not found");
        }

        var drawingEntities = await _userRepository.GetAll(cancellationToken);

        var result = drawingEntities.Any(u =>
            u?.Email == user?.Email
            && u?.Id != user?.Id);

        if (result)
        {
            throw new ArgumentException("The same email already exists");
        }

        var userAfterMap = _mapper.Map<UserEntity>(user);
        await _userRepository.Update(userAfterMap, cancellationToken);

        return user;
    }
}
