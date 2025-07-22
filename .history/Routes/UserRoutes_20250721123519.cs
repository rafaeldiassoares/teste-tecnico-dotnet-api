public static class UserRoutes
{
    public static void UserRoutes(this WebApplication app)
    {
        app.MapGet("/users", () => "Hello World!");
    }
}