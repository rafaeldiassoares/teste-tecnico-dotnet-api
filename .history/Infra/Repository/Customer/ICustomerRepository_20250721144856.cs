using dotnet_api.Models;

namespace dotnet_api.Infra.Repository.Customer;

public interface ICustomerRepository
{
    Task AddAsync(CustomerModel customers);
    
    Task UpdateAsync(CustomerModel customers);

    Task<CustomerModel?> GetById(int id);
    
    Task<CustomerModel?> GetByCpfEmail(string cpf, string email);

    Task CommitAsync();
}