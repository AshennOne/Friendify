using API.Extensions;
using API.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.LoadServices(builder.Configuration);
builder.Services.LoadAuth(builder.Configuration);
var app = builder.Build();
app.UseCors(builder => builder.AllowAnyHeader().AllowAnyMethod().AllowCredentials().WithOrigins("https://localhost:4200"));
await app.UseSeedingUsers();
app.LoadBaseMiddlewares();
app.Run();
