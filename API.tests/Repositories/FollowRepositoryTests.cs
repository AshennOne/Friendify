using API.Data;
using API.Data.Repositories;
using API.Dtos;
using API.Entities;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
namespace API.tests.Repositories;

public class FollowRepositoryTests
{
    private readonly IMapper _mapper;
    public FollowRepositoryTests()
    {
        _mapper = A.Fake<IMapper>();
    }
    private async Task<ApplicationDbContext> GetDatabaseContext()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
        var databaseContext = new ApplicationDbContext(options);
        databaseContext.Database.EnsureCreated();
        if (await databaseContext.Follows.CountAsync() <= 0)
        {
            for (int i = 1; i < 10; i++)
            {
                databaseContext.Follows.Add(new Follow()
                {
                    Id = i,
                    FollowerId = "abcdefg",
                    FollowedId = "hijklmn" + i.ToString()
                });

            }
            await databaseContext.SaveChangesAsync();
        }
        return databaseContext;
    }
    [Theory]
    [InlineData("abcdefg")]
    public async Task FollowRepository_GetFollowedByUser_ReturnFollowed(string userId)
    {
        //arrange
        var dbContext = await GetDatabaseContext();
        var repository = new FollowRepository(dbContext, _mapper);
        //act
        var result = repository.GetFollowedByUser(userId);
        //assert
        result.Should().NotBeNull();
        result.Should().BeAssignableTo(typeof(IEnumerable<FollowDto>));
    }
    [Theory]
    [InlineData("hijklmn")]
    public async Task FollowRepository_GetFollowersForUser_ReturnFollowers(string userId)
    {
        //arrange
        var dbContext = await GetDatabaseContext();
        var repository = new FollowRepository(dbContext, _mapper);
        //act
        var result = repository.GetFollowersForUser(userId);
        //assert
        result.Should().NotBeNull();
        result.Should().BeAssignableTo(typeof(IEnumerable<FollowDto>));
    }
    [Theory]
    [InlineData("abcdefg","hijklmn")]
    public async Task FollowRepository_GetFollow_ReturnFollow(string followerId, string followedId)
    {
        //arrange
        var dbContext = await GetDatabaseContext();
        var repository = new FollowRepository(dbContext, _mapper);
        //act
        var result =await repository.GetFollow(followerId,followedId);
        //assert
        result.Should().NotBeNull();
        result.Should().BeAssignableTo(typeof(FollowDto));
    }
}