using API.Controllers;
using API.Data.Repositories;
using API.Dtos;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.tests.ControllersTests;
public class CommentsControllerTests
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly UserManager<User> _userManager;
    private readonly IMapper _mapper;
    public CommentsControllerTests()
    {
        _unitOfWork = A.Fake<IUnitOfWork>();
        _userManager = A.Fake<UserManager<User>>();
        _mapper = A.Fake<IMapper>();
    }
    [Fact]
    public void CommentsController_GetCommentsForPost_ReturnOk()
    {

        //Arrange
        var postId = 1;
        var expectedComments = A.Fake<IEnumerable<CommentResponseDto>>();

        A.CallTo(() => _unitOfWork.CommentRepository.GetCommentsForPost(postId)).Returns(expectedComments);
        var controller = new CommentsController(_unitOfWork, _userManager);
        //Act
        var result = controller.GetCommentsForPost(postId);
        //Assert
        result.Should().NotBeNull();
       // result.Should().BeAssignableTo<ActionResult<IEnumerable<CommentResponseDto>>>();
        result.Should().BeOfType(typeof(OkObjectResult));
    }
}
