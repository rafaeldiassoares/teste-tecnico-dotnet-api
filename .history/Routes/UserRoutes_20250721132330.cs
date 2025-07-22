namespace dotnet_api.Routes;

public static class UserRoutes
{
    public static void MapUserRoutes(this WebApplication app)
    {
        app.MapGet("/users", () => new UserModel());
            
    }
}