using AdventureWorks.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace AdventureWorks.Web.Services
{
    public class ProductService : IProductService
    {
        private readonly sampledbContext _context;

        public ProductService(sampledbContext context)
        {
            _context = context;
        }

        public List<Product> GetAllProducts()
        {
            return _context.Product
                .Include(p => p.ProductCategory)
                .Include(p => p.ProductModel)
                .ToList();
        }

        public Product? GetProductById(int id)
        {
            return _context.Product
                .Include(p => p.ProductCategory)
                .Include(p => p.ProductModel)
                .FirstOrDefault(p => p.ProductId == id);
        }

        public bool AddProduct(Product product)
        {
            try
            {
                _context.Product.Add(product);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool UpdateProduct(Product product)
        {
            try
            {
                _context.Entry(product).State = EntityState.Modified;
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteProduct(int id)
        {
            try
            {
                var product = _context.Product.Find(id);
                if (product != null)
                {
                    _context.Product.Remove(product);
                    _context.SaveChanges();
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }
    }
}