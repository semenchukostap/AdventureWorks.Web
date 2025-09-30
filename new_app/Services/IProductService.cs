using AdventureWorks.Web.Models;
using System.ServiceModel;

namespace AdventureWorks.Web.Services
{
    [ServiceContract]
    public interface IProductService
    {
        [OperationContract]
        List<Product> GetAllProducts();
        
        [OperationContract]
        Product? GetProductById(int id);
        
        [OperationContract]
        bool AddProduct(Product product);
        
        [OperationContract]
        bool UpdateProduct(Product product);
        
        [OperationContract]
        bool DeleteProduct(int id);
    }
}