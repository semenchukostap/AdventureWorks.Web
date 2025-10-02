using AdventureWorks.Web.Models;
using AdventureWorks.Web.Services;
using Microsoft.EntityFrameworkCore;

namespace AdventureWorks.Web.ServiceImplementations;

public class ProductCategoryService : IProductCategoryService
{
    private readonly SampleDbContext _context;

    public ProductCategoryService(SampleDbContext context)
    {
        _context = context;
    }

    public async Task<List<ProductCategory>> GetAllProductCategoriesAsync()
    {
        return await _context.ProductCategory
            .Include(p => p.ParentProductCategory)
            .ToListAsync();
    }

    public async Task<ProductCategory?> GetProductCategoryByIdAsync(int productCategoryId)
    {
        return await _context.ProductCategory
            .Include(p => p.ParentProductCategory)
            .FirstOrDefaultAsync(m => m.ProductCategoryId == productCategoryId);
    }

    public async Task<ProductCategory> CreateProductCategoryAsync(ProductCategory productCategory)
    {
        _context.ProductCategory.Add(productCategory);
        await _context.SaveChangesAsync();
        return productCategory;
    }

    public async Task<bool> UpdateProductCategoryAsync(ProductCategory productCategory)
    {
        try
        {
            _context.Update(productCategory);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await ProductCategoryExistsAsync(productCategory.ProductCategoryId))
            {
                return false;
            }
            throw;
        }
    }

    public async Task<bool> DeleteProductCategoryAsync(int productCategoryId)
    {
        var productCategory = await _context.ProductCategory.FindAsync(productCategoryId);
        if (productCategory == null)
        {
            return false;
        }

        _context.ProductCategory.Remove(productCategory);
        await _context.SaveChangesAsync();
        return true;
    }

    private async Task<bool> ProductCategoryExistsAsync(int id)
    {
        return await _context.ProductCategory.AnyAsync(e => e.ProductCategoryId == id);
    }
}
