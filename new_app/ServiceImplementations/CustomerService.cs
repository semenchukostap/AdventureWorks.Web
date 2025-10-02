using AdventureWorks.Web.Models;
using AdventureWorks.Web.Services;
using Microsoft.EntityFrameworkCore;

namespace AdventureWorks.Web.ServiceImplementations;

public class CustomerService : ICustomerService
{
    private readonly SampleDbContext _context;

    public CustomerService(SampleDbContext context)
    {
        _context = context;
    }

    public async Task<List<Customer>> GetAllCustomersAsync()
    {
        return await _context.Customer.ToListAsync();
    }

    public async Task<Customer?> GetCustomerByIdAsync(int customerId)
    {
        return await _context.Customer
            .FirstOrDefaultAsync(m => m.CustomerId == customerId);
    }

    public async Task<Customer> CreateCustomerAsync(Customer customer)
    {
        _context.Customer.Add(customer);
        await _context.SaveChangesAsync();
        return customer;
    }

    public async Task<bool> UpdateCustomerAsync(Customer customer)
    {
        try
        {
            _context.Update(customer);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await CustomerExistsAsync(customer.CustomerId))
            {
                return false;
            }
            throw;
        }
    }

    public async Task<bool> DeleteCustomerAsync(int customerId)
    {
        var customer = await _context.Customer.FindAsync(customerId);
        if (customer == null)
        {
            return false;
        }

        _context.Customer.Remove(customer);
        await _context.SaveChangesAsync();
        return true;
    }

    private async Task<bool> CustomerExistsAsync(int id)
    {
        return await _context.Customer.AnyAsync(e => e.CustomerId == id);
    }
}
