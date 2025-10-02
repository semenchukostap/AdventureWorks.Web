using AdventureWorks.Web.Models;
using AdventureWorks.Web.Services;
using Microsoft.EntityFrameworkCore;

namespace AdventureWorks.Web.ServiceImplementations;

/// <summary>
/// Service implementation for customer-related operations.
/// Migrates business logic from legacy CustomersController with MVC-specific code removed
/// (View returns, ActionResults, ValidateAntiForgeryToken, Bind attributes).
/// </summary>
public class CustomerService : ICustomerService
{
    private readonly SampleDbContext _context;

    /// <summary>
    /// Initializes a new instance of the CustomerService class.
    /// </summary>
    /// <param name="context">The database context for customer operations.</param>
    /// <exception cref="ArgumentNullException">Thrown when context is null.</exception>
    public CustomerService(SampleDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    /// <summary>
    /// Retrieves all customers from the database.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation. The task result contains a list of all customers.</returns>
    public async Task<List<Customer>> GetAllCustomersAsync()
    {
        return await _context.Customer.ToListAsync();
    }

    /// <summary>
    /// Retrieves a customer by their unique identifier.
    /// </summary>
    /// <param name="customerId">The unique identifier of the customer.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the customer if found; otherwise, null.</returns>
    public async Task<Customer?> GetCustomerByIdAsync(int customerId)
    {
        return await _context.Customer
            .FirstOrDefaultAsync(m => m.CustomerId == customerId);
    }

    /// <summary>
    /// Creates a new customer in the database.
    /// </summary>
    /// <param name="customer">The customer entity to create.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the created customer.</returns>
    /// <exception cref="ArgumentNullException">Thrown when customer is null.</exception>
    public async Task<Customer> CreateCustomerAsync(Customer customer)
    {
        if (customer == null)
        {
            throw new ArgumentNullException(nameof(customer));
        }

        _context.Customer.Add(customer);
        await _context.SaveChangesAsync();
        return customer;
    }

    /// <summary>
    /// Updates an existing customer in the database.
    /// </summary>
    /// <param name="customer">The customer entity with updated values.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains true if the update was successful; otherwise, false.</returns>
    /// <exception cref="ArgumentNullException">Thrown when customer is null.</exception>
    /// <exception cref="DbUpdateConcurrencyException">Thrown when a concurrency conflict occurs and the customer still exists.</exception>
    public async Task<bool> UpdateCustomerAsync(Customer customer)
    {
        if (customer == null)
        {
            throw new ArgumentNullException(nameof(customer));
        }

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

    /// <summary>
    /// Deletes a customer from the database.
    /// </summary>
    /// <param name="customerId">The unique identifier of the customer to delete.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains true if the customer was deleted; otherwise, false if the customer was not found.</returns>
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

    /// <summary>
    /// Checks if a customer exists in the database.
    /// </summary>
    /// <param name="id">The unique identifier of the customer.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains true if the customer exists; otherwise, false.</returns>
    private async Task<bool> CustomerExistsAsync(int id)
    {
        return await _context.Customer.AnyAsync(e => e.CustomerId == id);
    }
}