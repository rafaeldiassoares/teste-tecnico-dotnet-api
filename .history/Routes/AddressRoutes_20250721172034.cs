using dotnet_api.Infra.Repository.Addreess;
using dotnet_api.Models;

namespace dotnet_api.Routes;

public static class AddressRoutes
{
    public static void MapAddressRoutes(this WebApplication app)
    {
        app.MapGet("/addresses", async (IAddressRepository repository) =>
        {
            // TODO: Implement list all addresses
            return Results.Ok("Lista de endereços");
        });
        
        app.MapGet("/addresses/{id}", async (int id, IAddressRepository repository) =>
        {
            var address = await repository.GetById(id);
            if (address == null)
                return Results.NotFound();
            
            return Results.Ok(address);
        });
        
        app.MapGet("/customers/{customerId}/addresses", async (int customerId, IAddressRepository repository) =>
        {
            // TODO: Implement list addresses by customer
            return Results.Ok($"Endereços do cliente {customerId}");
        });
        
        app.MapPost("/addresses", async (AddressesModel address, IAddressRepository repository) =>
        {
            address.CreatedAt = DateTime.UtcNow;
            address.UpdatedAt = DateTime.UtcNow;
            
            await repository.AddAsync(address);
            await repository.CommitAsync();
            
            return Results.Created($"/addresses/{address.Id}", address);
        });
        
        app.MapPut("/addresses/{id}", async (int id, AddressesModel address, IAddressRepository repository) =>
        {
            var existingAddress = await repository.GetById(id);
            if (existingAddress == null)
                return Results.NotFound();
            
            existingAddress.CustomerId = address.CustomerId;
            existingAddress.Address = address.Address;
            existingAddress.Number = address.Number;
            existingAddress.Complement = address.Complement;
            existingAddress.ZipCode = address.ZipCode;
            existingAddress.UpdatedAt = DateTime.UtcNow;
            
            await repository.UpdateAsync(existingAddress);
            await repository.CommitAsync();
            
            return Results.Ok(existingAddress);
        });
        
        app.MapDelete("/addresses/{id}", async (int id, IAddressRepository repository) =>
        {
            var address = await repository.GetById(id);
            if (address == null)
                return Results.NotFound();
            
            // TODO: Implement delete
            return Results.Ok("Endereço deletado");
        });
    }
} 