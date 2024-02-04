using API.Data;
using API.Data.Repositories;
using API.Dtos;
using API.Entities;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace API.tests.Repositories;
public class PostRepositoryTests
{
    private readonly IMapper _mapper;
    public PostRepositoryTests()
    {
        _mapper = A.Fake<IMapper>();
    }
    private async Task<ApplicationDbContext> GetDatabaseContext()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
        var databaseContext = new ApplicationDbContext(options);
        databaseContext.Database.EnsureCreated();
        if (await databaseContext.Posts.CountAsync() <= 0)
        {
            for (int i = 1; i < 10; i++)
            {
                databaseContext.Posts.Add(new Post()
                {
                    Id = i + 10,
                    AuthorId = "abcdefg",
                    OriginalAuthorId = "abcdefg",
                    TextContent = "Hello",
                    RepostedFromId = 1
                });

            }
            await databaseContext.SaveChangesAsync();
        }
        return databaseContext;
    }
    [Fact]
    public async Task PostRepository_GetRepostedPosts_ReturnPosts()
    {
        //arrange
        var user = A.Fake<User>();
        var dbContext = await GetDatabaseContext();
        var repository = new PostRepository(dbContext, _mapper);
        //act
        var result = await repository.GetRepostedPosts(user);
        //assert
        result.Should().NotBeNull();
        result.Should().BeAssignableTo(typeof(IEnumerable<PostDto>));
    }
    [Fact]
    public async Task PostRepository_SearchPosts_ReturnPosts()
    {
        //arrange
        var searchString = "ello";
        var dbContext = await GetDatabaseContext();
        var repository = new PostRepository(dbContext, _mapper);
        //act
        var result = await repository.SearchPosts(searchString);
        //assert
        result.Should().NotBeNull();
        result.Should().BeAssignableTo(typeof(IEnumerable<PostDto>));
    }
    [Fact]
    public async Task PostRepository_GetAllPosts_ReturnPosts()
    {
        //arrange
        var dbContext = await GetDatabaseContext();
        var repository = new PostRepository(dbContext, _mapper);
        //act
        var result = repository.GetAllPosts();
        //assert
        result.Should().NotBeNull();
        result.Should().BeAssignableTo(typeof(IEnumerable<PostDto>));
    }
    [Fact]
    public async Task PostRepository_UnRepost_ReturnPost()
    {
        //arrange
        var post = A.Fake<Post>();
        var user = A.Fake<User>();
        post.Id = 1;
        user.Id = "abcdefg";
        var dbContext = await GetDatabaseContext();
        var repository = new PostRepository(dbContext, _mapper);
        //act
        var result = await repository.UnRepost(post, user);
        //assert
        result.Should().NotBeNull();
        result.Should().BeAssignableTo(typeof(Post));
    }
    [Fact]
    public async Task PostRepository_GetPostById_ReturnPost()
    {
        //arrange
        var id = 12;
        var dbContext = await GetDatabaseContext();
        var repository = new PostRepository(dbContext, _mapper);
        //act
        var result = await repository.GetPostById(id);
        //assert
        result.Should().NotBeNull();
        result.Should().BeAssignableTo(typeof(Post));
    }
    [Fact]
    public async Task PostRepository_ConvertToDto_ReturnPostDto()
    {
        //arrange
        var post = A.Fake<Post>();
        var dbContext = await GetDatabaseContext();
        var repository = new PostRepository(dbContext, _mapper);
        //act
        var result = repository.ConvertToDto(post);
        //assert
        result.Should().NotBeNull();
        result.Should().BeAssignableTo(typeof(PostDto));
    }
    [Fact]
    public async Task PostRepository_GetPostsForUser_ReturnPosts()
    {
        //arrange
        var dbContext = await GetDatabaseContext();
        var repository = new PostRepository(dbContext, _mapper);
        //act
        var result = repository.GetPostsForUser("username");
        //assert
        result.Should().NotBeNull();
        result.Should().BeAssignableTo(typeof(IEnumerable<PostDto>));
    }

}