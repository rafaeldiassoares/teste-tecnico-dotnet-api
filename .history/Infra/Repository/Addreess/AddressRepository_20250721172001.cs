using dotnet_api.Models;
using Microsoft.EntityFrameworkCore;

namespace dotnet_api.Infra.Repository.Addreess;

public class AddressRepository: IAddressRepository
{
    private readonly ApplicationDbContext _context;
    
    public AddressRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(AddressesModel address)
    {
        await _context.Addresses.AddAsync(address);
    }

    public async Task UpdateAsync(AddressesModel address)
    {
        _context.Addresses.Update(address);
        await Task.CompletedTask;
    }

    public async Task<AddressesModel?> GetById(int id)
    {
        return await _context.Addresses
            .Include(a => a.Customer)
            .FirstOrDefaultAsync(a => a.Id == id);
    }

    public async Task CommitAsync()
    {
        await _context.SaveChangesAsync();
    }
}