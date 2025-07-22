public static class UserRoutes
{
    public static void UserRoutes(WebApplication app)
    {
        app.MapGet("/users", () => "Hello World!");
    }
}