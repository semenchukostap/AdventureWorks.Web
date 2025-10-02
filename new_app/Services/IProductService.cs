using System.ServiceModel;
using AdventureWorks.Web.Models;

namespace AdventureWorks.Web.Services;

/// <summary>
/// SOAP service contract for Product management operations.
/// Replaces the legacy MVC ProductsController with SOAP web service endpoints.
/// </summary>
[ServiceContract]
public interface IProductService
{
    /// <summary>
    /// Retrieves all products from the database.
    /// Includes related ProductCategory and ProductModel data.
    /// </summary>
    /// <returns>A list of all products.</returns>
    [OperationContract]
    Task<List<Product>> GetAllProductsAsync();

    /// <summary>
    /// Retrieves a single product by its unique identifier.
    /// Includes related ProductCategory and ProductModel data.
    /// </summary>
    /// <param name="productId">The unique identifier of the product.</param>
    /// <returns>The product if found; otherwise, null.</returns>
    [OperationContract]
    Task<Product?> GetProductByIdAsync(int productId);

    /// <summary>
    /// Creates a new product in the database.
    /// </summary>
    /// <param name="product">The product entity to create.</param>
    /// <returns>The created product with generated identifiers.</returns>
    [OperationContract]
    Task<Product> CreateProductAsync(Product product);

    /// <summary>
    /// Updates an existing product in the database.
    /// </summary>
    /// <param name="product">The product entity with updated values.</param>
    /// <returns>True if the update was successful; false if the product was not found.</returns>
    [OperationContract]
    Task<bool> UpdateProductAsync(Product product);

    /// <summary>
    /// Deletes a product from the database by its unique identifier.
    /// </summary>
    /// <param name="productId">The unique identifier of the product to delete.</param>
    /// <returns>True if the deletion was successful; false if the product was not found.</returns>
    [OperationContract]
    Task<bool> DeleteProductAsync(int productId);
}
