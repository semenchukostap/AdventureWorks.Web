using Microsoft.EntityFrameworkCore;
using AdventureWorks.Soap.Data;
using AdventureWorks.Soap.Models;
using AdventureWorks.Soap.Services.Contracts;

namespace AdventureWorks.Soap.Services;

public class ProductCategoryService : IProductCategoryService
{
    private readonly AdventureWorksContext _context;
    private readonly ILogger<ProductCategoryService> _logger;

    public ProductCategoryService(AdventureWorksContext context, ILogger<ProductCategoryService> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<List<ProductCategory>> GetAllProductCategoriesAsync()
    {
        _logger.LogInformation("Retrieving all product categories");
        
        return await _context.ProductCategories
            .Include(p => p.ParentProductCategory)
            .ToListAsync();
    }

    public async Task<ProductCategory?> GetProductCategoryByIdAsync(int id)
    {
        _logger.LogInformation("Retrieving product category with ID: {ProductCategoryId}", id);
        
        return await _context.ProductCategories
            .Include(p => p.ParentProductCategory)
            .FirstOrDefaultAsync(m => m.ProductCategoryId == id);
    }

    public async Task<ProductCategory> CreateProductCategoryAsync(ProductCategory productCategory)
    {
        _logger.LogInformation("Creating new product category: {ProductCategoryName}", productCategory.Name);
        
        _context.ProductCategories.Add(productCategory);
        await _context.SaveChangesAsync();
        
        _logger.LogInformation("Product category created successfully with ID: {ProductCategoryId}", productCategory.ProductCategoryId);
        
        return productCategory;
    }

    public async Task<bool> UpdateProductCategoryAsync(int id, ProductCategory productCategory)
    {
        if (id != productCategory.ProductCategoryId)
        {
            _logger.LogWarning("Product category ID mismatch. URL ID: {UrlId}, Entity ID: {EntityId}", id, productCategory.ProductCategoryId);
            return false;
        }

        _logger.LogInformation("Updating product category with ID: {ProductCategoryId}", id);

        _context.Entry(productCategory).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
            _logger.LogInformation("Product category updated successfully with ID: {ProductCategoryId}", id);
            return true;
        }
        catch (DbUpdateConcurrencyException ex)
        {
            if (!await ProductCategoryExistsAsync(id))
            {
                _logger.LogWarning("Product category with ID: {ProductCategoryId} not found during update", id);
                return false;
            }
            
            _logger.LogError(ex, "Concurrency error while updating product category with ID: {ProductCategoryId}", id);
            throw;
        }
    }

    public async Task<bool> DeleteProductCategoryAsync(int id)
    {
        _logger.LogInformation("Deleting product category with ID: {ProductCategoryId}", id);
        
        var productCategory = await _context.ProductCategories.FindAsync(id);
        if (productCategory == null)
        {
            _logger.LogWarning("Product category with ID: {ProductCategoryId} not found for deletion", id);
            return false;
        }

        _context.ProductCategories.Remove(productCategory);
        await _context.SaveChangesAsync();
        
        _logger.LogInformation("Product category deleted successfully with ID: {ProductCategoryId}", id);
        
        return true;
    }

    private async Task<bool> ProductCategoryExistsAsync(int id)
    {
        return await _context.ProductCategories.AnyAsync(e => e.ProductCategoryId == id);
    }
}
