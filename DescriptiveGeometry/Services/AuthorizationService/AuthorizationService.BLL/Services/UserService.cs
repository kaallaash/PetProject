using AuthorizationService.BLL.Exceptions;
using AuthorizationService.BLL.Interfaces;
using AuthorizationService.BLL.Models;
using AuthorizationService.BLL.Models.Enums;
using AuthorizationService.DAL;
using AuthorizationService.DAL.Entities;
using AuthorizationService.DAL.Interfaces.Repositories;
using System.Diagnostics.Metrics;

namespace AuthorizationService.BLL.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(
        IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<User> Create(User user, CancellationToken cancellationToken)
    {
        var users = await _userRepository.GetAll(cancellationToken);

        if (users.Any(u => u.Email == user.Email))
        {
            throw new ArgumentException("Description already exists");
        }

        var userEntity = new AuthorizationService.DAL.Entities.UserEntity()
        {
            Id = user.Id,
            Email = user.Email,
            Name = user.Name,
            Password = user.Password,
            Role = (AuthorizationService.DAL.Entities.Enums.Role)(int)user.Role
        };

        var createdUser = await _userRepository.Create(userEntity, cancellationToken);

        var userAfterMap = new User()
        {
            Id = createdUser.Id,
            Email = createdUser.Email,
            Name = createdUser.Name,
            Password = createdUser.Password,
            Role = (Role)(int)createdUser.Role
        };

        return userAfterMap;
    }

    public async Task Delete(int id, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetById(id, cancellationToken);

        if (user is null)
        {
            throw new NotFoundException("The drawing is not found");
        }

        var userEntity = new AuthorizationService.DAL.Entities.UserEntity()
        {
            Id = user.Id,
            Email = user.Email,
            Name = user.Name,
            Password = user.Password,
            Role = (AuthorizationService.DAL.Entities.Enums.Role)(int)user.Role
        };

        await _userRepository.Delete(userEntity, cancellationToken);
    }

    public async Task<User> Get(int id, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetById(id, cancellationToken);

        if (user != null)
        {
            return new User()
            {
                Id = user.Id,
                Email = user.Email,
                Name = user.Name,
                Password = user.Password,
                Role = (Role)(int)user.Role
            };
        }

        throw new NotImplementedException();
    }

    public async Task<List<User>> GetAll(CancellationToken cancellationToken)
    {
        //throw new NotImplementedException();
        var userEntities = await _userRepository.GetAll(cancellationToken);

        var users = new List<User>();

        foreach (var userEntity in userEntities)
        {
            var user = new User()
            {
                Id = userEntity.Id,
                Name = userEntity.Name,
                Email = userEntity.Email,
                Password = userEntity.Password,
                Role = (Role)userEntity.Role
            };

            users.Add(user);
        }

        return users;
        //return _mapper.Map<List<Drawing>>(drawings);
    }

    public async Task<User> Update(User user, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
        //var drawingEntity = await _drawingRepository.GetById(drawing.Id, cancellationToken);

        //if (drawingEntity is null)
        //{
        //    throw new NotFoundException("The drawing is not found");
        //}

        //var drawingEntities = await _drawingRepository.GetAll(cancellationToken);

        //var result = drawingEntities.Any(d =>
        //    d.Description?.Text == drawing.Description?.Text
        //    && d.Id != drawing.Id);

        //if (result)
        //{
        //    throw new ArgumentException("The same description already exists");
        //}

        //var drawingAfterMap = _mapper.Map<DrawingEntity>(drawing);
        //await _drawingRepository.Update(drawingAfterMap, cancellationToken);

        //return drawing;
    }
}
