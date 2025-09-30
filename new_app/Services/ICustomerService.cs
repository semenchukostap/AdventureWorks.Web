using AdventureWorks.Web.Models;
using System.ServiceModel;

namespace AdventureWorks.Web.Services
{
    [ServiceContract]
    public interface ICustomerService
    {
        [OperationContract]
        List<Customer> GetAllCustomers();
        
        [OperationContract]
        Customer? GetCustomerById(int id);
        
        [OperationContract]
        bool AddCustomer(Customer customer);
        
        [OperationContract]
        bool UpdateCustomer(Customer customer);
        
        [OperationContract]
        bool DeleteCustomer(int id);
    }
}