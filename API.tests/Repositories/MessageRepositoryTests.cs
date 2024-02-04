using API.Data;
using API.Data.Repositories;
using API.Dtos;
using API.Entities;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace API.tests.Repositories;

public class MessageRepositoryTests
{
    private readonly IMapper _mapper;
    public MessageRepositoryTests()
    {
        _mapper = A.Fake<IMapper>();
    }
    private async Task<ApplicationDbContext> GetDatabaseContext()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
        var databaseContext = new ApplicationDbContext(options);
        databaseContext.Database.EnsureCreated();
        if (await databaseContext.Messages.CountAsync() <= 0)
        {
            for (int i = 1; i < 10; i++)
            {
                databaseContext.Messages.Add(new Message()
                {
                    Id = i,
                    SenderId = "abcdefg",
                    ReceiverId = "hijklmn",
                    Content = "Hello there"
                });

            }
            await databaseContext.SaveChangesAsync();
        }
        return databaseContext;
    }
    [Fact]
    public async Task MessageRepository_GetLastMessages_ReturnMessages()
    {
        //arrange
        var user = A.Fake<User>();
        var dbContext = await GetDatabaseContext();
        var repository = new MessageRepository(dbContext, _mapper);
        //act
        var result = await repository.GetLastMessages(user);
        //assert
        result.Should().NotBeNull();
        result.Should().BeAssignableTo(typeof(IEnumerable<MessageDto>));
    }
    [Fact]
    public async Task MessageRepository_GetMessageThread_ReturnMessages()
    {
        //arrange
        var currentUserId = "abcdefg";
        var viewedUserId = "hijklmn";
        var dbContext = await GetDatabaseContext();
        var repository = new MessageRepository(dbContext, _mapper);
        //act
        var result = await repository.GetMessageThread(currentUserId, viewedUserId);
        //assert
        result.Should().NotBeNull();
        result.Should().BeAssignableTo(typeof(IEnumerable<MessageDto>));
    }
    [Fact]
    public async Task MessageRepository_SendMessage_ReturnMessage()
    {
        //arrange
        var senderId = "abcdefg";
        var receiverId = "hijklmn";
        var content = "Heyyy";
        var dbContext = await GetDatabaseContext();
        var repository = new MessageRepository(dbContext, _mapper);
        //act
        var result = await repository.SendMessage(senderId, receiverId, content);
        //assert
        result.Should().NotBeNull();
        result.Should().BeAssignableTo(typeof(MessageDto));
    }
}