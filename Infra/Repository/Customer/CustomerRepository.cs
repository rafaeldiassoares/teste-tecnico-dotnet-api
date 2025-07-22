using dotnet_api.Models;
using Microsoft.EntityFrameworkCore;

namespace dotnet_api.Infra.Repository.Customer;

public class CustomerRepository: ICustomerRepository
{
    private readonly ApplicationDbContext _context;
    
    public CustomerRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(CustomerModel customer)
    {
        await _context.Customers.AddAsync(customer);
    }

    public async Task UpdateAsync(CustomerModel customer)
    {
        _context.Customers.Update(customer);
        await Task.CompletedTask;
    }

    public async Task<CustomerModel?> GetById(int id)
    {
        return await _context.Customers
            .Include(c => c.Addresses)
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<CustomerModel?> GetByCpfEmail(string cpf, string email)
    {
        return await _context.Customers
            .Include(c => c.Addresses)
            .FirstOrDefaultAsync(c => c.Cpf == cpf || c.Email == email);
    }

    public async Task<IEnumerable<CustomerModel>> GetAll()
    {
        return await _context.Customers
            .Include(c => c.Addresses)
            .ToListAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var customer = await _context.Customers.FindAsync(id);

        if(customer == null) return;

        _context.Customers.Remove(customer);
        await Task.CompletedTask;
    }

    public async Task CommitAsync()
    {
        await _context.SaveChangesAsync();
    }
}