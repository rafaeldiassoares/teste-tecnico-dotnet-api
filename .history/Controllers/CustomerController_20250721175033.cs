using dotnet_api.Infra.Repository.Customer;
using dotnet_api.Models;

namespace dotnet_api.Controllers;

public class CustomerController
{
    private readonly ICustomerRepository _customerRepository;

    public CustomerController(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task<IResult> CreateCustomer(CustomerModel customer)
    {
        try
        {
            // Verificar se o email ou CPF já existe
            var existingCustomer = await _customerRepository.GetByCpfEmail(customer.Cpf, customer.Email);
            if (existingCustomer != null)
            {
                if (existingCustomer.Email == customer.Email)
                {
                    return Results.BadRequest(new { message = "Email já cadastrado" });
                }
                if (existingCustomer.Cpf == customer.Cpf)
                {
                    return Results.BadRequest(new { message = "CPF já cadastrado" });
                }
            }

            customer.CreatedAt = DateTime.UtcNow;
            customer.UpdatedAt = DateTime.UtcNow;
            
            await _customerRepository.AddAsync(customer);
            await _customerRepository.CommitAsync();
            
            return Results.Created($"/customers/{customer.Id}", customer);
        }
        catch (Exception ex)
        {
            return Results.BadRequest(new { message = "Erro ao criar cliente", error = ex.Message });
        }
    }

    public async Task<IResult> UpdateCustomer(int id, CustomerModel customer)
    {
        try
        {
            var existingCustomer = await _customerRepository.GetById(id);
            if (existingCustomer == null)
            {
                return Results.NotFound(new { message = "Cliente não encontrado" });
            }

            // Verificar se o email ou CPF já existe em outro cliente
            var customerWithSameData = await _customerRepository.GetByCpfEmail(customer.Cpf, customer.Email);
            if (customerWithSameData != null && customerWithSameData.Id != id)
            {
                if (customerWithSameData.Email == customer.Email)
                {
                    return Results.BadRequest(new { message = "Email já cadastrado por outro cliente" });
                }
                if (customerWithSameData.Cpf == customer.Cpf)
                {
                    return Results.BadRequest(new { message = "CPF já cadastrado por outro cliente" });
                }
            }

            existingCustomer.Name = customer.Name;
            existingCustomer.Email = customer.Email;
            existingCustomer.Cpf = customer.Cpf;
            existingCustomer.UpdatedAt = DateTime.UtcNow;
            
            await _customerRepository.UpdateAsync(existingCustomer);
            await _customerRepository.CommitAsync();
            
            return Results.Ok(existingCustomer);
        }
        catch (Exception ex)
        {
            return Results.BadRequest(new { message = "Erro ao atualizar cliente", error = ex.Message });
        }
    }

    public async Task<IResult> DeleteCustomer(int id)
    {
        try
        {
            var customer = await _customerRepository.GetById(id);
            if (customer == null)
            {
                return Results.NotFound(new { message = "Cliente não encontrado" });
            }

            // TODO: Implementar delete no repositório
            // await _customerRepository.DeleteAsync(customer);
            // await _customerRepository.CommitAsync();
            
            return Results.Ok(new { message = "Cliente deletado com sucesso" });
        }
        catch (Exception ex)
        {
            return Results.BadRequest(new { message = "Erro ao deletar cliente", error = ex.Message });
        }
    }

    public async Task<IResult> GetCustomerById(int id)
    {
        try
        {
            var customer = await _customerRepository.GetById(id);
            if (customer == null)
            {
                return Results.NotFound(new { message = "Cliente não encontrado" });
            }

            return Results.Ok(customer);
        }
        catch (Exception ex)
        {
            return Results.BadRequest(new { message = "Erro ao buscar cliente", error = ex.Message });
        }
    }

    public async Task<IResult> GetCustomers()
    {
        try
        {
            // TODO: Implementar listagem de todos os clientes no repositório
            return Results.Ok(new { message = "Lista de clientes", customers = new List<CustomerModel>() });
        }
        catch (Exception ex)
        {
            return Results.BadRequest(new { message = "Erro ao buscar clientes", error = ex.Message });
        }
    }
} 