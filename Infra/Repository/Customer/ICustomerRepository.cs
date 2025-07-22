using dotnet_api.Models;

namespace dotnet_api.Infra.Repository.Customer;

public interface ICustomerRepository
{
    Task AddAsync(CustomerModel customer);
    
    Task UpdateAsync(CustomerModel customer);

    Task<CustomerModel?> GetById(int id);
    
    Task<CustomerModel?> GetByCpfEmail(string cpf, string email);

    Task<IEnumerable<CustomerModel>> GetAll();
    
    Task DeleteAsync(int id);

    Task CommitAsync();
}