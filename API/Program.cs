using API.Extensions;
using API.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.LoadServices(builder.Configuration);
builder.Services.LoadAuth(builder.Configuration);
var app = builder.Build();
await app.UseSeedingUsers();
app.LoadBaseMiddlewares();
app.Run();
