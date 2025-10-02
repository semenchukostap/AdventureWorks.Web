using AdventureWorks.Web.Models;
using AdventureWorks.Web.Services;
using Microsoft.EntityFrameworkCore;

namespace AdventureWorks.Web.ServiceImplementations;

/// <summary>
/// SOAP service implementation for Product operations.
/// Provides CRUD operations for products with eager loading of related entities.
/// </summary>
public class ProductService : IProductService
{
    private readonly SampleDbContext _context;

    /// <summary>
    /// Initializes a new instance of the ProductService class.
    /// </summary>
    /// <param name="context">The database context for data access.</param>
    public ProductService(SampleDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    /// <summary>
    /// Retrieves all products with their associated category and model information.
    /// </summary>
    /// <returns>A list of all products with eager-loaded navigation properties.</returns>
    public async Task<List<Product>> GetAllProductsAsync()
    {
        return await _context.Product
            .Include(p => p.ProductCategory)
            .Include(p => p.ProductModel)
            .ToListAsync();
    }

    /// <summary>
    /// Retrieves a specific product by its ID with related entities.
    /// </summary>
    /// <param name="productId">The unique identifier of the product.</param>
    /// <returns>The product if found; otherwise, null.</returns>
    public async Task<Product?> GetProductByIdAsync(int productId)
    {
        return await _context.Product
            .Include(p => p.ProductCategory)
            .Include(p => p.ProductModel)
            .FirstOrDefaultAsync(m => m.ProductId == productId);
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

        _context.Product.Add(product);
        await _context.SaveChangesAsync();
        return product;
    }

    /// <summary>
    /// Updates an existing product in the database.
    /// </summary>
    /// <param name="product">The product entity with updated values.</param>
    /// <returns>True if the update was successful; false if the product doesn't exist.</returns>
    /// <exception cref="ArgumentNullException">Thrown when product is null.</exception>
    /// <exception cref="DbUpdateConcurrencyException">Rethrown if concurrency conflict occurs with an existing product.</exception>
    public async Task<bool> UpdateProductAsync(Product product)
    {
        if (product == null)
        {
            throw new ArgumentNullException(nameof(product));
        }

        try
        {
            _context.Update(product);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await ProductExistsAsync(product.ProductId))
            {
                return false;
            }
            throw;
        }
    }

    /// <summary>
    /// Deletes a product from the database.
    /// </summary>
    /// <param name="productId">The unique identifier of the product to delete.</param>
    /// <returns>True if the product was deleted; false if the product doesn't exist.</returns>
    public async Task<bool> DeleteProductAsync(int productId)
    {
        var product = await _context.Product.FindAsync(productId);
        if (product == null)
        {
            return false;
        }

        _context.Product.Remove(product);
        await _context.SaveChangesAsync();
        return true;
    }

    /// <summary>
    /// Checks if a product exists in the database.
    /// </summary>
    /// <param name="id">The product ID to check.</param>
    /// <returns>True if the product exists; otherwise, false.</returns>
    private async Task<bool> ProductExistsAsync(int id)
    {
        return await _context.Product.AnyAsync(e => e.ProductId == id);
    }
}
