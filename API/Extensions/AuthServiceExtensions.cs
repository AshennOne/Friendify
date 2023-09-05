using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Identity;

namespace API.Extensions
{
    public static class AuthServiceExtensions
    {
        public static IServiceCollection LoadAuth(this IServiceCollection services)
        {
            services.AddIdentity<User, IdentityRole>(options =>
            {
             })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();
            return services;
        }
    }
}