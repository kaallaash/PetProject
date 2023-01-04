using AuthorizationService.BLL.Exceptions;
using AuthorizationService.BLL.Interfaces;
using AuthorizationService.BLL.Models;
using AuthorizationService.DAL.Entities;
using AuthorizationService.DAL.Interfaces.Repositories;

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
        throw new NotImplementedException();
        //var drawingEntities = await _drawingRepository.GetAll(cancellationToken);

        //if (drawingEntities.Any(d =>
        //        d.Description?.Text == drawing.Description?.Text))
        //{
        //    throw new ArgumentException("Description already exists");
        //}

        //var drawingEntity = _mapper.Map<DrawingEntity>(drawing);
        //var createdDrawing = await _drawingRepository.Create(drawingEntity, cancellationToken);

        //return _mapper.Map<Drawing>(createdDrawing);
    }

    public async Task Delete(int id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
        //var drawing = await _drawingRepository.GetById(id, cancellationToken);

        //if (drawing is null)
        //{
        //    throw new NotFoundException("The drawing is not found");
        //}

        //await _drawingRepository.Delete(drawing, cancellationToken);
    }

    public async Task<User> Get(int id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
        //var drawing = await _drawingRepository.GetById(id, cancellationToken);

        //return _mapper.Map<Drawing>(drawing);
    }

    public async Task<List<User>> GetAll(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
        //var drawings = await _drawingRepository.GetAll(cancellationToken);

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
