using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API.Middlewares
{
    /// <summary>
    /// Middleware class for seeding initial users into the database.
    /// </summary>
    public static class SeedingMiddleware
    {
        /// <summary>
        /// Configures seeding of initial users into the database.
        /// </summary>
        /// <param name="app">The application builder.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async static Task UseSeedingUsers(this WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<ApplicationDbContext>();
                context.Database.Migrate();
                var userMgr = services.GetRequiredService<UserManager<User>>();
                await Seed.SeedUsers(userMgr);
            }
        }
    }

}