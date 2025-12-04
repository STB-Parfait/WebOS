using DotNetEnv;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Backend.Services;
using Backend.Database;
using Backend.Routes;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", 
        policy =>
        {
            policy.WithOrigins("http://localhost:5173")
              .AllowAnyMethod()
              .AllowAnyHeader()
              .AllowCredentials();
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

var keyString = Environment.GetEnvironmentVariable("JWT_SECRET");
if (string.IsNullOrEmpty(keyString))
{
    throw new Exception("Missing enviroment variable");
}
var key = Encoding.ASCII.GetBytes(keyString);

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false; // Em prod, mude para true
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false, 
        ValidateAudience = false 
    };
    x.Events = new JwtBearerEvents
    {
        OnMessageReceived = context =>
        {
            var tokenNoCookie = context.Request.Cookies["accessToken"];
            
            if (!string.IsNullOrEmpty(tokenNoCookie))
            {
                context.Token = tokenNoCookie;
            }
            
            return Task.CompletedTask;
        }
    };
});

builder.Services.AddScoped<TokenService>();

var app = builder.Build();

app.UseAuthentication(); // "Quem é você?" (Lê o token)
app.UseAuthorization();  // "O que você pode fazer?"

app.UseCors("AllowAll");

app.MapGet("/", () => "Hello World!");

app.MapUserEndpoints();
app.Run();
