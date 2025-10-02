using System.ServiceModel;
using AdventureWorks.Web.Models;

namespace AdventureWorks.Web.Services;

/// <summary>
/// SOAP service contract for Customer management operations.
/// This interface defines the contract for CRUD operations on Customer entities.
/// Replaces the legacy MVC CustomersController with SOAP web service operations.
/// </summary>
[ServiceContract]
public interface ICustomerService
{
    /// <summary>
    /// Retrieves all customers from the database.
    /// </summary>
    /// <returns>A list of all Customer entities.</returns>
    [OperationContract]
    Task<List<Customer>> GetAllCustomersAsync();

    /// <summary>
    /// Retrieves a single customer by their unique identifier.
    /// </summary>
    /// <param name="customerId">The unique identifier of the customer.</param>
    /// <returns>The Customer entity if found; otherwise, null.</returns>
    [OperationContract]
    Task<Customer?> GetCustomerByIdAsync(int customerId);

    /// <summary>
    /// Creates a new customer in the database.
    /// </summary>
    /// <param name="customer">The Customer entity to create.</param>
    /// <returns>The created Customer entity with generated values.</returns>
    [OperationContract]
    Task<Customer> CreateCustomerAsync(Customer customer);

    /// <summary>
    /// Updates an existing customer in the database.
    /// </summary>
    /// <param name="customer">The Customer entity with updated values.</param>
    /// <returns>True if the update was successful; false if the customer was not found.</returns>
    [OperationContract]
    Task<bool> UpdateCustomerAsync(Customer customer);

    /// <summary>
    /// Deletes a customer from the database by their unique identifier.
    /// </summary>
    /// <param name="customerId">The unique identifier of the customer to delete.</param>
    /// <returns>True if the deletion was successful; false if the customer was not found.</returns>
    [OperationContract]
    Task<bool> DeleteCustomerAsync(int customerId);
}