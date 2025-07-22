using dotnet_api.Controllers;
using dotnet_api.Models;

namespace dotnet_api.Routes;

public static class CustomerAddressesRoutes
{
    public static void MapCustomerAddressesRoutes(this WebApplication app)
    {
        // Todas as rotas de endereços de clientes requerem autenticação
        app.MapPost("/customers/{customerId}/addresses", async (int customerId, AddressesModel address, CustomerAddressesController customerAddressesController) =>
        {
            return await customerAddressesController.CreateAddress(customerId, address);
        });

        app.MapPatch("/customers/{customerId}/addresses/{id}", async (int customerId, int id, AddressesModel address, CustomerAddressesController customerAddressesController) =>
        {
            return await customerAddressesController.UpdateAddress(customerId, id, address);
        });

        app.MapDelete("/customers/{customerId}/addresses/{id}", async (int customerId, int id, CustomerAddressesController customerAddressesController) =>
        {
            return await customerAddressesController.DeleteAddress(customerId, id);
        });

        app.MapGet("/customers/{customerId}/addresses/{id}", async (int customerId, int id, CustomerAddressesController customerAddressesController) =>
        {
            return await customerAddressesController.GetAddressById(customerId, id);
        });

        app.MapGet("/customers/{customerId}/addresses", async (int customerId, CustomerAddressesController customerAddressesController) =>
        {
            return await customerAddressesController.GetAddresses(customerId);
        });
    }
} 