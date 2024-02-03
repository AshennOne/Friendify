using API.Data;
using API.Data.Repositories;
using API.Dtos;
using API.Entities;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace API.tests.Repositories;

public class CommentRepositoryTests
{
    private readonly IMapper _mapper;
    public CommentRepositoryTests()
    {
        _mapper = A.Fake<IMapper>();
    }
    private async Task<ApplicationDbContext> GetDatabaseContext()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
        var databaseContext = new ApplicationDbContext(options);
        databaseContext.Database.EnsureCreated();
        if (await databaseContext.Comments.CountAsync() <= 0)
        {
            for (int i = 1; i < 10; i++)
            {
                databaseContext.Comments.Add(new Comment()
                {
                    Id = i,
                    CommentedById = "dmfingn",
                    PostId = i,
                    Content = "Hello" + i.ToString()
                });

            }
            await databaseContext.SaveChangesAsync();
        }
        return databaseContext;
    }
    [Fact]
    public async Task CommentRepository_BelongsToUser_ReturnsTrue()
    {
        //arrange
        var dbContext = await GetDatabaseContext();
        var repository = new CommentRepository(dbContext, _mapper);
        var userId = "dmfingn";
        var commentId = 1;
        //act
        var result = await repository.BelongsToUser(userId, commentId);
        //assert
        result.Should().BeTrue();
    }
    [Theory]
    [InlineData("dmfingw", 1)]
    [InlineData("dmfingn", 12)]
    public async Task CommentRepository_BelongsToUser_ReturnsFalse(string userId, int commentId)
    {
        //arrange
        var dbContext = await GetDatabaseContext();
        var repository = new CommentRepository(dbContext, _mapper);

        //act
        var result = await repository.BelongsToUser(userId, commentId);
        //assert
        result.Should().BeFalse();
    }
    [Theory]
    [InlineData(1)]
    public async Task CommentRepository_GetCommentById_ReturnComment(int commentId)
    {
        //arrange
        var dbContext = await GetDatabaseContext();
        var repository = new CommentRepository(dbContext, _mapper);
        //act
        var result = await repository.GetCommentById(commentId);
        //assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(Comment));

    }
    [Theory]
    [InlineData(1)]
    public async Task CommentRepository_GetCommentsForPost_ReturnComments(int postId)
    {
        //arrange
        var dbContext = await GetDatabaseContext();
        var comments = dbContext.Comments.Include(c => c.CommentedBy).Include(c => c.Post).ThenInclude(p => p.Author).Where(c => c.PostId == postId).OrderByDescending(c => c.Created);
        var commentsDto = _mapper.Map<IEnumerable<CommentResponseDto>>(comments);
        A.CallTo(() => _mapper.Map<IEnumerable<CommentResponseDto>>(dbContext.Comments)).Returns(commentsDto);
        var repository = new CommentRepository(dbContext, _mapper);
        //act
        var result = repository.GetCommentsForPost(postId);
        //assert
        result.Should().NotBeNull();
        result.Should().BeAssignableTo(typeof(IEnumerable<CommentResponseDto>));

    }
}