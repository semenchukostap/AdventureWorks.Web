using AdventureWorks.Web.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AdventureWorks.Web.Services
{
    /// <summary>
    /// Service for managing product operations in the database
    /// </summary>
    public class ProductService : IProductService
    {
        private readonly sampledbContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductService"/> class
        /// </summary>
        /// <param name="context">The database context</param>
        /// <exception cref="ArgumentNullException">Thrown when context is null</exception>
        public ProductService(sampledbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        /// <summary>
        /// Retrieves all products from the database asynchronously
        /// </summary>
        /// <returns>A list of all products</returns>
        public async Task<List<Product>> GetAllProductsAsync()
        {
            return await _context.Product
                .Include(p => p.ProductCategory)
                .Include(p => p.ProductModel)
                .ToListAsync();
        }

        /// <summary>
        /// Retrieves a product by its ID asynchronously
        /// </summary>
        /// <param name="id">The ID of the product to retrieve</param>
        /// <returns>The product if found; otherwise, null</returns>
        public async Task<Product?> GetProductByIdAsync(int id)
        {
            return await _context.Product
                .Include(p => p.ProductCategory)
                .Include(p => p.ProductModel)
                .FirstOrDefaultAsync(p => p.ProductId == id);
        }

        /// <summary>
        /// Adds a new product to the database asynchronously
        /// </summary>
        /// <param name="product">The product to add</param>
        /// <returns>True if the product was added successfully; otherwise, false</returns>
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

        /// <summary>
        /// Updates an existing product in the database asynchronously
        /// </summary>
        /// <param name="product">The product to update</param>
        /// <returns>True if the product was updated successfully; otherwise, false</returns>
        public async Task<bool> UpdateProductAsync(Product product)
        {
            if (product == null)
            {
                return false;
            }

            try
            {
                if (!await ProductExistsAsync(product.ProductId))
                {
                    return false;
                }

                _context.Entry(product).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Deletes a product from the database asynchronously
        /// </summary>
        /// <param name="id">The ID of the product to delete</param>
        /// <returns>True if the product was deleted successfully; otherwise, false</returns>
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

        /// <summary>
        /// Checks if a product with the specified ID exists in the database asynchronously
        /// </summary>
        /// <param name="id">The ID of the product to check</param>
        /// <returns>True if the product exists; otherwise, false</returns>
        private async Task<bool> ProductExistsAsync(int id)
        {
            return await _context.Product.AnyAsync(p => p.ProductId == id);
        }

        // Maintain backward compatibility with non-async methods
        /// <summary>
        /// Retrieves all products from the database
        /// </summary>
        /// <returns>A list of all products</returns>
        public List<Product> GetAllProducts()
        {
            return GetAllProductsAsync().GetAwaiter().GetResult();
        }

        /// <summary>
        /// Retrieves a product by its ID
        /// </summary>
        /// <param name="id">The ID of the product to retrieve</param>
        /// <returns>The product if found; otherwise, null</returns>
        public Product? GetProductById(int id)
        {
            return GetProductByIdAsync(id).GetAwaiter().GetResult();
        }

        /// <summary>
        /// Adds a new product to the database
        /// </summary>
        /// <param name="product">The product to add</param>
        /// <returns>True if the product was added successfully; otherwise, false</returns>
        public bool AddProduct(Product product)
        {
            return AddProductAsync(product).GetAwaiter().GetResult();
        }

        /// <summary>
        /// Updates an existing product in the database
        /// </summary>
        /// <param name="product">The product to update</param>
        /// <returns>True if the product was updated successfully; otherwise, false</returns>
        public bool UpdateProduct(Product product)
        {
            return UpdateProductAsync(product).GetAwaiter().GetResult();
        }

        /// <summary>
        /// Deletes a product from the database
        /// </summary>
        /// <param name="id">The ID of the product to delete</param>
        /// <returns>True if the product was deleted successfully; otherwise, false</returns>
        public bool DeleteProduct(int id)
        {
            return DeleteProductAsync(id).GetAwaiter().GetResult();
        }
    }
}