using AdventureWorks.Web.Models;
using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;

namespace AdventureWorks.Web.Services
{
    [ServiceContract]
    public interface ICustomerService
    {
        [OperationContract]
        Task<List<Customer>> GetAllCustomersAsync();
        
        [OperationContract]
        Task<Customer?> GetCustomerByIdAsync(int id);
        
        [OperationContract]
        Task<bool> AddCustomerAsync(Customer customer);
        
        [OperationContract]
        Task<bool> UpdateCustomerAsync(Customer customer);
        
        [OperationContract]
        Task<bool> DeleteCustomerAsync(int id);
    }
}