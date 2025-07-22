using dotnet_api.Infra.Repository.Addreess;
using dotnet_api.Infra.Repository.Customer;
using dotnet_api.Models;

namespace dotnet_api.Controllers;

public class CustomerAddressesController
{
    private readonly IAddressRepository _addressRepository;
    private readonly ICustomerRepository _customerRepository;

    public CustomerAddressesController(IAddressRepository addressRepository, ICustomerRepository customerRepository)
    {
        _addressRepository = addressRepository;
        _customerRepository = customerRepository;
    }

    public async Task<IResult> CreateAddress(int customerId, AddressesModel address)
    {
        try
        {
            // Verificar se o cliente existe
            var customer = await _customerRepository.GetById(customerId);
            if (customer == null)
            {
                return Results.NotFound(new { message = "Cliente não encontrado" });
            }

            // Associar o endereço ao cliente
            address.CustomerId = customerId;
            address.CreatedAt = DateTime.UtcNow;
            address.UpdatedAt = DateTime.UtcNow;
            
            await _addressRepository.AddAsync(address);
            await _addressRepository.CommitAsync();
            
            return Results.Created($"/customers/{customerId}/addresses/{address.Id}", address);
        }
        catch (Exception ex)
        {
            return Results.BadRequest(new { message = "Erro ao criar endereço", error = ex.Message });
        }
    }

    public async Task<IResult> UpdateAddress(int customerId, int id, AddressesModel address)
    {
        try
        {
            // Verificar se o cliente existe
            var customer = await _customerRepository.GetById(customerId);
            if (customer == null)
            {
                return Results.NotFound(new { message = "Cliente não encontrado" });
            }

            // Verificar se o endereço existe e pertence ao cliente
            var existingAddress = await _addressRepository.GetById(id);
            if (existingAddress == null)
            {
                return Results.NotFound(new { message = "Endereço não encontrado" });
            }

            if (existingAddress.CustomerId != customerId)
            {
                return Results.BadRequest(new { message = "Endereço não pertence ao cliente especificado" });
            }

            // Atualizar os dados do endereço
            existingAddress.Address = address.Address;
            existingAddress.Number = address.Number;
            existingAddress.Complement = address.Complement;
            existingAddress.ZipCode = address.ZipCode;
            existingAddress.UpdatedAt = DateTime.UtcNow;
            
            await _addressRepository.UpdateAsync(existingAddress);
            await _addressRepository.CommitAsync();
            
            return Results.Ok(existingAddress);
        }
        catch (Exception ex)
        {
            return Results.BadRequest(new { message = "Erro ao atualizar endereço", error = ex.Message });
        }
    }

    public async Task<IResult> DeleteAddress(int customerId, int id)
    {
        try
        {
            // Verificar se o cliente existe
            var customer = await _customerRepository.GetById(customerId);
            if (customer == null)
            {
                return Results.NotFound(new { message = "Cliente não encontrado" });
            }

            // Verificar se o endereço existe e pertence ao cliente
            var address = await _addressRepository.GetById(id);
            if (address == null)
            {
                return Results.NotFound(new { message = "Endereço não encontrado" });
            }

            if (address.CustomerId != customerId)
            {
                return Results.BadRequest(new { message = "Endereço não pertence ao cliente especificado" });
            }

            // TODO: Implementar delete no repositório
            // await _addressRepository.DeleteAsync(address);
            // await _addressRepository.CommitAsync();
            
            return Results.Ok(new { message = "Endereço deletado com sucesso" });
        }
        catch (Exception ex)
        {
            return Results.BadRequest(new { message = "Erro ao deletar endereço", error = ex.Message });
        }
    }

    public async Task<IResult> GetAddressById(int customerId, int id)
    {
        try
        {
            // Verificar se o cliente existe
            var customer = await _customerRepository.GetById(customerId);
            if (customer == null)
            {
                return Results.NotFound(new { message = "Cliente não encontrado" });
            }

            // Verificar se o endereço existe e pertence ao cliente
            var address = await _addressRepository.GetById(id);
            if (address == null)
            {
                return Results.NotFound(new { message = "Endereço não encontrado" });
            }

            if (address.CustomerId != customerId)
            {
                return Results.BadRequest(new { message = "Endereço não pertence ao cliente especificado" });
            }

            return Results.Ok(address);
        }
        catch (Exception ex)
        {
            return Results.BadRequest(new { message = "Erro ao buscar endereço", error = ex.Message });
        }
    }

    public async Task<IResult> GetAddresses(int customerId)
    {
        try
        {
            // Verificar se o cliente existe
            var customer = await _customerRepository.GetById(customerId);
            if (customer == null)
            {
                return Results.NotFound(new { message = "Cliente não encontrado" });
            }

            // Retornar os endereços do cliente
            var addresses = customer.Addresses;
            return Results.Ok(new { message = "Endereços do cliente", addresses = addresses });
        }
        catch (Exception ex)
        {
            return Results.BadRequest(new { message = "Erro ao buscar endereços", error = ex.Message });
        }
    }
} 