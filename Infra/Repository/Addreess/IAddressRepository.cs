using dotnet_api.Models;

namespace dotnet_api.Infra.Repository.Addreess;

public interface IAddressRepository
{
    Task AddAsync(AddressesModel addresses);
    
    Task UpdateAsync(AddressesModel addresses);

    Task<AddressesModel?> GetById(int id);
    
    Task<AddressesModel?> GetByCustomerIdAndAddressId(int customerId, int addressId);
    
    Task DeleteAsync(int id);

    Task CommitAsync();
}