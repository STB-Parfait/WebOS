using System.Threading.Tasks;
using Backend.Models;
using Backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Routes;

public static class UserRoutes
{
    public static void MapUserEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/api/users");

        group.MapPost("/", async (UserService service, UserDTO dto) =>
        {
            try
            {
                var user = await service.Create(dto);

                return Results.Created($"/api/users/{user.Id}", user);
            }
            catch (InvalidOperationException ex)
            {
                return Results.Conflict(new { error = ex.Message });
            }
        });

        group.MapGet("/{id}", async (UserService service, string id) =>
        {
            var user = await service.GetById(id);
            return user is not null ? Results.Ok(user) : Results.NotFound();
        });

        group.MapDelete("/{id}", async (UserService service, string id) =>
        {
            var deleted = await service.Delete(id);

            return deleted ? Results.NoContent() : Results.NotFound();
        });

        group.MapPost("/checkemail", async (UserService service, [FromBody] EmailCheckRequest req) =>
        {
            var avaliable = await service.CheckEmail(req.email);

            return Results.Ok(new { isAvaliable = !avaliable });
        });

        group.MapPost("/login", async (HttpContext httpContext, UserService userService, TokenService tokenService, [FromBody] LoginDTO login) =>
        {
            var user = await userService.ValidarLogin(login.Email, login.Password);

            if (user is null) return Results.Unauthorized();

            var token = tokenService.GenerateToken(user);

            var cookieOptions = new CookieOptions
            {
                HttpOnly = true, 
                Secure = true,   
                SameSite = SameSiteMode.Strict, 
                Expires = DateTime.UtcNow.AddHours(8) 
            };

            httpContext.Response.Cookies.Append("accessToken", token, cookieOptions);

            return Results.Ok(new { message = "Successfull login!" });
        });

        group.MapPost("/logout", (HttpContext httpContext) =>
        {
        httpContext.Response.Cookies.Delete("accessToken");
        return Results.Ok(new { message = "Logout" });
        });
    }
}