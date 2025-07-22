using dotnet_api.Models;

namespace dotnet_api.Infra.Repository.User;

public interface IUserRepository
{
    Task AddAsync(UserModel customers);
    
    Task UpdateAsync(UserModel customers);

    Task<UserModel?> GetById(int id);
    
    Task<UserModel?> GetByEmail(string email);

    Task CommitAsync();
}