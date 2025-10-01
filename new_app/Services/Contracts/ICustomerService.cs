using System.ServiceModel;
using AdventureWorks.Soap.Models;

namespace AdventureWorks.Soap.Services.Contracts;

/// <summary>
/// SOAP service contract for Customer operations
/// Migrated from legacy CustomersController MVC endpoints to SOAP operations
/// </summary>
[ServiceContract]
public interface ICustomerService
{
    /// <summary>
    /// Retrieves all customers from the database
    /// Legacy equivalent: GET /Customers (Index action)
    /// </summary>
    /// <returns>List of all Customer entities</returns>
    [OperationContract]
    Task<List<Customer>> GetAllCustomersAsync();

    /// <summary>
    /// Retrieves a specific customer by ID
    /// Legacy equivalent: GET /Customers/Details/{id}
    /// </summary>
    /// <param name="id">Customer ID</param>
    /// <returns>Customer entity or null if not found</returns>
    [OperationContract]
    Task<Customer?> GetCustomerByIdAsync(int id);

    /// <summary>
    /// Creates a new customer in the database
    /// Legacy equivalent: POST /Customers/Create
    /// </summary>
    /// <param name="customer">Customer entity to create</param>
    /// <returns>Created Customer entity with generated ID</returns>
    [OperationContract]
    Task<Customer> CreateCustomerAsync(Customer customer);

    /// <summary>
    /// Updates an existing customer
    /// Legacy equivalent: POST /Customers/Edit/{id}
    /// </summary>
    /// <param name="id">Customer ID to update</param>
    /// <param name="customer">Updated customer data</param>
    /// <returns>True if update successful, false if customer not found or ID mismatch</returns>
    [OperationContract]
    Task<bool> UpdateCustomerAsync(int id, Customer customer);

    /// <summary>
    /// Deletes a customer from the database
    /// Legacy equivalent: POST /Customers/Delete/{id}
    /// </summary>
    /// <param name="id">Customer ID to delete</param>
    /// <returns>True if deletion successful, false if customer not found</returns>
    [OperationContract]
    Task<bool> DeleteCustomerAsync(int id);
}
