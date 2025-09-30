using AdventureWorks.Web.Models;
using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;

namespace AdventureWorks.Web.Services
{
    /// <summary>
    /// Service interface for managing customers.
    /// </summary>
    [ServiceContract]
    public interface ICustomerService
    {
        /// <summary>
        /// Retrieves all customers asynchronously.
        /// </summary>
        /// <returns>A list of all customers.</returns>
        [OperationContract]
        Task<List<Customer>> GetAllCustomersAsync();
        
        /// <summary>
        /// Retrieves a customer by their ID asynchronously.
        /// </summary>
        /// <param name="id">The ID of the customer to retrieve.</param>
        /// <returns>The customer if found; otherwise, null.</returns>
        [OperationContract]
        Task<Customer?> GetCustomerByIdAsync(int id);
        
        /// <summary>
        /// Adds a new customer asynchronously.
        /// </summary>
        /// <param name="customer">The customer to add.</param>
        /// <returns>True if the customer was successfully added; otherwise, false.</returns>
        [OperationContract]
        Task<bool> AddCustomerAsync(Customer customer);
        
        /// <summary>
        /// Updates an existing customer asynchronously.
        /// </summary>
        /// <param name="customer">The customer with updated information.</param>
        /// <returns>True if the customer was successfully updated; otherwise, false.</returns>
        [OperationContract]
        Task<bool> UpdateCustomerAsync(Customer customer);
        
        /// <summary>
        /// Deletes a customer by their ID asynchronously.
        /// </summary>
        /// <param name="id">The ID of the customer to delete.</param>
        /// <returns>True if the customer was successfully deleted; otherwise, false.</returns>
        [OperationContract]
        Task<bool> DeleteCustomerAsync(int id);
    }
}