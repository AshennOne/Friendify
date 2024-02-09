using API.Data;
using API.Data.Repositories;
using API.Helpers;
using API.Interfaces;
using API.SignalR;
using Microsoft.EntityFrameworkCore;

namespace API.Extensions
{
    /// <summary>
    /// Provides extension methods for configuring and loading services into the application.
    /// </summary>
    public static class AppServiceExtensions
    {
        /// <summary>
        /// This method loads various services into the ASP.NET Core application's service container.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns>The modified service collection.</returns>
        public static IServiceCollection LoadServices(this IServiceCollection services, ConfigurationManager configuration)
        {
            services.AddControllers();
            services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<IUnitOfWork,UnitOfWork>();
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
           {
               builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
           });
            });
            services.AddSingleton<PresenceTracker>();
            services.AddSignalR();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddSingleton<IConfiguration>(configuration);
            services.AddScoped<IPostLikeRepository,PostLikeRepository>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IEmailSender, EmailSender>();
            services.AddScoped<IPostRepository, PostRepository>();
             services.AddScoped<INotificationRepository, NotificationRepository>();
            services.AddScoped<ICommentRepository,CommentRepository>();
            services.AddScoped<IFollowRepository,FollowRepository>();
            services.AddScoped<IMessageRepository,MessageRepository>();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            return services;
        }
    }
}