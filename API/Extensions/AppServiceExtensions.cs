using API.Data;
using API.Data.Repositories;
using API.Helpers;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Extensions
{
    public static class AppServiceExtensions
    {
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
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddSingleton<IConfiguration>(configuration);
            services.AddScoped<IPostLikeRepository,PostLikeRepository>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IEmailSender, EmailSender>();
            services.AddScoped<IPostRepository, PostRepository>();
             services.AddScoped<INotificationRepository, NotificationRepository>();
            services.AddScoped<ICommentRepository,CommentRepository>();
            services.AddScoped<IFollowRepository,FollowRepository>();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            return services;
        }
    }
}