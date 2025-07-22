using dotnet_api.Models;

namespace dotnet_api.Infra.Repository.User;

public interface IUserRepository
{
    Task AddAsync(UserModel user);
    
    Task UpdateAsync(UserModel user);

    Task<UserModel?> GetById(int id);
    
    Task<UserModel?> GetByEmail(string email);

    Task<IEnumerable<UserModel>> GetAll();
    
    Task DeleteAsync(int id);

    Task CommitAsync();
}