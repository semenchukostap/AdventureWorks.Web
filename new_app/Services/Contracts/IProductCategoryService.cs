using System.ServiceModel;
using AdventureWorks.Soap.Models;

namespace AdventureWorks.Soap.Services.Contracts;

/// <summary>
/// SOAP service contract for ProductCategory operations.
/// Provides CRUD operations for managing product categories in the AdventureWorks system.
/// </summary>
[ServiceContract]
public interface IProductCategoryService
{
    /// <summary>
    /// Retrieves all product categories from the database.
    /// Includes parent product category relationships.
    /// </summary>
    /// <returns>A list of all product categories.</returns>
    [OperationContract]
    Task<List<ProductCategory>> GetAllProductCategoriesAsync();

    /// <summary>
    /// Retrieves a specific product category by its unique identifier.
    /// Includes parent product category relationship.
    /// </summary>
    /// <param name="id">The unique identifier of the product category.</param>
    /// <returns>The product category if found; otherwise, null.</returns>
    [OperationContract]
    Task<ProductCategory?> GetProductCategoryByIdAsync(int id);

    /// <summary>
    /// Creates a new product category in the database.
    /// </summary>
    /// <param name="productCategory">The product category entity to create.</param>
    /// <returns>The created product category with generated identifiers.</returns>
    [OperationContract]
    Task<ProductCategory> CreateProductCategoryAsync(ProductCategory productCategory);

    /// <summary>
    /// Updates an existing product category in the database.
    /// </summary>
    /// <param name="id">The unique identifier of the product category to update.</param>
    /// <param name="productCategory">The product category entity with updated values.</param>
    /// <returns>True if the update was successful; otherwise, false.</returns>
    [OperationContract]
    Task<bool> UpdateProductCategoryAsync(int id, ProductCategory productCategory);

    /// <summary>
    /// Deletes a product category from the database.
    /// </summary>
    /// <param name="id">The unique identifier of the product category to delete.</param>
    /// <returns>True if the deletion was successful; otherwise, false.</returns>
    [OperationContract]
    Task<bool> DeleteProductCategoryAsync(int id);
}
