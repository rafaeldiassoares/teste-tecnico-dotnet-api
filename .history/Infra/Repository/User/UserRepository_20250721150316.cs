using dotnet_api.Models;

namespace dotnet_api.Infra.Repository.User;

public class UserRepository: IUserRepository
{
    private readonly ApplicationDbContext _context;
    
    public UserRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(UserModel customers)
    {
        await _context.Users.AddAsync(customers);
    }

    public async Task UpdateAsync(UserModel customers)
    {
        await _context.Users.AddAsync(customers);
    }

    public async Task<UserModel?> GetById(int id)
    {
        return await _context.Users.FindAsync(id);
    }

    public async Task<UserModel?> GetByEmail(string email)
    {
        return await _context.Users.FindAsync(email);
    }

    public Task CommitAsync()
    {
        throw new NotImplementedException();
    }
}