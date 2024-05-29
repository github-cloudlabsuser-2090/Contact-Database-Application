using Xunit;
using Moq;
using CRUD_application_2.Controllers;
using CRUD_application_2.Models;
using System.Web.Mvc;
using System.Collections.Generic;
using System.Linq;

public class UserControllerTests
{
    private readonly UserController _controller;
    private readonly List<User> _userList;

    public UserControllerTests()
    {
        _controller = new UserController();
        _userList = UserController.userlist;
    }

    [Fact]
    public void Index_ReturnsViewResult_WithUserList()
    {
        // Act
        var result = _controller.Index("") as ViewResult;

        // Assert
        Assert.NotNull(result);
        var model = result.Model as List<User>;
        Assert.Equal(_userList, model);
    }

    [Fact]
    public void Details_ReturnsUser_WhenUserExists()
    {
        // Arrange
        var user = new User { Id = 1, Name = "Test User" };
        _userList.Add(user);

        // Act
        var result = _controller.Details(1) as ViewResult;

        // Assert
        Assert.NotNull(result);
        var model = result.Model as User;
        Assert.Equal(user, model);
    }

    [Fact]
    public void Create_AddsUser_WhenModelStateIsValid()
    {
        // Arrange
        var user = new User { Id = 2, Name = "New User" };

        // Act
        var result = _controller.Create(user) as RedirectToRouteResult;

        // Assert
        Assert.NotNull(result);
        Assert.Contains(user, _userList);
    }

    // Add more tests here for Edit and Delete methods
    [Fact]
    public void Edit_ReturnsUser_WhenUserExists()
    {
        // Arrange
        var user = new User { Id = 3, Name = "Test User" };
        _userList.Add(user);

        // Act
        var result = _controller.Edit(3) as ViewResult;

        // Assert
        Assert.NotNull(result);
        var model = result.Model as User;
        Assert.Equal(user, model);
    }

    
    [Fact]
    public void Edit_UpdatesUser_WhenModelStateIsValid()
    {
        // Arrange
        var user = new User { Id = 5, Name = "Test User" };
        _userList.Add(user);
        var updatedUser = new User { Id = 5, Name = "Updated User" };

        // Act
        var result = _controller.Edit(5, updatedUser) as RedirectToRouteResult;

        // Assert
        Assert.NotNull(result);
        Assert.Contains(_userList, u => u.Id == updatedUser.Id && u.Name == updatedUser.Name && u.Email == updatedUser.Email);
    }

    [Fact]
    public void Edit_ReturnsHttpNotFound_WhenUserDoesNotExist()
    {
        // Arrange
        var updatedUser = new User { Id = 6, Name = "Updated User" };

        // Act
        var result = _controller.Edit(6, updatedUser) as HttpNotFoundResult;

        // Assert
        Assert.NotNull(result);
    }

}
