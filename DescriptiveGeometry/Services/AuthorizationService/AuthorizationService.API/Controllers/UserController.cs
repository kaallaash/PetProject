using AuthorizationService.API.ViewModels;
using AuthorizationService.BLL.Interfaces;
using AuthorizationService.BLL.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthorizationService.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : Controller
{
    private readonly IUserService<User, int> _userService;
    private readonly IMapper _mapper;

    public UserController(
        IUserService<User, int> userService,
        IMapper mapper)
    {
        _userService = userService;
        _mapper = mapper;
    }

    [Authorize]
    [HttpGet("{id}")]
    public async Task<UserViewModel> Get(
        int id,
        CancellationToken cancellationToken)
    {
        var user = await _userService
            .GetById(id, cancellationToken);

        return _mapper.Map<UserViewModel>(user);
    }

    [Authorize]
    [HttpGet]
    public async Task<IEnumerable<UserViewModel>> GetAll(
        CancellationToken cancellationToken)
    {
        var users = await _userService
            .GetAll(cancellationToken);

        return _mapper.Map<IEnumerable<UserViewModel>>(users);
    }

    [HttpPost]
    public async Task<UserViewModel> Create(
        [FromBody] ChangeUserViewModel changeUserViewModel,
        CancellationToken cancellationToken)
    {
        var userModel = _mapper.Map<User>(changeUserViewModel);

        var user = await _userService
            .Create(userModel, cancellationToken);

        return _mapper.Map<UserViewModel>(user);
    }

    [Authorize]
    [HttpPut("{id}")]
    public async Task<UserViewModel> Update(
        int id,
        [FromBody] ChangeUserViewModel changeUserViewModel,
      CancellationToken cancellationToken)
    {
        var userModel = _mapper.Map<User>(changeUserViewModel);
        userModel.Id = id;

        var result = await _userService
            .Update(userModel, cancellationToken);

        return _mapper.Map<UserViewModel>(result);
    }

    [Authorize]
    [HttpDelete("{id}")]
    public Task Delete(
        int id,
        CancellationToken cancellationToken)
    {
        return _userService.Delete(id, cancellationToken);
    }
}