using API.SignalR;

namespace API.Middlewares
{
    /// <summary>
    /// Class containing base middlewares for configuring the application pipeline.
    /// </summary>
    public static class BaseMiddlewares
    {
        /// <summary>
        /// Configures the base middlewares for the application pipeline.
        /// </summary>
        /// <param name="app">The application builder.</param>
        /// <returns>The configured application builder.</returns>
        public static WebApplication LoadBaseMiddlewares(this WebApplication app)
        {
            //if (app.Environment.IsDevelopment())
            //{
            app.UseSwagger();
            app.UseSwaggerUI();
            //}
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