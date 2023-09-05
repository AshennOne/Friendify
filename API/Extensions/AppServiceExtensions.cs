using API.Data;
using Microsoft.EntityFrameworkCore;

namespace API.Extensions
{
    public static class AppServiceExtensions
    {
        public static IServiceCollection LoadServices(this IServiceCollection services,ConfigurationManager configuration)
        {
            services.AddControllers();
            services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            
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
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            return services;
        }
    }
}