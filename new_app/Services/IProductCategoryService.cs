using System.ServiceModel;
using AdventureWorks.Web.Models;

namespace AdventureWorks.Web.Services;

/// <summary>
/// SOAP service contract for ProductCategory management operations.
/// Replaces the legacy MVC ProductCategoriesController with SOAP web service interface.
/// </summary>
[ServiceContract]
public interface IProductCategoryService
{
    /// <summary>
    /// Retrieves all product categories from the database.
    /// Maps to the legacy Index action in ProductCategoriesController.
    /// </summary>
    /// <returns>A list of all product categories including parent category relationships.</returns>
    [OperationContract]
    Task<List<ProductCategory>> GetAllProductCategoriesAsync();

    /// <summary>
    /// Retrieves a single product category by its unique identifier.
    /// Maps to the legacy Details action in ProductCategoriesController.
    /// </summary>
    /// <param name="productCategoryId">The unique identifier of the product category.</param>
    /// <returns>The product category if found; otherwise, null.</returns>
    [OperationContract]
    Task<ProductCategory?> GetProductCategoryByIdAsync(int productCategoryId);

    /// <summary>
    /// Creates a new product category in the database.
    /// Maps to the legacy Create POST action in ProductCategoriesController.
    /// </summary>
    /// <param name="productCategory">The product category entity to create.</param>
    /// <returns>The created product category with generated values.</returns>
    [OperationContract]
    Task<ProductCategory> CreateProductCategoryAsync(ProductCategory productCategory);

    /// <summary>
    /// Updates an existing product category in the database.
    /// Maps to the legacy Edit POST action in ProductCategoriesController.
    /// </summary>
    /// <param name="productCategory">The product category entity with updated values.</param>
    /// <returns>True if the update was successful; false if the product category was not found.</returns>
    [OperationContract]
    Task<bool> UpdateProductCategoryAsync(ProductCategory productCategory);

    /// <summary>
    /// Deletes a product category from the database by its unique identifier.
    /// Maps to the legacy DeleteConfirmed action in ProductCategoriesController.
    /// </summary>
    /// <param name="productCategoryId">The unique identifier of the product category to delete.</param>
    /// <returns>True if the deletion was successful; false if the product category was not found.</returns>
    [OperationContract]
    Task<bool> DeleteProductCategoryAsync(int productCategoryId);
}