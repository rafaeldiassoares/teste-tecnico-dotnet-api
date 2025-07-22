using dotnet_api.Infra.Repository.Customer;
using dotnet_api.Models;

namespace dotnet_api.Routes;

public static class CustomerRoutes
{
    public static void MapCustomerRoutes(this WebApplication app)
    {
        app.MapGet("/customers", async (ICustomerRepository repository) =>
        {
            // TODO: Implement list all customers
            return Results.Ok("Lista de clientes");
        });
        
        app.MapGet("/customers/{id}", async (int id, ICustomerRepository repository) =>
        {
            var customer = await repository.GetById(id);
            if (customer == null)
                return Results.NotFound();
            
            return Results.Ok(customer);
        });
        
        app.MapPost("/customers", async (CustomerModel customer, ICustomerRepository repository) =>
        {
            customer.CreatedAt = DateTime.UtcNow;
            customer.UpdatedAt = DateTime.UtcNow;
            
            await repository.AddAsync(customer);
            await repository.CommitAsync();
            
            return Results.Created($"/customers/{customer.Id}", customer);
        });
        
        app.MapPut("/customers/{id}", async (int id, CustomerModel customer, ICustomerRepository repository) =>
        {
            var existingCustomer = await repository.GetById(id);
            if (existingCustomer == null)
                return Results.NotFound();
            
            existingCustomer.Name = customer.Name;
            existingCustomer.Email = customer.Email;
            existingCustomer.Cpf = customer.Cpf;
            existingCustomer.UpdatedAt = DateTime.UtcNow;
            
            await repository.UpdateAsync(existingCustomer);
            await repository.CommitAsync();
            
            return Results.Ok(existingCustomer);
        });
        
        app.MapDelete("/customers/{id}", async (int id, ICustomerRepository repository) =>
        {
            var customer = await repository.GetById(id);
            if (customer == null)
                return Results.NotFound();
            
            // TODO: Implement delete
            return Results.Ok("Cliente deletado");
        });
    }
} 