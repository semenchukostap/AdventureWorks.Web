using AdventureWorks.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace AdventureWorks.Web.Services
{
    /// <summary>
    /// Provides customer service operations for SOAP web services.
    /// Implements the ICustomerService contract with Entity Framework Core data access.
    /// </summary>
    public class CustomerService : ICustomerService
    {
        private readonly sampledbContext _context;

        /// <summary>
        /// Initializes a new instance of the CustomerService class.
        /// </summary>
        /// <param name="context">The database context for customer data access.</param>
        /// <exception cref="ArgumentNullException">Thrown when context is null.</exception>
        public CustomerService(sampledbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        /// <summary>
        /// Retrieves all customers from the database asynchronously.
        /// </summary>
        /// <returns>A list of all customers.</returns>
        public async Task<List<Customer>> GetAllCustomersAsync()
        {
            return await _context.Customer.ToListAsync();
        }

        /// <summary>
        /// Retrieves a specific customer by their unique identifier asynchronously.
        /// </summary>
        /// <param name="customerId">The unique identifier of the customer.</param>
        /// <returns>The customer if found; otherwise, null.</returns>
        public async Task<Customer?> GetCustomerByIdAsync(int customerId)
        {
            return await _context.Customer
                .FirstOrDefaultAsync(c => c.CustomerId == customerId);
        }

        /// <summary>
        /// Creates a new customer in the database asynchronously.
        /// </summary>
        /// <param name="customer">The customer entity to create.</param>
        /// <returns>The unique identifier of the newly created customer.</returns>
        /// <exception cref="ArgumentNullException">Thrown when customer is null.</exception>
        /// <exception cref="DbUpdateException">Thrown when the database update fails.</exception>
        public async Task<int> CreateCustomerAsync(Customer customer)
        {
            if (customer == null)
            {
                throw new ArgumentNullException(nameof(customer));
            }

            _context.Customer.Add(customer);
            await _context.SaveChangesAsync();
            return customer.CustomerId;
        }

        /// <summary>
        /// Updates an existing customer in the database asynchronously.
        /// </summary>
        /// <param name="customer">The customer entity with updated information.</param>
        /// <returns>True if the update was successful; otherwise, false.</returns>
        public async Task<bool> UpdateCustomerAsync(Customer customer)
        {
            if (customer == null)
            {
                return false;
            }

            try
            {
                _context.Update(customer);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                // Check if the customer still exists
                if (!await _context.Customer.AnyAsync(c => c.CustomerId == customer.CustomerId))
                {
                    return false;
                }
                throw;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Deletes a customer from the database by their unique identifier asynchronously.
        /// </summary>
        /// <param name="customerId">The unique identifier of the customer to delete.</param>
        /// <returns>True if the deletion was successful; otherwise, false.</returns>
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
    }
}
