using AutoMapper;
using Moq;
using Xunit;
using AuthorizationService.BLL.Models;
using AuthorizationService.DAL.Interfaces.Repositories;
using AuthorizationService.DAL.Entities;
using AuthorizationService.BLL.Services;
using static AuthorizationService.BLL.Tests.Models.TestUserModel;
using static AuthorizationService.BLL.Tests.Entities.TestUserEntity;

namespace AuthorizationService.BLL.Tests;

public class UserServiceTests
{
    private readonly UserService _userService;
    private readonly Mock<IUserRepository<UserEntity>> _userRepository;
    private readonly Mock<IMapper> _mapper;

    public UserServiceTests()
    {
        _userRepository = new Mock<IUserRepository<UserEntity>>();
        _mapper = new Mock<IMapper>();
        _userService = new UserService(_userRepository.Object, _mapper.Object);
    }

    [Fact]
    public async Task GetById_ValidId_ReturnsUserModel()
    {
        _userRepository
                .Setup(ur =>
                    ur.GetById(ValidUserModel.Id, default))
                .ReturnsAsync(ValidUserEntity);
        _mapper
            .Setup(m => m.Map<User>(ValidUserEntity))
            .Returns(ValidUserModel);

        var result = await _userService.GetById(ValidUserModel.Id, default);

        Assert.Equal(result.Email, ValidUserModel.Email);
        Assert.Equal(result.Password, ValidUserModel.Password);
    }

    [Fact]
    public async Task GetAll_ReturnsUserModelList()
    {
        _userRepository
            .Setup(ur =>
                ur.GetAll(default))
            .ReturnsAsync(ValidUserEntities);
        _mapper
            .Setup(m =>m.Map<IEnumerable<User>>(ValidUserEntities))
            .Returns(ValidUserModels);

        var result = await _userService.GetAll(default);
        
        Assert.Equal(result.Count(), ValidUserEntities.Count());
    }

    [Fact]
    public async Task Create_ValidUserModel_ReturnsUserModel()
    {
        _userRepository
            .Setup(ur => 
                ur.Create(ValidUserEntity, default))
            .ReturnsAsync(ValidUserEntity);
        _mapper
            .Setup(m => m.Map<User>(ValidUserEntity))
            .Returns(ValidUserModel);
        _mapper
            .Setup(m => m.Map<UserEntity>(ValidUserModel))
            .Returns(ValidUserEntity);

        var result = await _userService.Create(ValidUserModel, default);

        Assert.Equal(result.Name, ValidUserModel.Name);
        Assert.Equal(result.Email, ValidUserModel.Email);
    }

    [Fact]
    public async Task Update_ValidUserModel_ReturnsUserModel()
    {
        _userRepository
            .Setup(ur =>
                ur.Update(ValidUserEntity, default))
            .ReturnsAsync(ValidUserEntity);
        _userRepository
            .Setup(ur => 
                ur.GetById(ValidUserEntity.Id, default))
            .ReturnsAsync(ValidUserEntity);
        _mapper
            .Setup(m => m.Map<User>(ValidUserEntity))
            .Returns(ValidUserModel);
        _mapper
            .Setup(m => m.Map<UserEntity>(ValidUserModel))
            .Returns(ValidUserEntity);

        var result = await _userService.Update(ValidUserModel, default);

        Assert.Equal(result.Id, ValidUserModel.Id);
        Assert.Equal(result.Email, ValidUserModel.Email);
    }
}
