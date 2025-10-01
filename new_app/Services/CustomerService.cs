using Microsoft.EntityFrameworkCore;
using AdventureWorks.Soap.Data;
using AdventureWorks.Soap.Models;
using AdventureWorks.Soap.Services.Contracts;

namespace AdventureWorks.Soap.Services;

/// <summary>
/// SOAP service implementation for Customer entity operations.
/// Provides CRUD operations for managing customer data.
/// </summary>
public class CustomerService : ICustomerService
{
    private readonly AdventureWorksContext _context;
    private readonly ILogger<CustomerService> _logger;

    /// <summary>
    /// Initializes a new instance of the CustomerService class.
    /// </summary>
    /// <param name="context">The database context for AdventureWorks.</param>
    /// <param name="logger">The logger instance for this service.</param>
    /// <exception cref="ArgumentNullException">Thrown when context or logger is null.</exception>
    public CustomerService(AdventureWorksContext context, ILogger<CustomerService> logger)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    /// <summary>
    /// Retrieves all customers from the database.
    /// </summary>
    /// <returns>A list of all customers.</returns>
    public async Task<List<Customer>> GetAllCustomersAsync()
    {
        try
        {
            _logger.LogInformation("Retrieving all customers");
            var customers = await _context.Customers.ToListAsync();
            _logger.LogInformation("Successfully retrieved {Count} customers", customers.Count);
            return customers;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while retrieving all customers");
            throw;
        }
    }

    /// <summary>
    /// Retrieves a specific customer by ID.
    /// </summary>
    /// <param name="id">The customer ID to search for.</param>
    /// <returns>The customer if found; otherwise, null.</returns>
    public async Task<Customer?> GetCustomerByIdAsync(int id)
    {
        try
        {
            _logger.LogInformation("Retrieving customer with ID: {CustomerId}", id);
            var customer = await _context.Customers
                .FirstOrDefaultAsync(m => m.CustomerId == id);

            if (customer == null)
            {
                _logger.LogWarning("Customer with ID {CustomerId} not found", id);
            }
            else
            {
                _logger.LogInformation("Successfully retrieved customer with ID: {CustomerId}", id);
            }

            return customer;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while retrieving customer with ID: {CustomerId}", id);
            throw;
        }
    }

    /// <summary>
    /// Creates a new customer in the database.
    /// </summary>
    /// <param name="customer">The customer entity to create.</param>
    /// <returns>The created customer with generated ID.</returns>
    /// <exception cref="ArgumentNullException">Thrown when customer is null.</exception>
    public async Task<Customer> CreateCustomerAsync(Customer customer)
    {
        if (customer == null)
        {
            throw new ArgumentNullException(nameof(customer));
        }

        try
        {
            _logger.LogInformation("Creating new customer: {FirstName} {LastName}", customer.FirstName, customer.LastName);
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Successfully created customer with ID: {CustomerId}", customer.CustomerId);
            return customer;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while creating customer: {FirstName} {LastName}", customer.FirstName, customer.LastName);
            throw;
        }
    }

    /// <summary>
    /// Updates an existing customer in the database.
    /// </summary>
    /// <param name="id">The ID of the customer to update.</param>
    /// <param name="customer">The customer entity with updated values.</param>
    /// <returns>True if the update was successful; false if the customer was not found or ID mismatch.</returns>
    /// <exception cref="ArgumentNullException">Thrown when customer is null.</exception>
    public async Task<bool> UpdateCustomerAsync(int id, Customer customer)
    {
        if (customer == null)
        {
            throw new ArgumentNullException(nameof(customer));
        }

        if (id != customer.CustomerId)
        {
            _logger.LogWarning("Customer ID mismatch. URL ID: {UrlId}, Customer ID: {CustomerId}", id, customer.CustomerId);
            return false;
        }

        _context.Entry(customer).State = EntityState.Modified;

        try
        {
            _logger.LogInformation("Updating customer with ID: {CustomerId}", id);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Successfully updated customer with ID: {CustomerId}", id);
            return true;
        }
        catch (DbUpdateConcurrencyException ex)
        {
            if (!await CustomerExistsAsync(id))
            {
                _logger.LogWarning("Customer with ID {CustomerId} not found during update", id);
                return false;
            }
            else
            {
                _logger.LogError(ex, "Concurrency error occurred while updating customer with ID: {CustomerId}", id);
                throw;
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while updating customer with ID: {CustomerId}", id);
            throw;
        }
    }

    /// <summary>
    /// Deletes a customer from the database.
    /// </summary>
    /// <param name="id">The ID of the customer to delete.</param>
    /// <returns>True if the deletion was successful; false if the customer was not found.</returns>
    public async Task<bool> DeleteCustomerAsync(int id)
    {
        try
        {
            _logger.LogInformation("Attempting to delete customer with ID: {CustomerId}", id);
            var customer = await _context.Customers.FindAsync(id);

            if (customer == null)
            {
                _logger.LogWarning("Customer with ID {CustomerId} not found for deletion", id);
                return false;
            }

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Successfully deleted customer with ID: {CustomerId}", id);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while deleting customer with ID: {CustomerId}", id);
            throw;
        }
    }

    /// <summary>
    /// Checks if a customer exists in the database.
    /// </summary>
    /// <param name="id">The customer ID to check.</param>
    /// <returns>True if the customer exists; otherwise, false.</returns>
    private async Task<bool> CustomerExistsAsync(int id)
    {
        try
        {
            return await _context.Customers.AnyAsync(e => e.CustomerId == id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while checking if customer exists with ID: {CustomerId}", id);
            throw;
        }
    }
}