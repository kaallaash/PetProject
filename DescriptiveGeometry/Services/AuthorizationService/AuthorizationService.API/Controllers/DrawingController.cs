using AuthorizationService.API.ViewModels;
using AuthorizationService.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AuthorizationService.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DrawingController : Controller
{
    private readonly IUserService _userService;

    public DrawingController(
        IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet("{id}")]
    public async Task<UserViewModel> Get(
        int id,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
        //var drawing = await _userService
        //    .Get(id, cancellationToken);

        //return _mapper.Map<DrawingViewModel>(drawing);
    }

    [HttpGet]
    public async Task<IEnumerable<UserViewModel>> GetAll(
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
        //var drawings = await _userService
        //    .GetAll(cancellationToken);

        //return _mapper.Map<IEnumerable<DrawingViewModel>>(drawings); ;
    }

    [HttpPost]
    public async Task<UserViewModel> Create(
        [FromBody] ChangeUserViewModel changeDrawingViewModel,
        CancellationToken cancellationToken)
    {

        throw new NotImplementedException();
        //await _changeDrawingViewModelValidator
        //    .ValidateAndThrowAsync(changeDrawingViewModel, cancellationToken);

        //var drawingModel = _mapper.Map<Drawing>(changeDrawingViewModel);

        //var drawing = await _userService
        //    .Create(drawingModel, cancellationToken);

        //return _mapper.Map<DrawingViewModel>(drawing);
    }

    [HttpPut("{id}")]
    public async Task<UserViewModel> Update(
        int id,
        [FromBody] ChangeUserViewModel changeDrawingViewModel,
      CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
        //await _changeDrawingViewModelValidator
        //    .ValidateAndThrowAsync(changeDrawingViewModel, cancellationToken);

        //var drawingModel = _mapper.Map<Drawing>(changeDrawingViewModel);
        //drawingModel.Id = id;

        //var result = await _userService
        //    .Update(drawingModel, cancellationToken);

        //return _mapper.Map<DrawingViewModel>(result);
    }

    [HttpDelete("{id}")]
    public Task Delete(
        int id,
        CancellationToken cancellationToken)
    {
        return _userService.Delete(id, cancellationToken);
    }
}