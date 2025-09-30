using System.ServiceModel;
using AdventureWorks.Web.Models;

namespace AdventureWorks.Web.Services
{
    /// <summary>
    /// SOAP service contract for customer operations.
    /// This interface defines the contract for SOAP web service operations exposed via SoapCore.
    /// </summary>
    [ServiceContract]
    public interface ICustomerService
    {
        /// <summary>
        /// Retrieves all customers from the database.
        /// </summary>
        /// <returns>A list of all customers.</returns>
        [OperationContract]
        Task<List<Customer>> GetAllCustomersAsync();

        /// <summary>
        /// Retrieves a specific customer by their unique identifier.
        /// </summary>
        /// <param name="customerId">The unique identifier of the customer.</param>
        /// <returns>The customer if found; otherwise, null.</returns>
        [OperationContract]
        Task<Customer?> GetCustomerByIdAsync(int customerId);

        /// <summary>
        /// Creates a new customer in the database.
        /// </summary>
        /// <param name="customer">The customer entity to create.</param>
        /// <returns>The customer ID of the newly created customer.</returns>
        [OperationContract]
        Task<int> CreateCustomerAsync(Customer customer);

        /// <summary>
        /// Updates an existing customer in the database.
        /// </summary>
        /// <param name="customer">The customer entity with updated information.</param>
        /// <returns>True if the update was successful; otherwise, false.</returns>
        [OperationContract]
        Task<bool> UpdateCustomerAsync(Customer customer);

        /// <summary>
        /// Deletes a customer from the database by their unique identifier.
        /// </summary>
        /// <param name="customerId">The unique identifier of the customer to delete.</param>
        /// <returns>True if the deletion was successful; otherwise, false.</returns>
        [OperationContract]
        Task<bool> DeleteCustomerAsync(int customerId);
    }
}
