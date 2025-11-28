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
    }
}