using dotnet_api.Controllers;
using dotnet_api.Models;

namespace dotnet_api.Routes;

public static class CustomerRoutes
{
    public static void MapCustomerRoutes(this WebApplication app)
    {
        // Rota que não precisa de autenticação
        app.MapGet("/customers", async (CustomerController customerController) =>
        {
            return await customerController.GetCustomers();
        });

        // Rotas que precisam de autenticação
        app.MapPost("/customers", async (CustomerModel customer, CustomerController customerController) =>
        {
            return await customerController.CreateCustomer(customer);
        });

        app.MapPatch("/customers/{id}", async (int id, CustomerModel customer, CustomerController customerController) =>
        {
            return await customerController.UpdateCustomer(id, customer);
        });

        app.MapDelete("/customers/{id}", async (int id, CustomerController customerController) =>
        {
            return await customerController.DeleteCustomer(id);
        });

        app.MapGet("/customers/{id}", async (int id, CustomerController customerController) =>
        {
            return await customerController.GetCustomerById(id);
        });
    }
} 