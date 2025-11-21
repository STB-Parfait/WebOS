using DotNetEnv;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

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

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();
