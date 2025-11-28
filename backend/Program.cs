using DotNetEnv;
using Microsoft.EntityFrameworkCore;
using Backend.Services;
using Backend.Database;
using Backend.Routes;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", 
        policy =>
        {
            policy.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
        }
    );
});

Env.Load();
var connectionString = Environment.GetEnvironmentVariable("MONGO_URI");

if(string.IsNullOrEmpty(connectionString))
{
    throw new Exception("MONGO_URI not declared on .env");
}

var databaseName = "WebOS";

builder.Services.AddDbContext<LocalDbContext>(options =>
{
    options.UseMongoDB(connectionString, databaseName);
});

builder.Services.AddScoped<UserService>();
builder.Services.AddAuthorization();

var app = builder.Build();

app.UseCors("AllowAll");

app.MapGet("/", () => "Hello World!");

app.MapUserEndpoints();

app.Run();
