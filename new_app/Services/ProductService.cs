using Microsoft.EntityFrameworkCore;
using AdventureWorks.Soap.Data;
using AdventureWorks.Soap.Models;
using AdventureWorks.Soap.Services.Contracts;

namespace AdventureWorks.Soap.Services;

/// <summary>
/// Product service implementation providing SOAP operations for Product entity management.
/// Implements business logic migrated from legacy ProductsController.
/// </summary>
public class ProductService : IProductService
{
    private readonly AdventureWorksContext _context;
    private readonly ILogger<ProductService> _logger;

    /// <summary>
    /// Initializes a new instance of the ProductService class.
    /// </summary>
    /// <param name="context">The database context for AdventureWorks.</param>
    /// <param name="logger">The logger instance for ProductService.</param>
    /// <exception cref="ArgumentNullException">Thrown when context or logger is null.</exception>
    public ProductService(AdventureWorksContext context, ILogger<ProductService> logger)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    /// <summary>
    /// Retrieves all products with related ProductCategory and ProductModel data.
    /// This method includes navigation properties for category and model information.
    /// </summary>
    /// <returns>A list of all products including related category and model entities.</returns>
    public async Task<List<Product>> GetAllProductsAsync()
    {
        try
        {
            _logger.LogInformation("Retrieving all products with related entities.");

            var products = await _context.Products
                .Include(p => p.ProductCategory)
                .Include(p => p.ProductModel)
                .ToListAsync();

            _logger.LogInformation("Successfully retrieved {Count} products.", products.Count);
            return products;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while retrieving all products.");
            throw;
        }
    }

    /// <summary>
    /// Retrieves a specific product by its ID with related ProductCategory and ProductModel data.
    /// Returns null if the product is not found.
    /// </summary>
    /// <param name="id">The product ID to retrieve.</param>
    /// <returns>The product with the specified ID including related entities, or null if not found.</returns>
    public async Task<Product?> GetProductByIdAsync(int id)
    {
        try
        {
            _logger.LogInformation("Retrieving product with ID: {ProductId}", id);

            var product = await _context.Products
                .Include(p => p.ProductCategory)
                .Include(p => p.ProductModel)
                .FirstOrDefaultAsync(m => m.ProductId == id);

            if (product == null)
            {
                _logger.LogWarning("Product with ID {ProductId} not found.", id);
            }
            else
            {
                _logger.LogInformation("Successfully retrieved product with ID: {ProductId} and Name: {ProductName}", id, product.Name);
            }

            return product;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while retrieving product with ID: {ProductId}", id);
            throw;
        }
    }

    /// <summary>
    /// Creates a new product in the database.
    /// </summary>
    /// <param name="product">The product entity to create.</param>
    /// <returns>The created product with generated ID.</returns>
    /// <exception cref="ArgumentNullException">Thrown when product is null.</exception>
    public async Task<Product> CreateProductAsync(Product product)
    {
        if (product == null)
        {
            throw new ArgumentNullException(nameof(product));
        }

        try
        {
            _logger.LogInformation("Creating new product: {ProductName}", product.Name);

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Successfully created product with ID: {ProductId} and Name: {ProductName}", product.ProductId, product.Name);
            return product;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while creating product: {ProductName}", product.Name);
            throw;
        }
    }

    /// <summary>
    /// Updates an existing product in the database.
    /// Validates that the ID parameter matches the product's ID.
    /// </summary>
    /// <param name="id">The ID of the product to update.</param>
    /// <param name="product">The updated product entity.</param>
    /// <returns>True if the update was successful, false if the ID mismatch or product not found.</returns>
    /// <exception cref="ArgumentNullException">Thrown when product is null.</exception>
    public async Task<bool> UpdateProductAsync(int id, Product product)
    {
        if (product == null)
        {
            throw new ArgumentNullException(nameof(product));
        }

        if (id != product.ProductId)
        {
            _logger.LogWarning("Product ID mismatch. URL ID: {UrlId}, Product ID: {ProductId}", id, product.ProductId);
            return false;
        }

        try
        {
            _logger.LogInformation("Updating product with ID: {ProductId}", id);

            _context.Entry(product).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            _logger.LogInformation("Successfully updated product with ID: {ProductId}", id);
            return true;
        }
        catch (DbUpdateConcurrencyException ex)
        {
            if (!await ProductExistsAsync(id))
            {
                _logger.LogWarning("Product with ID {ProductId} not found during update.", id);
                return false;
            }
            else
            {
                _logger.LogError(ex, "Concurrency error occurred while updating product with ID: {ProductId}", id);
                throw;
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while updating product with ID: {ProductId}", id);
            throw;
        }
    }

    /// <summary>
    /// Deletes a product from the database.
    /// </summary>
    /// <param name="id">The ID of the product to delete.</param>
    /// <returns>True if the deletion was successful, false if the product was not found.</returns>
    public async Task<bool> DeleteProductAsync(int id)
    {
        try
        {
            _logger.LogInformation("Deleting product with ID: {ProductId}", id);

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                _logger.LogWarning("Product with ID {ProductId} not found for deletion.", id);
                return false;
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Successfully deleted product with ID: {ProductId} and Name: {ProductName}", id, product.Name);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while deleting product with ID: {ProductId}", id);
            throw;
        }
    }

    /// <summary>
    /// Checks if a product exists in the database.
    /// </summary>
    /// <param name="id">The product ID to check.</param>
    /// <returns>True if the product exists, false otherwise.</returns>
    private async Task<bool> ProductExistsAsync(int id)
    {
        return await _context.Products.AnyAsync(e => e.ProductId == id);
    }
}