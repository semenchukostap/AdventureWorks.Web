using AdventureWorks.Web.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AdventureWorks.Web.Services
{
    public class ProductService : IProductService
    {
        private readonly sampledbContext _context;

        public ProductService(sampledbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<List<Product>> GetAllProductsAsync()
        {
            return await _context.Product
                .Include(p => p.ProductCategory)
                .Include(p => p.ProductModel)
                .ToListAsync();
        }

        public async Task<Product?> GetProductByIdAsync(int id)
        {
            return await _context.Product
                .Include(p => p.ProductCategory)
                .Include(p => p.ProductModel)
                .FirstOrDefaultAsync(p => p.ProductId == id);
        }

        public async Task<bool> AddProductAsync(Product product)
        {
            if (product == null)
            {
                return false;
            }

            try
            {
                await _context.Product.AddAsync(product);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> UpdateProductAsync(Product product)
        {
            if (product == null)
            {
                return false;
            }

            try
            {
                _context.Entry(product).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            try
            {
                var product = await _context.Product.FindAsync(id);
                if (product != null)
                {
                    _context.Product.Remove(product);
                    await _context.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        // Maintain backward compatibility with non-async methods
        public List<Product> GetAllProducts()
        {
            return GetAllProductsAsync().GetAwaiter().GetResult();
        }

        public Product? GetProductById(int id)
        {
            return GetProductByIdAsync(id).GetAwaiter().GetResult();
        }

        public bool AddProduct(Product product)
        {
            return AddProductAsync(product).GetAwaiter().GetResult();
        }

        public bool UpdateProduct(Product product)
        {
            return UpdateProductAsync(product).GetAwaiter().GetResult();
        }

        public bool DeleteProduct(int id)
        {
            return DeleteProductAsync(id).GetAwaiter().GetResult();
        }
    }
}