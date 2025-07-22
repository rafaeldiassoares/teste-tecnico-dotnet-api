namespace User.Data;

public class UserContext
{
    // This class can be used to manage user data, such as in-memory storage or database context.
    // For simplicity, we are not implementing any actual data storage here.
    
    public List<UserModel> Users { get; set; } = new List<UserModel>();

    public UserContext()
    {
        // Initialize with some dummy data if needed
        Users.Add(new UserModel("John Doe"));
    }
}