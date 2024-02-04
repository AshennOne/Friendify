using API.Data;
using API.Data.Repositories;
using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.tests.Repositories;

public class NotificationRepositoryTests
{
    private async Task<ApplicationDbContext> GetDatabaseContext()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
        var databaseContext = new ApplicationDbContext(options);
        databaseContext.Database.EnsureCreated();
        if (await databaseContext.Notifications.CountAsync() <= 0)
        {
            for (int i = 1; i < 10; i++)
            {
                databaseContext.Notifications.Add(new Notification()
                {
                    Id = i,
                    FromUserId = "abcdefg",
                    ToUserId = "hijklmn" + i.ToString(),
                    FromUserName = "David",
                    Message = "New notification",
                    Type = NotiType.Follow,
                    ElementId = 1,
                    ImgUrl = "https://something"
                });

            }
            await databaseContext.SaveChangesAsync();
        }
        return databaseContext;
    }
    [Fact]
    public async Task NotificationRepository_AddNotification_ReturnNotification()
    {
        //arrange
        var from = A.Fake<User>();
        var to = A.Fake<User>();
        var type = NotiType.Comment;
        var dbContext = await GetDatabaseContext();
        var repository = new NotificationRepository(dbContext);
        //act
        var result = await repository.AddNotification(from, to, type, 1);
        //assert
        result.Should().NotBeNull();
        result.Should().BeAssignableTo(typeof(Notification));
    }
    [Fact]
    public async Task NotificationRepository_GetNotificationById_ReturnNotification()
    {
        //arange
        var notificationId = 1;
        var dbContext = await GetDatabaseContext();
        var repository = new NotificationRepository(dbContext);
        //act
        var result = await repository.GetNotificationById(notificationId);
        //assert
        result.Should().NotBeNull();
        result.Should().BeAssignableTo(typeof(Notification));
    }
    [Fact]
    public async Task NotificationRepository_GetNotifications_ReturnNotifications()
    {
        //arange
        var user = A.Fake<User>();
        var dbContext = await GetDatabaseContext();
        var repository = new NotificationRepository(dbContext);
        //act
        var result = await repository.GetNotifications(user);
        //assert
        result.Should().NotBeNull();
        result.Should().BeAssignableTo(typeof(IEnumerable<Notification>));
    }
}