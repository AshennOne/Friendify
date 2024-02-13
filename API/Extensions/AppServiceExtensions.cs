using System.Reflection;
using API.Data;
using API.Data.Repositories;
using API.Helpers;
using API.Interfaces;
using API.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;

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
      var host = configuration["POSTGRESQL_ADDON_HOST"];
      var port = configuration["POSTGRESQL_ADDON_PORT"];
      var userId = configuration["POSTGRESQL_ADDON_USER"];
      var password = configuration["POSTGRESQL_ADDON_PASSWORD"];
      var database = configuration["POSTGRESQL_ADDON_DB"];
      var DefaultConnection = $"Server={host}; Port={port}; User Id={userId}; Password={password}; Database={database}";
      services.AddDbContext<ApplicationDbContext>(o =>
      {
        o.UseNpgsql(DefaultConnection);
      });
      services.AddScoped<IUnitOfWork, UnitOfWork>();
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
      services.AddSwaggerGen(c =>
{
c.SwaggerDoc("v1", new OpenApiInfo
{
Title = "Friendify API",
Version = "v1"
});
c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
{
In = ParameterLocation.Header,
Description = "Please insert JWT with Bearer into field",
Name = "Authorization",
Type = SecuritySchemeType.Http,
BearerFormat = "JWT",
Scheme = "bearer"
});
c.AddSecurityRequirement(new OpenApiSecurityRequirement {
   {
     new OpenApiSecurityScheme
     {
       Reference = new OpenApiReference
       {
         Type = ReferenceType.SecurityScheme,
         Id = "Bearer"
       }
      },
      new string[] { }
    }
});
var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

      services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
      services.AddSingleton<IConfiguration>(configuration);
      services.AddScoped<IPostLikeRepository, PostLikeRepository>();
      services.AddScoped<ITokenService, TokenService>();
      services.AddScoped<IEmailSender, EmailSender>();
      services.AddScoped<IPostRepository, PostRepository>();
      services.AddScoped<INotificationRepository, NotificationRepository>();
      services.AddScoped<ICommentRepository, CommentRepository>();
      services.AddScoped<IFollowRepository, FollowRepository>();
      services.AddScoped<IMessageRepository, MessageRepository>();
      services.AddEndpointsApiExplorer();
      services.AddSwaggerGen();
      return services;
    }
  }
}