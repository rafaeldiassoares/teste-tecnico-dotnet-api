using dotnet_api.Infra.Repository.User;
using dotnet_api.Models;

namespace dotnet_api.Routes;

public static class UserRoutes
{
    public static void MapUserRoutes(this WebApplication app)
    {
        app.MapGet("/users", async (IUserRepository repository) =>
        {
            // TODO: Implement list all users
            return Results.Ok("Lista de usuários");
        });
        
        app.MapGet("/users/{id}", async (int id, IUserRepository repository) =>
        {
            var user = await repository.GetById(id);
            if (user == null)
                return Results.NotFound();
            
            return Results.Ok(user);
        });
        
        app.MapPost("/users", async (UserModel user, IUserRepository repository) =>
        {
            user.CreatedAt = DateTime.UtcNow;
            user.UpdatedAt = DateTime.UtcNow;
            
            await repository.AddAsync(user);
            await repository.CommitAsync();
            
            return Results.Created($"/users/{user.Id}", user);
        });
        
        app.MapPut("/users/{id}", async (int id, UserModel user, IUserRepository repository) =>
        {
            var existingUser = await repository.GetById(id);
            if (existingUser == null)
                return Results.NotFound();
            
            existingUser.Name = user.Name;
            existingUser.Email = user.Email;
            existingUser.Password = user.Password;
            existingUser.UpdatedAt = DateTime.UtcNow;
            
            await repository.UpdateAsync(existingUser);
            await repository.CommitAsync();
            
            return Results.Ok(existingUser);
        });
        
        app.MapDelete("/users/{id}", async (int id, IUserRepository repository) =>
        {
            var user = await repository.GetById(id);
            if (user == null)
                return Results.NotFound();
            
            // TODO: Implement delete
            return Results.Ok("Usuário deletado");
        });
    }
}