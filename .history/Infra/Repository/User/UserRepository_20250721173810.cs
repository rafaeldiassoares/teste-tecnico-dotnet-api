using dotnet_api.Models;
using Microsoft.EntityFrameworkCore;

namespace dotnet_api.Infra.Repository.User;

public class UserRepository: IUserRepository
{
    private readonly ApplicationDbContext _context;
    
    public UserRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(UserModel user)
    {
        await _context.Users.AddAsync(user);
    }

    public async Task UpdateAsync(UserModel user)
    {
        _context.Users.Update(user);
        await Task.CompletedTask;
    }

    public async Task<UserModel?> GetById(int id)
    {
        return await _context.Users.FindAsync(id);
    }

    public async Task<UserModel?> GetByEmail(string email)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<IEnumerable<UserModel>> GetAll()
    {
        return await _context.Users.ToListAsync();
    }

    public async Task CommitAsync()
    {
        await _context.SaveChangesAsync();
    }
}