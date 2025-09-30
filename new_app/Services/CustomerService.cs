using AdventureWorks.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace AdventureWorks.Web.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly sampledbContext _context;

        public CustomerService(sampledbContext context)
        {
            _context = context;
        }

        public List<Customer> GetAllCustomers()
        {
            return _context.Customer.ToList();
        }

        public Customer? GetCustomerById(int id)
        {
            return _context.Customer.Find(id);
        }

        public bool AddCustomer(Customer customer)
        {
            try
            {
                _context.Customer.Add(customer);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool UpdateCustomer(Customer customer)
        {
            try
            {
                _context.Entry(customer).State = EntityState.Modified;
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteCustomer(int id)
        {
            try
            {
                var customer = _context.Customer.Find(id);
                if (customer != null)
                {
                    _context.Customer.Remove(customer);
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