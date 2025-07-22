using dotnet_api.Controllers;
using dotnet_api.Models;

namespace dotnet_api.Routes;

public static class UserRoutes
{
    public static void MapUserRoutes(this WebApplication app)
    {
        app.MapPost("/users/login", async (LoginRequest loginRequest, UserController userController) =>
        {
            return await userController.Login(loginRequest);
        });

        app.MapPost("/users", async (UserModel user, UserController userController) =>
        {
            return await userController.CreateUser(user);
        });

        app.MapPatch("/users/{id}", async (int id, UserModel user, UserController userController) =>
        {
            return await userController.UpdateUser(id, user);
        });

        app.MapDelete("/users/{id}", async (int id, UserController userController) =>
        {
            return await userController.DeleteUser(id);
        });

        app.MapGet("/users/{id}", async (int id, UserController userController) =>
        {
            return await userController.GetUserById(id);
        });

        app.MapGet("/users", async (UserController userController) =>
        {
            return await userController.GetUsers();
        });
    }
}