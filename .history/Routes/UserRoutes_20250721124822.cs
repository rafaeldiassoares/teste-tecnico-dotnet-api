namespace User.Routes;

public static class UserRoutes
{
    public static void MapUserRoutes(this WebApplication app)
    {
        app.MapGet("/users", () => new UserModel("John Doe"))
            .WithName("GetUsers")
            .WithSummary("Retrieves a list of users")
            .Produces<UserModel>(StatusCodes.Status200OK);
    }
}