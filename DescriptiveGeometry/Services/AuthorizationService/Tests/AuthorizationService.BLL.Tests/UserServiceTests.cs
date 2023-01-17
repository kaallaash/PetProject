using AutoMapper;
using Moq;
using Xunit;
using AuthorizationService.BLL.Models;
using AuthorizationService.DAL.Interfaces.Repositories;
using AuthorizationService.DAL.Entities;
using AuthorizationService.BLL.Services;
using static AuthorizationService.BLL.Tests.Models.TestUserModel;
using static AuthorizationService.BLL.Tests.Entities.TestUserEntity;
using Shouldly;
using AuthorizationService.BLL.Exceptions;
using AuthorizationService.BLL.Tests.Helpers;

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
                    ur.GetById(GetValidUserModel.Id, default))
                .ReturnsAsync(ValidUserEntity);
        _mapper
            .Setup(m => m.Map<User>(ValidUserEntity))
            .Returns(GetValidUserModel);

        var result = await _userService.GetById(GetValidUserModel.Id, default);

        Assert.Equal(result?.Email, GetValidUserModel.Email);
        Assert.Equal(result?.Password, GetValidUserModel.Password);
    }

    [Fact]
    public async Task GetByEmail_ValidEmail_ReturnsUserModel()
    {
        var validUserModel = GetValidUserModel;

        _userRepository
            .Setup(ur =>
                ur.GetByEmail(validUserModel.Email, default))
            .ReturnsAsync(ValidUserEntity);
        _mapper
            .Setup(m => m.Map<User>(ValidUserEntity))
            .Returns(validUserModel);

        var result = await _userService.GetByEmail(validUserModel.Email, default);

        Assert.Equal(result.Name, validUserModel.Name);
        Assert.Equal(result.Password, validUserModel.Password);
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
            .Returns(GetValidUserModels);

        var result = await _userService.GetAll(default);
        
        Assert.Equal(result.Count(), ValidUserEntities.Count());
    }

    [Fact]
    public async Task Create_ValidUserModel_ReturnsUserModel()
    {
        var validUserModel = GetValidUserModel;

        _userRepository
            .Setup(ur => 
                ur.Create(ValidUserEntity, default))
            .ReturnsAsync(ValidUserEntity);
        _mapper
            .Setup(m => m.Map<User>(ValidUserEntity))
            .Returns(GetValidUserModel);
        _mapper
            .Setup(m => m.Map<UserEntity>(validUserModel))
            .Returns(ValidUserEntity);

        var result = await _userService.Create(validUserModel, default);

        Assert.Equal(result.Name, validUserModel.Name);
        Assert.Equal(result.Email, validUserModel.Email);
    }

    [Fact]
    public async Task Create_ExistedEmail_ThrowArgumentException()
    {
        _userRepository
            .Setup(ur =>
                ur.GetAll(default))
            .ReturnsAsync(ValidUserEntities);

        await Should.ThrowAsync<ArgumentException>(
            async () => await _userService.Create(GetValidUserModel, default));
    }

    [Fact]
    public async Task Update_ValidUserModel_ReturnsUserModel()
    {
        var validUserModel = GetValidUserModel;

        _userRepository
            .Setup(ur =>
                ur.Update(ValidUserEntity, default))
            .ReturnsAsync(ValidUserEntity);
        _userRepository
            .Setup(ur =>
                ur.GetAll(default))
            .ReturnsAsync(ValidUserEntities);
        _userRepository
            .Setup(ur => 
                ur.GetById(ValidUserEntity.Id, default))
            .ReturnsAsync(ValidUserEntity);
        _mapper
            .Setup(m => m.Map<User>(ValidUserEntity))
            .Returns(validUserModel);
        _mapper
            .Setup(m => m.Map<UserEntity>(validUserModel))
            .Returns(ValidUserEntity);

        var result = await _userService.Update(validUserModel, default);

        Assert.Equal(result.Id, validUserModel.Id);
        Assert.Equal(result.Email, validUserModel.Email);
    }

    [Fact]
    public async Task Update_ExistedUserEmail_ThrowArgumentException()
    {
        var validUserModel = GetValidUserModel;

        _userRepository
            .Setup(ur =>
                ur.Update(ValidUserEntity, default))
            .ReturnsAsync(ValidUserEntity);
        _userRepository
            .Setup(ur =>
                ur.GetAll(default))
            .ReturnsAsync(ValidUserEntities);
        _userRepository
            .Setup(ur =>
                ur.GetById(ValidUserEntity.Id, default))
            .ReturnsAsync(ValidUserEntity);
        _mapper
            .Setup(m => m.Map<User>(ValidUserEntity))
            .Returns(validUserModel);
        _mapper
            .Setup(m => m.Map<UserEntity>(validUserModel))
            .Returns(ValidUserEntity);

        validUserModel.Email = UserModelHelper.Create(2).Email;

        await Should.ThrowAsync<ArgumentException>(
            async () => await _userService.Update(validUserModel, default));
    }

    [Fact]
    public async Task Delete_ValidId_NotThrowFoundException()
    {
        _userRepository
            .Setup(ur =>
                ur.GetById(ValidUserEntity.Id, default))
            .ReturnsAsync(ValidUserEntity);

        await Should.NotThrowAsync(
            async () => await _userService.Delete(ValidUserEntity.Id, default));
    }

    [Fact]
    public async Task Delete_InValidId_ThrowFoundException()
    {
        _userRepository
            .Setup(ur =>
                ur.GetById(ValidUserEntity.Id, default))
            .ReturnsAsync(ValidUserEntity);

        await Should.ThrowAsync<NotFoundException>(
            async () => await _userService.Delete(ValidUserEntity.Id + 1, default));
    }
}
