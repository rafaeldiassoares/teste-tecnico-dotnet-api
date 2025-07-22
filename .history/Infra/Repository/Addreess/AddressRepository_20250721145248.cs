using dotnet_api.Models;

namespace dotnet_api.Infra.Repository.Addreess;

public class AddressRepository: IAddressRepository
{
    public AddressRepository()
    {
    }

    public Task AddAsync(AddressesModel addresses)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(AddressesModel addresses)
    {
        throw new NotImplementedException();
    }

    public Task<AddressesModel?> GetById(int id)
    {
        throw new NotImplementedException();
    }

    public Task CommitAsync()
    {
        throw new NotImplementedException();
    }
}