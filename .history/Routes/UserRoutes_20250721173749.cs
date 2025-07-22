using dotnet_api.Controllers;
using dotnet_api.Infra.Middleware;
using dotnet_api.Models;

namespace dotnet_api.Routes;

public static class UserRoutes
{
    public static void MapUserRoutes(this WebApplication app)
    {
        // Rotas que não precisam de autenticação
        app.MapPost("/users/login", async (LoginRequest loginRequest, UserController userController) =>
        {
            return await userController.Login(loginRequest);
        });

        // Rotas que precisam de autenticação
        app.MapPost("/users", async (UserModel user, UserController userController) =>
        {
            return await userController.CreateUser(user);
        }).UseAuthMiddleware();

        app.MapPatch("/users/{id}", async (int id, UserModel user, UserController userController) =>
        {
            return await userController.UpdateUser(id, user);
        }).UseAuthMiddleware();

        app.MapDelete("/users/{id}", async (int id, UserController userController) =>
        {
            return await userController.DeleteUser(id);
        }).UseAuthMiddleware();

        app.MapGet("/users/{id}", async (int id, UserController userController) =>
        {
            return await userController.GetUserById(id);
        }).UseAuthMiddleware();

        app.MapGet("/users", async (UserController userController) =>
        {
            return await userController.GetUsers();
        }).UseAuthMiddleware();
    }
}