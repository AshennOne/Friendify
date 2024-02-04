using API.Data;
using API.Data.Repositories;
using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.tests.Repositories;

public class PostLikeRepositoryTests
{
    private async Task<ApplicationDbContext> GetDatabaseContext()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
        var databaseContext = new ApplicationDbContext(options);
        databaseContext.Database.EnsureCreated();
        if (await databaseContext.Likes.CountAsync() <= 0)
        {
            for (int i = 1; i < 10; i++)
            {
                databaseContext.Likes.Add(new PostLike()
                {
                    Id = i,
                    LikedById = "abcdefg",
                    PostId = 1
                });

            }
            await databaseContext.SaveChangesAsync();
        }
        return databaseContext;
    }
    [Fact]
    public async Task PostLikeRepository_AddLike_ReturnLike()
    {
        //arrange
        var userId = "abcdefg";
        var postId = 2;
        var dbContext = await GetDatabaseContext();
        var repository = new PostLikeRepository(dbContext);
        //act
        var result = await repository.AddLike(userId, postId);
        //assert
        result.Should().NotBeNull();
        result.Should().BeAssignableTo(typeof(PostLike));
    }
    [Fact]
    public async Task PostLikeRepository_GetLikedPosts_ReturnsLikedPosts()
    {
        //arrange
        var userId = "abcdefg";
        var dbContext = await GetDatabaseContext();
        var repository = new PostLikeRepository(dbContext);
        //act
        var result = await repository.GetLikedPosts(userId);
        //assert
        result.Should().NotBeNull();
        result.Should().BeAssignableTo(typeof(IEnumerable<Post>));
    }
    [Fact]
    public async Task PostLikeRepository_RemoveLike_ReturnLike()
    {
        //arrange
        var userId = "abcdefg";
        var postId = 1;
        var dbContext = await GetDatabaseContext();
        var repository = new PostLikeRepository(dbContext);
        //act
        var result = await repository.RemoveLike(userId, postId);
        //assert
        result.Should().NotBeNull();
        result.Should().BeAssignableTo(typeof(PostLike));
    }
}