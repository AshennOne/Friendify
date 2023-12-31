using API.SignalR;

namespace API.Middlewares
{
    public static class BaseMiddlewares
    {
        public static WebApplication LoadBaseMiddlewares(this WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.MapHub<PresenceHub>("hubs/presence");
            
            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();
            return app;
        }
    }
}