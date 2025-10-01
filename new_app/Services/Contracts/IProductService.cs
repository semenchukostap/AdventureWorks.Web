using System.ServiceModel;
using AdventureWorks.Soap.Models;

namespace AdventureWorks.Soap.Services.Contracts;

/// <summary>
/// SOAP service contract for Product operations.
/// Provides CRUD operations for managing products in the AdventureWorks database.
/// </summary>
[ServiceContract]
public interface IProductService
{
    /// <summary>
    /// Retrieves all products from the database with related category and model information.
    /// </summary>
    /// <returns>A list of all products.</returns>
    [OperationContract]
    Task<List<Product>> GetAllProductsAsync();

    /// <summary>
    /// Retrieves a specific product by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the product.</param>
    /// <returns>The product if found; otherwise, null.</returns>
    [OperationContract]
    Task<Product?> GetProductByIdAsync(int id);

    /// <summary>
    /// Creates a new product in the database.
    /// </summary>
    /// <param name="product">The product entity to create.</param>
    /// <returns>The created product with generated values.</returns>
    [OperationContract]
    Task<Product> CreateProductAsync(Product product);

    /// <summary>
    /// Updates an existing product in the database.
    /// </summary>
    /// <param name="id">The unique identifier of the product to update.</param>
    /// <param name="product">The product entity with updated values.</param>
    /// <returns>True if the update was successful; otherwise, false.</returns>
    [OperationContract]
    Task<bool> UpdateProductAsync(int id, Product product);

    /// <summary>
    /// Deletes a product from the database.
    /// </summary>
    /// <param name="id">The unique identifier of the product to delete.</param>
    /// <returns>True if the deletion was successful; otherwise, false.</returns>
    [OperationContract]
    Task<bool> DeleteProductAsync(int id);
}
