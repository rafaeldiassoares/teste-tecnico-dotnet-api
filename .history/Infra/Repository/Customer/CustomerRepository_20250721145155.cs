using dotnet_api.Models;

namespace dotnet_api.Infra.Repository.Customer;

public class CustomerRepository: ICustomerRepository
{
    public CustomerRepository()
    {
    }

    public Task AddAsync(CustomerModel customers)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(CustomerModel customers)
    {
        throw new NotImplementedException();
    }

    public Task<CustomerModel?> GetById(int id)
    {
        throw new NotImplementedException();
    }

    public Task<CustomerModel?> GetByCpfEmail(string cpf, string email)
    {
        throw new NotImplementedException();
    }

    public Task CommitAsync()
    {
        throw new NotImplementedException();
    }
}